using Repository.DAOs.Collections;
using Repository.Exceptions;
using Repository.Persistence;
using Repository.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.DAOs.GenericDAO.MachineLearningModelDAO
{
    public class MachineLearningModelDAO : GenericDAO<MachineLearningModel>, IMachineLearningModelDAO
    {
        public MachineLearningModelDAO(ApplicationDbContext context) : base(context)
        {
        }

        public PageList<MachineLearningModel> FindPageByTerm(int pageSize, int pageIndex, string searchTerm)
        {
            IEnumerable<MachineLearningModel> machineLearningModelContext = context.Set<MachineLearningModel>().AsEnumerable();

            if (pageSize <= 0)
            {
                throw new PageSizeException(pageSize);
            }

            int totalPages;
            IList<MachineLearningModel> items;
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                items = machineLearningModelContext
                    .OrderByDescending(x => x.CreationDate)
                    .Skip(pageSize * pageIndex)
                    .Take(pageSize)
                    .ToList();

                totalPages = (int)Math.Ceiling((double)machineLearningModelContext.Count() / pageSize);
            }
            else
            {
                string[] words = searchTerm.ToLower().Split(" ");
                IEnumerable<MachineLearningModel> machineLearningModels = machineLearningModelContext
                    .Where(x => words.Any(w => x.Name.ToLower().Contains(w) ||
                                               x.Architecture.ToLower().Contains(w) ||
                                               x.Loss.ToLower().Contains(w)));

                items = machineLearningModels
                    .OrderByDescending(x => x.CreationDate)
                    .Skip(pageSize * pageIndex)
                    .Take(pageSize)
                    .ToList();

                totalPages = (int)Math.Ceiling((double)machineLearningModels.Count() / pageSize);
            }

            if (pageIndex < 0 || pageIndex > totalPages)
            {
                throw new PageIndexException(pageIndex, totalPages);
            }

            return new PageList<MachineLearningModel>()
            {
                PageSize = pageSize,
                PageIndex = pageIndex,
                TotalPages = totalPages,
                Items = items
            };
        }

        public IList<MachineLearningModel> FindByName(string name)
        {
            IEnumerable<MachineLearningModel> machineLearningModelContext = context.Set<MachineLearningModel>().AsEnumerable();

            return machineLearningModelContext
                .Where(x => x.Name == name)
                .OrderBy(x => x.Id)
                .ToList();
        }
    }
}