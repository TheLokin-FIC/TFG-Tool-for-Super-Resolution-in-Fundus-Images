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
        public IActionResult Register([FromBody] UserRegister userRegister)
        {
            try
            {
                return StatusCode(StatusCodes.Status201Created, userProfileService.Register(userRegister));
            }
            catch (ArgumentException)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }
    }
}