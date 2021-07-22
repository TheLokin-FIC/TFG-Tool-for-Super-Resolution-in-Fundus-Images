using Repository.DAOs;
using Repository.Exceptions;
using Repository.Persistence.Models;

namespace Repository.DAOs.UserProfileDAO
{
    public interface IUserProfileDAO : IDAO<UserProfile>
    {
        /// <exception cref="InstanceNotFoundException"/>
        UserProfile FindByUsername(string username);
    }
}