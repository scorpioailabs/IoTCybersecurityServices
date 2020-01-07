using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IoTCybersecurityAPI.Library.DataAccess;
using IoTCybersecurityAPI.Library.Models;
using IoTCybersecurityApiREST.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace IoTCybersecurityApiREST.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _config;


        public AccountController(IConfiguration config, UserManager<ApplicationUser> userManager)
        {
            _config = config;
            _userManager = userManager;
        }


        [HttpPost]
        [Route("/register")]
        public async Task Register([FromBody]RegisterUserModel model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                var uiUser = new UserModel
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    EmailAddress = user.Email
                };

                UserData data = new UserData(_config);
                data.Add(uiUser);
            }
        }
    }
}