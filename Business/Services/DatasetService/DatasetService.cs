using Business.Exceptions;
using Business.Utils;
using DataTransfer.Input.Dataset;
using DataTransfer.Output.Collections;
using DataTransfer.Output.Dataset;
using Microsoft.Data.SqlClient;
using Repository.DAOs.Collections;
using Repository.DAOs.GenericDAO.DatasetDAO;
using Repository.DAOs.GenericDAO.ImageDAO;
using Repository.DAOs.GenericDAO.UserProfileDAO;
using Repository.Exceptions;
using Repository.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using Image = Repository.Persistence.Models.Image;

namespace Business.Services.DatasetService
{
    public class DatasetService : IDatasetService
    {
        private readonly IUserProfileDAO userProfileDAO;
        private readonly IDatasetDAO datasetDAO;
        private readonly IImageDAO imageDAO;

        public DatasetService(IUserProfileDAO userProfileDAO, IDatasetDAO datasetDAO, IImageDAO imageDAO)
        {
            this.userProfileDAO = userProfileDAO;
            this.datasetDAO = datasetDAO;
            this.imageDAO = imageDAO;
        }

        public Page<DatasetInfo> GetDatasetPage(int pageSize, int pageIndex, string searchTerm, long? userId)
        {
            try
            {
                PageList<Dataset> pageList;
                if (userId == null)
                {
                    pageList = datasetDAO.FindPageByTerm(pageSize, pageIndex - 1, searchTerm);
                }
                else
                {
                    pageList = datasetDAO.FindPageByTermAndUser(pageSize, pageIndex - 1, searchTerm, (long)userId);
                }

                Page<DatasetInfo> page = new()
                {
                    PageSize = pageList.PageSize,
                    PageIndex = pageList.PageIndex + 1,
                    TotalPages = pageList.TotalPages,
                    Items = new List<DatasetInfo>(),
                    HasPreviousPage = pageList.HasPreviousPage,
                    HasNextPage = pageList.HasNextPage
                };

                foreach (Dataset dataset in pageList.Items)
                {
                    page.Items.Add(new DatasetInfo()
                    {
                        Id = dataset.Id,
                        Cover = imageDAO.FindCover(dataset.Id).File,
                        Title = dataset.Title,
                        Date = dataset.LastModification,
                        Public = dataset.Public,
                        Size = imageDAO.FindByDatasetId(dataset.Id).Sum(x => x.Size)
                    });
                }

                return page;
            }
            catch (Exception e) when (e is PageSizeException || e is PageIndexException)
            {
                throw new PageException(e);
            }
        }

        public long CreateDataset(long userId, NewDataset newDataset)
        {
            if (userProfileDAO.Exists(userId))
            {
                try
                {
                    Dataset dataset = new()
                    {
                        UserId = userId,
                        Title = newDataset.Title.Trim(),
                        Public = newDataset.Public,
                        LastModification = DateTime.Now
                    };
                    datasetDAO.Insert(dataset);

                    foreach (NewImage newImage in newDataset.Images)
                    {
                        imageDAO.Insert(new Image()
                        {
                            DatasetId = dataset.Id,
                            Name = newImage.Name.Trim(),
                            Size = newImage.File.Length,
                            File = newImage.File
                        });
                    }

                    return dataset.Id;
                }
                catch (SqlException e)
                {
                    throw new ArgumentException(e.Message);
                }
            }
            else
            {
                throw new AuthenticationException();
            }
        }

        public DatasetDetails GetDataset(long datasetId, long? userId)
        {
            try
            {
                Dataset dataset = datasetDAO.Find(datasetId);
                if (!dataset.Public && dataset.UserId != userId)
                {
                    throw new AuthenticationException();
                }

                double size = 0;
                IList<ImageDetails> images = new List<ImageDetails>();
                foreach (Image image in imageDAO.FindByDatasetId(dataset.Id))
                {
                    size += image.Size;
                    images.Add(new ImageDetails()
                    {
                        Name = image.Name,
                        Size = image.Size,
                        File = image.File
                    });
                }

                return new DatasetDetails()
                {
                    Id = dataset.Id,
                    Title = dataset.Title,
                    Date = dataset.LastModification,
                    Public = dataset.Public,
                    Size = size,
                    Images = images
                };
            }
            catch (InstanceNotFoundException)
            {
                throw new NotFoundException(datasetId);
            }
        }

        public IList<ImageDetails> GetImagesDataset(long datasetId, long? userId)
        {
            try
            {
                Dataset dataset = datasetDAO.Find(datasetId);
                if (!dataset.Public && dataset.UserId != userId)
                {
                    throw new AuthenticationException();
                }

                IList<ImageDetails> images = new List<ImageDetails>();
                foreach (Image image in imageDAO.FindByDatasetId(dataset.Id))
                {
                    images.Add(new ImageDetails()
                    {
                        Name = image.Name,
                        Size = image.Size,
                        File = image.File
                    });
                }

                return images;
            }
            catch (InstanceNotFoundException)
            {
                throw new NotFoundException(datasetId);
            }
        }

        public void UpdateDataset(long datasetId, long userId, NewDataset newDataset)
        {
            try
            {
                Dataset dataset = datasetDAO.Find(datasetId);
                if (dataset.UserId != userId)
                {
                    throw new AuthenticationException();
                }

                dataset.Title = newDataset.Title.Trim();
                dataset.Public = newDataset.Public;
                dataset.LastModification = DateTime.Now;
                datasetDAO.Update(dataset);

                IList<Image> newImages = new List<Image>();
                foreach (NewImage image in newDataset.Images)
                {
                    newImages.Add(new Image()
                    {
                        DatasetId = datasetId,
                        Name = image.Name.Trim(),
                        Size = image.File.Length,
                        File = image.File
                    });
                }
                IList<Image> oldImages = imageDAO.FindByDatasetId(datasetId);

                foreach (Image image in oldImages.Except(newImages))
                {
                    imageDAO.Delete(image.DatasetId, image.Name);
                }
                foreach (Image image in newImages.Except(oldImages))
                {
                    imageDAO.Insert(image);
                }
            }
            catch (InstanceNotFoundException)
            {
                throw new NotFoundException(datasetId);
            }
        }

        public void DeleteDataset(long datasetId, long userId)
        {
            try
            {
                Dataset dataset = datasetDAO.Find(datasetId);
                if (dataset.UserId != userId)
                {
                    throw new AuthenticationException();
                }

                datasetDAO.Delete(datasetId);
            }
            catch (InstanceNotFoundException)
            {
                throw new NotFoundException(datasetId);
            }
        }
    }
}