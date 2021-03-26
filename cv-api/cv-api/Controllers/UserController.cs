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

        [HttpGet]
        public IEnumerable<string> Get()
        {
            var users = _userRepository.FilterBy(
                filter => filter.FirstName != "",
                projection => projection.FirstName);

            return users;
        }

        [HttpPost]
        public async Task Post(User newUser)
        {
            await _userRepository.InsertOneAsync(newUser);
        }

        //https://localhost:44390/user/?field=FirstName&findThis=test&update=Horse
        [HttpPut]
        public IActionResult Put(string field, string findThis, string update)
        {
            repository Users = new repository();
            Users.Update("user", new BsonDocument(field, findThis), field, update);
            return Ok();
        }
    }
}
