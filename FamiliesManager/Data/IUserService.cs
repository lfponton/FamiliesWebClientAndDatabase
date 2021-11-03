using System.Threading.Tasks;
using Models;

namespace FamiliesManager.Data
{
    public interface IUserService
    {
        Task<User> ValidateUserAsync(string username, string password);
    }
}