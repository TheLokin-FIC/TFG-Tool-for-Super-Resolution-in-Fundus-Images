using Microsoft.EntityFrameworkCore;
using Repository.DAOs.GenericDAO.CacheDAO;
using Repository.Persistence;
using Repository.Persistence.Models;
using System.Collections.Generic;
using System.Linq;

namespace Repository.DAOs.SuperResolutionModelDAO
{
    public class SuperResolutionModelDAO : CacheDAO<SuperResolutionModel>, ISuperResolutionModelDAO
    {
        public SuperResolutionModelDAO(ApplicationDbContext context) : base(context)
        {
        }

        public IList<SuperResolutionModel> FindByModelId(int modelId)
        {
            return FromCache($"{typeof(SuperResolutionModel).Name}FindByModelId={modelId}", () =>
            {
                DbSet<SuperResolutionModel> superResolutionModelContext = context.Set<SuperResolutionModel>();

                return superResolutionModelContext.Where(superResolutionModel => superResolutionModel.ModelId == modelId)
                    .OrderBy(superResolutionModelContext => superResolutionModelContext.UpscaleFactor)
                    .ToList();
            }, typeof(SuperResolutionModel));
        }
    }
}