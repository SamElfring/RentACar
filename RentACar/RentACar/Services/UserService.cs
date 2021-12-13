using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using RentACar.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RentACar.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(IHttpContextAccessor httpContextAccessor,
            UserManager<ApplicationUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public string GetUserId()
        {
            return _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public async Task<ApplicationUser> GetUser()
        {
            string userId = this.GetUserId();
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task<List<string>> GetUserRoles()
        {
            string userId = this.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            return await _userManager.GetRolesAsync(user) as List<string>;
        }

        public async Task<bool> HasAccess()
        {
            List<string> roles = await this.GetUserRoles();

            bool hasAccess = false;
            foreach (string role in roles)
            {
                if (role == "Admin" || role == "Employee")
                {
                    hasAccess = true;
                    break;
                }
            }

            return hasAccess;
        }

        public ApplicationUser GetEmployees()
        {
            return _userManager.GetUsersInRoleAsync("Employee").Result[0];
        }

        public async Task<bool> IsAdmin()
        {
            if (!_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                return false;

            return await _userManager.IsInRoleAsync(await this.GetUser(), "Admin");
        }
    }
}
