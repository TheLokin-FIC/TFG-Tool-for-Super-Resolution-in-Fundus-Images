using Repository.Persistence;
using Repository.Persistence.Models;
using System.Collections.Generic;
using System.Linq;

namespace Repository.DAOs.GenericDAO.SuperResolutionModelDAO
{
    public class SuperResolutionModelDAO : GenericDAO<SuperResolutionModel>, ISuperResolutionModelDAO
    {
        public SuperResolutionModelDAO(ApplicationDbContext context) : base(context)
        {
        }

        public IList<SuperResolutionModel> FindByModelId(int modelId)
        {
            IEnumerable<SuperResolutionModel> superResolutionModelContext = context.Set<SuperResolutionModel>().AsEnumerable();

            return superResolutionModelContext
                .Where(x => x.ModelId == modelId)
                .OrderBy(x => x.UpscaleFactor)
                .ToList();
        }
    }
}