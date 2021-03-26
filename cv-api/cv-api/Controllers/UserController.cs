using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using cv_api.Models;
using MongoDB.Bson.Serialization;
using cv_api.Repository;

namespace cv_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IMongoRepository<User> _userRepository;

        public UserController(ILogger<UserController> logger, IMongoRepository<User> userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        [HttpGet("getUsers")]
        public IEnumerable<string> GetUsers() //TODO: Kommer vi behöver hämta alla users?
        {
            var people = _userRepository.FilterBy(
                filter => filter.FirstName != "Horse",
                projection => projection.FirstName
            );
            return people;
        }

        [HttpPost("registerUser")]
        public async Task<StatusCodeResult> Post(User newUser) //TODO: Skippa try-catch?
        {
            try
            {
                await _userRepository.InsertOneAsync(newUser);
                return Ok();
            }
            catch
            {
                // Conflict with the current state of the target resource StatusCode(409);
                return Conflict();
            }

            //repository Users = new repository();
            //Users.Post("user", newUser);
            //return Ok();
        }

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