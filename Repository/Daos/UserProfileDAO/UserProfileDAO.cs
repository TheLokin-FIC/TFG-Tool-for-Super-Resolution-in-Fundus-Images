using Repository.DAOs.GenericDAO.CacheDAO;
using Repository.Persistence;
using Repository.Persistence.Models;

namespace Repository.Daos.UserProfileDAO
{
    public class UserProfileDAO : CacheDAO<UserProfile>, IUserProfileDAO
    {
        public UserProfileDAO(ApplicationDbContext context) : base(context)
        {
        }
    }
}