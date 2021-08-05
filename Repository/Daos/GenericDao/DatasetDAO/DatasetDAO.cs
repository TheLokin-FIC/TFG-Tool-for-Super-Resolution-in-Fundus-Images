using Repository.DAOs.Collections;
using Repository.Exceptions;
using Repository.Persistence;
using Repository.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.DAOs.GenericDAO.DatasetDAO
{
    public class DatasetDAO : GenericDAO<Dataset>, IDatasetDAO
    {
        public DatasetDAO(ApplicationDbContext context) : base(context)
        {
        }

        public PageList<Dataset> FindPageByTerm(int pageSize, int pageIndex, string searchTerm)
        {
            IEnumerable<Dataset> datasetContext = context.Set<Dataset>().AsEnumerable();

            if (pageSize <= 0)
            {
                throw new PageSizeException(pageSize);
            }

            IEnumerable<Dataset> datasets;
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                datasets = datasetContext.Where(x => x.Public);
            }
            else
            {
                string[] words = searchTerm.ToLower().Split(" ");
                datasets = datasetContext.Where(x => words.Any(w => x.Title.ToLower().Contains(w)));
            }

            int totalPages = (int)Math.Ceiling((double)datasets.Count() / pageSize);
            if (pageIndex < 0 || pageIndex > totalPages)
            {
                throw new PageIndexException(pageIndex, totalPages);
            }

            return new PageList<Dataset>()
            {
                PageSize = pageSize,
                PageIndex = pageIndex,
                TotalPages = totalPages,
                Items = datasets
                    .OrderByDescending(x => x.LastModification)
                    .Skip(pageSize * pageIndex)
                    .Take(pageSize)
                    .ToList()
            };
        }

        public PageList<Dataset> FindPageByTermAndUser(int pageSize, int pageIndex, string searchTerm, long userId)
        {
            IEnumerable<Dataset> datasetContext = context.Set<Dataset>().AsEnumerable();

            if (pageSize <= 0)
            {
                throw new PageSizeException(pageSize);
            }

            IEnumerable<Dataset> datasets;
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                datasets = datasetContext.Where(x => x.UserId == userId);
            }
            else
            {
                string[] words = searchTerm.ToLower().Split(" ");
                datasets = datasetContext.Where(x => x.UserId == userId && words.Any(w => x.Title.ToLower().Contains(w)));
            }

            int totalPages = (int)Math.Ceiling((double)datasets.Count() / pageSize);
            if (pageIndex < 0 || pageIndex > totalPages)
            {
                throw new PageIndexException(pageIndex, totalPages);
            }

            return new PageList<Dataset>()
            {
                PageSize = pageSize,
                PageIndex = pageIndex,
                TotalPages = totalPages,
                Items = datasets
                    .OrderByDescending(x => x.LastModification)
                    .Skip(pageSize * pageIndex)
                    .Take(pageSize)
                    .ToList()
            };
        }
    }
}