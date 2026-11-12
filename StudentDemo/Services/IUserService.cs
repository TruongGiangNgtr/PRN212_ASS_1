using StudentDemo.Models;

namespace StudentDemo.Services;

public interface IUserService
{
    IEnumerable<User> GetAll();
    User? GetById(int id);
    User? GetByUsername(string username);
    bool Add(User user, string password);
    bool Update(User user, string? newPassword = null);
    bool Delete(int id);
    User? ValidateCredentials(string username, string password);
}
