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

        [HttpGet("getConsultantList")]
        public IEnumerable<User> GetConsultantList()
        {
            var result = _userRepository.FilterBy(
                filter => filter.Role == "Konsult" || filter.Role == "Consultant");

            return result;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task Post(User newUser)
        {
            await _userRepository.InsertOneAsync(newUser);
        }

        //https://localhost:44390/user/?field=FirstName&findThis=test&update=Horse
        [HttpPut]
        public IActionResult Put(string field, string findThis, string update)
        {
            //repository Users = new repository();
            //Users.Update("user", new BsonDocument(field, findThis), field, update);
            return Ok();

        //            public virtual async Task ReplaceOneAsync(TDocument document)
        //{
        //    var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, document.Id);
        //    await _collection.FindOneAndReplaceAsync(filter, document);
        //}
    }

        [HttpPost("login")]
        public async Task<IActionResult> Login (Login user)
        {
            var token = Authenticate(user.Email, user.Password);

            if (token == null)
                return Unauthorized();

            return Ok(token);
        }

        public string Authenticate(string email, string password)
        {

            var user = _userRepository.FindOne(
                filter => filter.Email == email && filter.Password == password);

            if (user == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenKey = Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]{
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.Role, user.Role)
                }),

                Expires = DateTime.UtcNow.AddHours(1),

                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature
                    ),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
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