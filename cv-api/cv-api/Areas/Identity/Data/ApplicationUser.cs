using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Identity.MongoDbCore.Models;
using cv_api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace cv_api.Areas.Identity.Data
{
	// Add profile data for application users by adding properties to the ApplicationUser class
	public class ApplicationUser : MongoIdentityUser<Guid>
	{
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public string EmailConfirmationToken { get; set; }
		public string ResetPasswordToken { get; set; }

		public CV CV { get; set; }

		public List<Experience>? Experiences { get; set; }

		public List<Presentation>? Presentations { get; set; }

		public ApplicationUser() : base()
		{
		}

		public ApplicationUser(string userName, string email) : base(userName, email)
		{
		}
	}

	public class ApplicationRole : MongoIdentityRole<Guid>
	{
		public ApplicationRole() : base()
		{
		}

		public ApplicationRole(string roleName) : base(roleName)
		{
		}
	}
}
