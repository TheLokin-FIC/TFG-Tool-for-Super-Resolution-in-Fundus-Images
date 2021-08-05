using Business.Exceptions;
using Business.Services.UserProfileService;
using DataTransfer.Input.UserProfile;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Server.Controllers.UserProfileController
{
    [ApiController]
    [Route("api/account")]
    public class UserProfileController : Controller
    {
        private readonly IUserProfileService userProfileService;

        public UserProfileController(IUserProfileService userProfileService)
        {
            this.userProfileService = userProfileService;
        }

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Login([FromBody] UserCredentials userCredentials)
        {
            try
            {
                return StatusCode(StatusCodes.Status201Created, userProfileService.Login(userCredentials.Username, userCredentials.Password));
            }
            catch (AuthenticationException)
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
        }

        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Register([FromBody] NewUserProfile newUser)
        {
            try
            {
                return StatusCode(StatusCodes.Status201Created, userProfileService.Register(newUser));
            }
            catch (ArgumentException)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }

        [HttpGet]
        [Route("details/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetUser([FromRoute] long userId)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, userProfileService.GetUser(userId));
            }
            catch (NotFoundException)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }

        [HttpPut]
        [Route("password/{userId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult ChangePassword([FromRoute] long userId, [FromBody] NewPassword newPassword)
        {
            try
            {
                userProfileService.ChangePassword(userId, newPassword.OldPassword, newPassword.Password);

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (NotFoundException)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            catch (AuthenticationException)
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
            catch (ArgumentException)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }

        [HttpDelete]
        [Route("delete/{userId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteUser([FromRoute] long userId)
        {
            try
            {
                userProfileService.DeleteUser(userId);

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (NotFoundException)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }
    }
}