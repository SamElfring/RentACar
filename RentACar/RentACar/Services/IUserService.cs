using RentACar.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentACar.Services
{
    public interface IUserService
    {
        public string GetUserId();
        public Task<ApplicationUser> GetUser();
        public Task<List<string>> GetUserRoles();
        public Task<bool> HasAccess();
        public ApplicationUser GetEmployees();
    }
}
