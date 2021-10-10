using Models;

namespace FamilyManager.Data
{
    public interface IUserService
    {
        User ValidateUser(string username, string password);
    }
}