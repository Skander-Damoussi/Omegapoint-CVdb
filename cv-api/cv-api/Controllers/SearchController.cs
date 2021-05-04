using cv_api.Areas.Identity.Data;
using cv_api.Models;
using cv_api.Repository;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<ApplicationUser> userManager;

        public SearchController(ILogger<SearchController> logger, IMongoRepository<User> searchRepository, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _searchRepository = searchRepository;
            this.userManager = userManager;
        }

        [HttpGet("getSearchResult/{search}")]
        public async Task<IActionResult> GetSearchResult(string search) 
        {
            try
            {
                var userSearch = await userManager.GetUsersInRoleAsync("Konsult");
                var result = userSearch.Where(x => x.FirstName.Contains(search)
                            || x.LastName.Contains(search) || x.Email.Contains(search));  
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }

            //List<ApplicationUser> result1 = new List<ApplicationUser>();

            //foreach (var user in userSearch)
            //{
            //    if(user.Experiences != null)
            //    {
            //        foreach (var exp in user.Experiences)
            //        {
            //            if (exp.Software.Contains(search) || exp.Title.Contains(search) || exp.Language.Contains(search) || exp.Assignments.Contains(search))
            //            {
            //                result1.Add(user);
            //            }
            //        }
            //    }

            //}

            
        }
    }
}
