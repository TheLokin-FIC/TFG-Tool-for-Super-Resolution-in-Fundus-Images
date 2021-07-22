using Business.Exceptions;
using Business.Utils;
using DataTransfer.Input.UserProfile;
using DataTransfer.Output.UserProfile;
using Repository.DAOs.UserProfileDAO;
using Repository.Exceptions;
using Repository.Persistence.Models;
using System;

namespace Business.Services.UserProfileService
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IUserProfileDAO userProfileDAO;

        public UserProfileService(IUserProfileDAO userProfileDAO)
        {
            this.userProfileDAO = userProfileDAO;
        }

        public UserDetails Login(string username, string password)
        {
            try
            {
                UserProfile userProfile = userProfileDAO.FindByUsername(username);

                string encryptedPassword = Encrypter.Crypt(password);
                if (userProfile.EncryptedPassword != encryptedPassword)
                {
                    throw new AuthenticationException();
                }

                return new UserDetails()
                {
                    Id = userProfile.Id,
                    Role = userProfile.Role,
                    Username = userProfile.Username
                };
            }
            catch (InstanceNotFoundException)
            {
                throw new AuthenticationException();
            }
        }

        public UserDetails Register(UserRegister userRegister)
        {
            IsValidUsername(userRegister.Username);
            IsValidPassword(userRegister.Password);

            try
            {
                userProfileDAO.FindByUsername(userRegister.Username);

                throw new ArgumentException("Invalid username");
            }
            catch (InstanceNotFoundException)
            {
                UserProfile userProfile = new()
                {
                    Role = "user",
                    Username = userRegister.Username,
                    EncryptedPassword = Encrypter.Crypt(userRegister.Password),
                };
                userProfileDAO.Insert(userProfile);

                return new UserDetails()
                {
                    Id = userProfile.Id,
                    Role = userProfile.Role,
                    Username = userProfile.Username
                };
            }
        }

        private static void IsValidUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username) || username.Length < 4 || username.Length > 24)
            {
                throw new ArgumentException("Invalid username");
            }
        }

        private static void IsValidPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password) && password.Length >= 6 && password.Length <= 24)
            {
                throw new ArgumentException("Invalid password");
            }
        }
    }
}