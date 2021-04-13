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

        public UserController(ILogger<UserController> logger, IMongoRepository<User> userRepository, IConfiguration configuration)
        {
            _logger = logger;
            _userRepository = userRepository;
            _configuration = configuration;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            var users = _userRepository.FilterBy(
                filter => filter.FirstName != "",
                projection => projection.FirstName);

            return users;
        }

        //[Authorize(Roles = "Admin,Konsultchef")]
        [HttpGet("getConsultantList")]
        public IEnumerable<User> GetConsultantList()
        {
            var result = _userRepository.FilterBy(
                filter => filter.Role == "Konsult" || filter.Role == "Consultant");

            return result;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> Post(User newUser)
        {
            try
            {
                var user = _userRepository.FindOne(
                filter => filter.Email == newUser.Email);

                if (user != null)
                    return StatusCode(409, $"User {newUser.Email} already exist.");
                

                else
                    await _userRepository.InsertOneAsync(newUser);
                return StatusCode(200, "User har been created");
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "");
            }
            return BadRequest();
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

            var user = _userRepository.FindOne(
                filter => filter.Email == userInput.Email && filter.Password == userInput.Password);

            if (user == null)
                return Unauthorized();

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenKey = Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]{
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role)
                }),

                Expires = DateTime.UtcNow.AddHours(1),

                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature
                    ),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new
            {
                Token = tokenHandler.WriteToken(token),
                role = user.Role,
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

        ////https://localhost:44390/user/?field=FirstName&findThis=test&update=Horse
        //[HttpPut("updateUser")]
        //public async Task<User> Put(User user)
        //{
        //    //repository Users = new repository();
        //    //Users.Update("user", new BsonDocument(field, findThis), field, update);
        //    //return Ok(user);

        //    await _userRepository.

        //}

    }
}