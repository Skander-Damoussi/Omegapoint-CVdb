using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cv_api.Models;
using MongoDB.Bson.Serialization;
using cv_api.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Reflection;
using cv_api.Areas.Identity.Data;
using Microsoft.AspNetCore.Http;

namespace cv_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IMongoRepository<User> _userRepository;
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;

        public UserController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, ILogger<UserController> logger, IMongoRepository<User> userRepository, IConfiguration configuration)
        {
            _logger = logger;
            _userRepository = userRepository;
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await userManager.GetUsersInRoleAsync("Konsult");

            return Ok(result);
        }

        //[Authorize(Roles = "Admin,Konsultchef")]
        [HttpGet("getConsultantList")]
        public async Task<IActionResult> GetConsultantList()
        {
            var result = await userManager.GetUsersInRoleAsync("Konsult");

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Post(User newUser)
        {
            var userExists = await userManager.FindByNameAsync(newUser.Email);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError);

            ApplicationUser user = new ApplicationUser()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = newUser.Email,
                Email = newUser.Email,
                PhoneNumber = newUser.PhoneNo,
                FirstName = newUser.FirstName,
                LastName = newUser.LastName
            };
            var result = await userManager.CreateAsync(user, newUser.Password);

            if (!await roleManager.RoleExistsAsync(newUser.Role))
                await roleManager.CreateAsync(new ApplicationRole(newUser.Role));

            if (await roleManager.RoleExistsAsync(newUser.Role))
                await userManager.AddToRoleAsync(user, newUser.Role);

            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError);

            var returnUserId = await userManager.FindByNameAsync(newUser.Email);

            return Ok(new
            {
                userId = returnUserId.Id
            });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(UpdateUser updatedUser)
        {
            var user = _userRepository.FindById(updatedUser.Id);

            //CheckIfNull(updatedUser);

            if(updatedUser.FirstName != "" || user.FirstName != updatedUser.FirstName)
            {
               user.FirstName = updatedUser.FirstName;
            }
            if(updatedUser.LastName != "" || user.LastName != updatedUser.LastName)
            {
                user.LastName = updatedUser.LastName;
            }
            if(updatedUser.Password != "" || user.Password != updatedUser.Password)
            {
                user.Password = updatedUser.Password;
            }
            await _userRepository.ReplaceOneAsync(user);

            return Ok(user);

        }

        //public UpdateUser CheckIfNull (UpdateUser updatedUser)
        //{
        //    var user = _userRepository.FindById(updatedUser.Id);
        //    foreach (PropertyInfo prop in updatedUser.GetType().GetProperties())
        //    {
        //        var res = prop.GetValue(updatedUser, null);
        //        if (res != "")
        //        {
        //            var property = prop.Name;
        //            user.property = res;
                    
        //        }
        //        Console.WriteLine($"{prop.Name}: {prop.GetValue(updatedUser, null)}");
                
        //    }
        //    return updatedUser;
        //}


        [HttpPost("login")]
        public async Task<IActionResult> Authenticate(Login userInput)
        {
            var user = await userManager.FindByNameAsync(userInput.Email);

            if (user == null || await userManager.CheckPasswordAsync(user, userInput.Password) == false)
                return Unauthorized();

            var userRoles = await userManager.GetRolesAsync(user);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]{
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                }),

                Expires = DateTime.UtcNow.AddHours(1),

                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"])),
                    SecurityAlgorithms.HmacSha256Signature
                    ),
            };

            foreach (var userRole in userRoles)
            {
                tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, userRole));
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new
            {
                Token = tokenHandler.WriteToken(token),
                role = userRoles[0],
                firstName = user.FirstName,
                lastName = user.LastName,
                userId = user.Id.ToString()
            });
        }


        //[HttpPost("registerUser")]
        //public async Task<StatusCodeResult> Post(User newUser) //TODO: Skippa try-catch?
        //{
        //    try
        //    {
        //        await _userRepository.InsertOneAsync(newUser);
        //        return Ok();
        //    }
        //    catch
        //    {
        //        // Conflict with the current state of the target resource StatusCode(409);
        //        return Conflict();
        //    }

            //repository Users = new repository();
            //Users.Post("user", newUser);
            //return Ok();
        //}

    }
}