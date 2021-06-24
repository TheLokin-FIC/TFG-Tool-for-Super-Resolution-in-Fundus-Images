using Repository.DAOs.GenericDAO.CacheDAO;
using Repository.Persistence;
using Repository.Persistence.Models;

namespace Repository.DAOs.MachineLearningModelDAO
{
    public class MachineLearningModelDAO : CacheDAO<MachineLearningModel>, IMachineLearningModelDAO
    {
        public MachineLearningModelDAO(ApplicationDbContext context) : base(context)
        {
        }
    }
}