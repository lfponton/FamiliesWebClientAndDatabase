using Models;

namespace FamiliesManager.Data
{
    public interface IUserService
    {
        User ValidateUser(string username, string password);
    }
}