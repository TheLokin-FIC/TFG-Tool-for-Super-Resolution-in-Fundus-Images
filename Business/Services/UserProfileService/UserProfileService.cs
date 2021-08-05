using Business.Exceptions;
using Business.Utils;
using DataTransfer.Input.UserProfile;
using DataTransfer.Output.UserProfile;
using Microsoft.Data.SqlClient;
using Repository.DAOs.GenericDAO.UserProfileDAO;
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
                UserProfile userProfile = userProfileDAO.FindByUsername(username.Trim());

                string encryptedPassword = Encrypter.Crypt(password.Trim());
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

        public UserDetails Register(NewUserProfile newUser)
        {
            try
            {
                userProfileDAO.FindByUsername(newUser.Username);

                throw new ArgumentException("Invalid username");
            }
            catch (InstanceNotFoundException)
            {
                try
                {
                    UserProfile userProfile = new()
                    {
                        Role = "user",
                        Username = newUser.Username.Trim(),
                        EncryptedPassword = Encrypter.Crypt(newUser.Password.Trim()),
                    };
                    userProfileDAO.Insert(userProfile);

                    return new UserDetails()
                    {
                        Id = userProfile.Id,
                        Role = userProfile.Role,
                        Username = userProfile.Username
                    };
                }
                catch (SqlException e)
                {
                    throw new ArgumentException(e.Message);
                }
            }
        }

        public UserDetails GetUser(long userId)
        {
            try
            {
                UserProfile userProfile = userProfileDAO.Find(userId);

                return new UserDetails()
                {
                    Id = userProfile.Id,
                    Role = userProfile.Role,
                    Username = userProfile.Username
                };
            }
            catch (InstanceNotFoundException)
            {
                throw new NotFoundException(userId);
            }
        }

        public void ChangePassword(long userId, string oldPassword, string newPassword)
        {
            try
            {
                UserProfile userProfile = userProfileDAO.Find(userId);
                if (userProfile.EncryptedPassword != Encrypter.Crypt(oldPassword.Trim()))
                {
                    throw new AuthenticationException();
                }

                userProfile.EncryptedPassword = Encrypter.Crypt(newPassword.Trim());
                userProfileDAO.Update(userProfile);
            }
            catch (InstanceNotFoundException)
            {
                throw new NotFoundException(userId);
            }
            catch (SqlException e)
            {
                throw new ArgumentException(e.Message);
            }
        }

        public void DeleteUser(long userId)
        {
            try
            {
                userProfileDAO.Delete(userId);
            }
            catch (InstanceNotFoundException)
            {
                throw new NotFoundException(userId);
            }
        }
    }
}