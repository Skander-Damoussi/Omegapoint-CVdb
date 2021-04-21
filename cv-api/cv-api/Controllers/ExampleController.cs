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
    public class ExampleController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IMongoRepository<Example> _exampleRepository;

        public ExampleController(ILogger<UserController> logger, IMongoRepository<Example> exampleRepository)
        {
            _logger = logger;
            _exampleRepository = exampleRepository;
        }

        [HttpPost("registerPerson")]
        public async Task AddPerson(Example Foo)
        {
            await _exampleRepository.InsertOneAsync(Foo);
        }

        [HttpGet("getPeopleData")]
        public IEnumerable<string> GetPeopleData()
        {           
            
            var people = _exampleRepository.FilterBy(
                filter => filter.FirstName != "Horse",
                projection => projection.FirstName
            );
            return people;
        }
    }
}
