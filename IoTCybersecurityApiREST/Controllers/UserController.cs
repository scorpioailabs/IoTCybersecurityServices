using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IoTCybersecurityAPI.Library.DataAccess;
using IoTCybersecurityAPI.Library.Models;
using IoTCybersecurityApiREST.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace IoTCybersecurityApiREST.Controllers
{
    [Route("api/{controller}")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        public UserController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            IConfiguration config)
        {
            _context = context;
            _userManager = userManager;
            _config = config;
        }

        // GET: User/Details/5
        [HttpGet]
        public UserModel GetById()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier); 
            UserData data = new UserData(_config);

            return data.GetUserById(userId).First();
        }
    }
}