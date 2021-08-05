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
        UserDetails Register(NewUserProfile newUser);

        /// <exception cref="NotFoundException"/>
        UserDetails GetUser(long userId);

        /// <exception cref="NotFoundException"/>
        /// <exception cref="AuthenticationException"/>
        /// <exception cref="ArgumentException"/>
        void ChangePassword(long userId, string oldPassword, string newPassword);

        /// <exception cref="NotFoundException"/>
        void DeleteUser(long userId);
    }
}