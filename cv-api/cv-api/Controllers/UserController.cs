﻿using Microsoft.AspNetCore.Mvc;
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
using NETCore.MailKit.Core;

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
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailService;

        public UserController(UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager, 
            RoleManager<ApplicationRole> roleManager, 
            ILogger<UserController> logger, 
            IMongoRepository<User> userRepository, 
            IConfiguration configuration,
            IEmailService emailService)
        {
            _logger = logger;
            _userRepository = userRepository;
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
            _signInManager = signInManager;
            _emailService = emailService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await userManager.GetUsersInRoleAsync("Konsult");

            return Ok(result);
        }

        //[Authorize(Roles = "Admin,Konsultchef")]
        //[Authorize(Roles = "Konsultchef")]
        [HttpGet("getConsultantList")]
        public async Task<IActionResult> GetConsultantList()
        {
            var result = await userManager.GetUsersInRoleAsync("Konsult");

            return Ok(result);
        }

        //[Authorize(Roles = "Konsult")]
        [HttpGet("getConsultantExperienceList/{userId}")]
        public async Task<IActionResult> getConsultantExperienceList(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            return Ok(user.Experiences);
        }

        //[Authorize(Roles = "Konsult")]
        [HttpPost("postExperience")]
        public async Task<IActionResult> postExperience(ExperienceDTO input)
        {
            var user = await userManager.FindByIdAsync(input.userID);

            if (input.newExperience)
            {
                if(user.Experiences == null)
                {
                    user.Experiences = new List<Experience>();
                }
                user.Experiences.Add(new Experience
                {
                    Title = input.title,
                    Assignments = input.Assignments,
                    Language = input.Language,
                    Role = input.Role,
                    StartDate = input.startDate,
                    EndDate = input.endDate,
                    Software = input.Software,
                    id = input.id
                });
            }
            else
            {
                for(int i = 0; i < user.Experiences.Count; i++)
                {
                    
                }
            }
            await userManager.UpdateAsync(user);
            return Ok(user.Experiences);
        }

        //[Authorize(Roles = "Konsult")]
        [HttpPost("updateExperience")]
        public async Task<IActionResult> updateExperience(ExperienceDTO input)
        {
            var user = await userManager.FindByIdAsync(input.userID);
            for(int i = 0; i < user.Experiences.Count; i++)
            {
                if(user.Experiences[i].id == input.id)
                {
                    user.Experiences[i].Title = input.title;
                    user.Experiences[i].Assignments = input.Assignments;
                    user.Experiences[i].Language = input.Language;
                    user.Experiences[i].Role = input.Role;
                    user.Experiences[i].StartDate = input.startDate;
                    user.Experiences[i].EndDate = input.endDate;
                    user.Experiences[i].Software = input.Software;
                    await userManager.UpdateAsync(user);
                    return Ok(user.Experiences);
                }
            }
            return BadRequest();
        }

        //[Authorize(Roles = "Konsult")]
        [HttpPost("removeExperience")]
        public async Task<IActionResult> removeExperience(ExperienceDTO input)
        {
            var user = await userManager.FindByIdAsync(input.userID);
            for (int i = 0; i < user.Experiences.Count; i++)
            {
                if (user.Experiences[i].id == input.id)
                {
                    user.Experiences.RemoveAt(i);
                    await userManager.UpdateAsync(user);
                    return Ok(user.Experiences);
                }
            }
            return BadRequest();
        }

        //[Authorize(Roles = "Konsult")]
        [HttpGet("getConsultantPresentationList/{userId}")]
        public async Task<IActionResult> getConsultantPresentationList(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            return Ok(user.Presentations);
        }

        //[Authorize(Roles = "Konsult")]
        [HttpPost("postPresentation")]
        public async Task<IActionResult> postPresentation(PresentationDTO input)
        {
            var user = await userManager.FindByIdAsync(input.userID);

            if (input.newPresentation)
            {
                if (user.Presentations == null)
                {
                    user.Presentations = new List<Presentation>();
                }
                user.Presentations.Add(new Presentation
                {
                    Title = input.title,
                    Paragraph = input.Paragraph,
                    id = input.id
                });
            }
            else
            {
                for (int i = 0; i < user.Presentations.Count; i++)
                {

                }
            }
            await userManager.UpdateAsync(user);
            return Ok(user.Presentations);
        }

        //[Authorize(Roles = "Konsult")]
        [HttpPost("updatePresentation")]
        public async Task<IActionResult> updatePresentation(PresentationDTO input)
        {
            var user = await userManager.FindByIdAsync(input.userID);
            for (int i = 0; i < user.Presentations.Count; i++)
            {
                if (user.Presentations[i].id == input.id)
                {
                    user.Presentations[i].Title = input.title;
                    user.Presentations[i].Paragraph = input.Paragraph;
                    await userManager.UpdateAsync(user);
                    return Ok(user.Presentations);
                }
            }
            return BadRequest();
        }

        //[Authorize(Roles = "Konsult")]
        [HttpPost("removePresentation")]
        public async Task<IActionResult> removePresentation(PresentationDTO input)
        {
            var user = await userManager.FindByIdAsync(input.userID);
            for (int i = 0; i < user.Presentations.Count; i++)
            {
                if (user.Presentations[i].id == input.id)
                {
                    user.Presentations.RemoveAt(i);
                    await userManager.UpdateAsync(user);
                    return Ok(user.Presentations);
                }
            }
            return BadRequest();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Post(User newUser)
        {
            var userExists = await userManager.FindByNameAsync(newUser.Email);
            if (userExists != null)
                return BadRequest("There is already an user with that email");

            ApplicationUser user = new ApplicationUser()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = newUser.Email,
                Email = newUser.Email,
                PhoneNumber = newUser.PhoneNo,
                FirstName = newUser.FirstName,
                LastName = newUser.LastName
            };
            if(newUser.Role == "Konsult")
            {
                user.Experiences = new List<Experience>();
            }

            var result = await userManager.CreateAsync(user, newUser.Password);

            if (!await roleManager.RoleExistsAsync(newUser.Role))
                await roleManager.CreateAsync(new ApplicationRole(newUser.Role));

            if (await roleManager.RoleExistsAsync(newUser.Role))
                await userManager.AddToRoleAsync(user, newUser.Role);

            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError);

            var code = await userManager.GenerateEmailConfirmationTokenAsync(user);

            user.EmailConfirmationToken = code;

            var update = userManager.UpdateAsync(user);

            var link = Url.Action(nameof(VerifyEmail), "User", new { userId = user.Id , code}, Request.Scheme, Request.Host.ToString());


            var returnUserId = await userManager.FindByNameAsync(newUser.Email);

             await _emailService.SendAsync("skander_test@hotmail.com", "email verify", $"<a href=\"{link}\">Click here to verify email</a>", true);
            return Ok(new
            {
                userId = returnUserId.Id
            });
        }

        [HttpGet("verify")]
        public async Task<IActionResult> VerifyEmail(string userId, string code)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
                return BadRequest("Invalid confirmation or expired token.");


            if(user.EmailConfirmationToken == null)
            {
                if(user.EmailConfirmed)
                {
                    return BadRequest("Email has already been confirmed");
                }
            }

            if (user.EmailConfirmationToken != code)
            {
                return BadRequest("Invalid confirmation or expired token.");
            }

            var result = await userManager.ConfirmEmailAsync(user, user.EmailConfirmationToken);

            
            if (!result.Succeeded)
                return BadRequest("Invalid confirmation or expired token.");
            
            user.EmailConfirmationToken = null;

            var update = await userManager.UpdateAsync(user);

            return Ok("Email has been confirmed");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(UpdateUser updatedUser)
        {
            try
            {
                var identityUser = await userManager.FindByIdAsync(updatedUser.Id);

                if (updatedUser.FirstName != "" && identityUser.FirstName != updatedUser.FirstName)
                {
                    identityUser.FirstName = updatedUser.FirstName;
                }
                if(updatedUser.LastName != "" && identityUser.LastName != updatedUser.LastName)
                {
                    identityUser.LastName = updatedUser.LastName;
                }

                var result = await userManager.UpdateAsync(identityUser);

                var roles = await userManager.GetRolesAsync(identityUser);

                return Ok(new
                {
                    role = roles[0],
                    firstName = identityUser.FirstName,
                    lastName = identityUser.LastName,
                    userId = identityUser.Id.ToString(),
                    experiences = identityUser.Experiences
                });
            }
            catch
            {
                return BadRequest(updatedUser);
            }

        }

        [HttpPut("updatePassword")]
        public async Task<IActionResult> UpdatePassword(UpdatePassword updatedPassword)
        {
            try
            {
                var identityUser = await userManager.FindByIdAsync(updatedPassword.Id);

                if (updatedPassword.NewPassword != "" && await userManager.CheckPasswordAsync(identityUser, updatedPassword.NewPassword) == false)
                {
                    var res = await userManager.ChangePasswordAsync(identityUser, updatedPassword.CurrentPassword, updatedPassword.NewPassword);
                }
                var result = await userManager.UpdateAsync(identityUser);

                var roles = await userManager.GetRolesAsync(identityUser);

                return Ok(new
                {
                    role = roles[0],
                    firstName = identityUser.FirstName,
                    lastName = identityUser.LastName,
                    userId = identityUser.Id.ToString(),
                    experiences = identityUser.Experiences
                });
            }
            catch
            {
                return BadRequest(updatedPassword);
            }

        }

        [HttpPost("login")]
        public async Task<IActionResult> Authenticate(Login userInput)
        {
            var signInResult = await _signInManager.PasswordSignInAsync(userInput.Email, userInput.Password, false, false);

            var user = await userManager.FindByNameAsync(userInput.Email);

            if (signInResult.IsNotAllowed)
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
                userId = user.Id.ToString(),
                experiences = user.Experiences
            });
        }

    }
}