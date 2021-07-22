using Microsoft.EntityFrameworkCore;
using Repository.DAOs.GenericDAO.CacheDAO;
using Repository.Exceptions;
using Repository.Persistence;
using Repository.Persistence.Models;
using System.Linq;

namespace Repository.DAOs.UserProfileDAO
{
    public class UserProfileDAO : CacheDAO<UserProfile>, IUserProfileDAO
    {
        public UserProfileDAO(ApplicationDbContext context) : base(context)
        {
        }

        public UserProfile FindByUsername(string username)
        {
            return FromCache($"{typeof(UserProfile).Name}FindByUsername={username}", () =>
            {
                DbSet<UserProfile> userProfileContext = context.Set<UserProfile>();

                UserProfile userProfile = userProfileContext.SingleOrDefault(u => u.Username == username);

                if (userProfile is null)
                {
                    throw new InstanceNotFoundException(typeof(UserProfile), username);
                }

                return userProfile;
            }, typeof(UserProfile));
        }
    }
}