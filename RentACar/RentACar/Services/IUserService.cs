using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentACar.Services
{
    public interface IUserService
    {
        public string GetUserId();
        public Task<List<string>> GetUserRoles();
    }
}
