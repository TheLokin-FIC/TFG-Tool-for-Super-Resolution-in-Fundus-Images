using Microsoft.EntityFrameworkCore;
using Repository.DAOs.Collections;
using Repository.DAOs.GenericDAO.CacheDAO;
using Repository.Exceptions;
using Repository.Persistence;
using Repository.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.DAOs.MachineLearningModelDAO
{
    public class MachineLearningModelDAO : CacheDAO<MachineLearningModel>, IMachineLearningModelDAO
    {
        public MachineLearningModelDAO(ApplicationDbContext context) : base(context)
        {
        }

        public IList<MachineLearningModel> FindByName(string name)
        {
            return FromCache($"{typeof(MachineLearningModel).Name}FindByName={name}", () =>
            {
                DbSet<MachineLearningModel> machineLearningModelContext = context.Set<MachineLearningModel>();

                return machineLearningModelContext
                    .Where(x => x.Name == name)
                    .OrderBy(x => x.Id)
                    .ToList();
            }, typeof(MachineLearningModel));
        }

        public PageList<MachineLearningModel> FindPageByTerm(int pageSize, int pageIndex, string searchTerm)
        {
            return FromCache($"{typeof(MachineLearningModel).Name}FindPageByTerm={pageSize},{pageIndex},{searchTerm}", () =>
            {
                DbSet<MachineLearningModel> machineLearningModelContext = context.Set<MachineLearningModel>();

                if (pageSize <= 0)
                {
                    throw new PageSizeException(pageSize);
                }

                int totalPages = (int)Math.Ceiling((double)machineLearningModelContext.Count() / pageSize);
                if (pageIndex < 0 || pageIndex >= totalPages)
                {
                    throw new PageIndexException(pageIndex, totalPages);
                }

                IList<MachineLearningModel> items;
                if (string.IsNullOrWhiteSpace(searchTerm))
                {
                    items = machineLearningModelContext
                        .Skip(pageSize * pageIndex)
                        .Take(pageSize)
                        .OrderBy(x => x.CreationDate)
                        .ToList();
                }
                else
                {
                    string[] words = searchTerm.ToLower().Split(" ");
                    items = machineLearningModelContext
                        .AsEnumerable()
                        .Where(x => words.Any(w => x.Name.ToLower().Contains(w) || x.Architecture.ToLower().Contains(w) || x.Loss.ToLower().Contains(w)))
                        .Skip(pageSize * pageIndex)
                        .Take(pageSize)
                        .OrderBy(x => x.CreationDate)
                        .ToList();
                }

                return new PageList<MachineLearningModel>()
                {
                    PageSize = pageSize,
                    PageIndex = pageIndex,
                    TotalPages = totalPages,
                    Items = items
                };
            }, typeof(MachineLearningModel));
        }
    }
}