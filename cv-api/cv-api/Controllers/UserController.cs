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

namespace cv_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            repository Users = new repository();
            List<BsonDocument> list = Users.Get("user").ToList();

            List<User> result = new List<User>();

            foreach(BsonDocument user in list)
            {
               result.Add(BsonSerializer.Deserialize<User>(user));
            }

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post(User newUser)
        {
            repository Users = new repository();
            Users.Post("user", newUser);
            return Ok();
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
