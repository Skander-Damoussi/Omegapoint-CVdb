using cv_api.Models;
using cv_api.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cv_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly ILogger<SearchController> _logger;
        private readonly IMongoRepository<User> _searchRepository;

        public SearchController(ILogger<SearchController> logger, IMongoRepository<User> searchRepository)
        {
            _logger = logger;
            _searchRepository = searchRepository;
        }
        public IActionResult Index()
        {
            return Ok();
        }
    }
}
