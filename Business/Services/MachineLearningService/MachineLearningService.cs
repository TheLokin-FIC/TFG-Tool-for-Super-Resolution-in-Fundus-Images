using Business.Exceptions;
using Repository.DAOs.Collections;
using Repository.DAOs.MachineLearningModelDAO;
using Repository.Exceptions;
using Repository.Persistence.Models;
using SharedData.Collections;
using SharedData.MachineLearning;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business.Services.MachineLearningService
{
    public class MachineLearningService : IMachineLearningService
    {
        private readonly IMachineLearningModelDAO machineLearningModelDAO;

        public MachineLearningService(IMachineLearningModelDAO machineLearningModelDAO)
        {
            this.machineLearningModelDAO = machineLearningModelDAO;
        }

        public Page<ModelDetails> GetPage(int pageSize, int pageIndex, string searchTerm)
        {
            try
            {
                PageList<MachineLearningModel> pageList = machineLearningModelDAO.FindPageByTerm(pageSize, pageIndex - 1, searchTerm);

                Page<ModelDetails> page = new()
                {
                    PageSize = pageList.PageSize,
                    PageIndex = pageList.PageIndex + 1,
                    TotalPages = pageList.TotalPages,
                    Items = new List<ModelDetails>(),
                    HasPreviousPage = pageList.HasPreviousPage,
                    HasNextPage = pageList.HasNextPage
                };

                foreach (MachineLearningModel machineLearningModel in pageList.Items)
                {
                    page.Items.Add(new ModelDetails()
                    {
                        Id = machineLearningModel.Id,
                        Title = machineLearningModel.Name,
                        Subtitle = $"{machineLearningModel.Architecture} {machineLearningModel.Loss}",
                        Description = machineLearningModel.ShortDescription,
                        Date = machineLearningModel.CreationDate
                    });
                }

                return page;
            }
            catch (Exception e) when (e is PageSizeException || e is PageIndexException)
            {
                throw new PageException(e);
            }
        }
    }
}