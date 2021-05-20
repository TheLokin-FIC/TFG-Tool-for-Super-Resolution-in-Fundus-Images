using Microsoft.EntityFrameworkCore;
using Model.Persistence.Data;
using Model.Persistence.Models;
using System.Collections.Generic;
using System.Linq;

namespace Model.Persistence.Daos.SuperResolutionModelDao
{
    public class SuperResolutionModelDaoCacheEntityFramework : GenericDaoCacheEntityFramework<SuperResolutionModel, int>, ISuperResolutionModelDao
    {
        public SuperResolutionModelDaoCacheEntityFramework(ApplicationDbContext context) : base(context)
        {
        }

        public IList<SuperResolutionModel> FindByName(string name)
        {
            return FromCache(entityClass.Name + "FindByName=" + name, () =>
            {
                DbSet<SuperResolutionModel> superResolutionModelContext = context.Set<SuperResolutionModel>();

                return superResolutionModelContext.Where(m => m.Name == name).OrderBy(m => m.UpscaleFactor).ToList();
            }, entityClass.Name);
        }
    }
}