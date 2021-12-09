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
        private readonly ApplicationDbContext _db;

        public UserService(IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager, ApplicationDbContext db)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _db = db;
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

        public ApplicationUser GetEmployees()
        {
            return _userManager.GetUsersInRoleAsync("Employee").Result[0];
        }
    }
}
