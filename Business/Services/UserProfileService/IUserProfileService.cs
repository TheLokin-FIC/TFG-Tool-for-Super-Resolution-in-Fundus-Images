using Business.Exceptions;
using DataTransfer.Input.UserProfile;
using DataTransfer.Output.UserProfile;
using System;

namespace Business.Services.UserProfileService
{
    public interface IUserProfileService
    {
        /// <exception cref="AuthenticationException"/>
        UserDetails Login(string username, string password);

        /// <exception cref="ArgumentException"/>
        UserDetails Register(UserRegister userRegister);
    }
}