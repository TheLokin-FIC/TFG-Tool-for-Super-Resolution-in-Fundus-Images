using Repository.Exceptions;
using Repository.Persistence;
using Repository.Persistence.Models;
using System.Collections.Generic;
using System.Linq;

namespace Repository.DAOs.GenericDAO.UserProfileDAO
{
    public class UserProfileDAO : GenericDAO<UserProfile>, IUserProfileDAO
    {
        public UserProfileDAO(ApplicationDbContext context) : base(context)
        {
        }

        public UserProfile FindByUsername(string username)
        {
            IEnumerable<UserProfile> userProfileContext = context.Set<UserProfile>().AsEnumerable();

            UserProfile userProfile = userProfileContext.SingleOrDefault(u => u.Username == username);

            if (userProfile == null)
            {
                throw new InstanceNotFoundException(typeof(UserProfile), username);
            }

            return userProfile;
        }
    }
}