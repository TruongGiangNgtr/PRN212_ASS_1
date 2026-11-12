using Microsoft.AspNetCore.Identity;
using StudentDemo.Models;

namespace StudentDemo.Services;

public class InMemoryUserService : IUserService
{
    private readonly List<User> _users = new();
    private readonly PasswordHasher<User> _hasher = new();

    public InMemoryUserService()
    {
        Add(new User { Username = "admin", FullName = "Quản trị viên" }, "Admin@123");
    }

    public IEnumerable<User> GetAll() => _users;

    public User? GetById(int id) => _users.FirstOrDefault(u => u.Id == id);

    public User? GetByUsername(string username) =>
        _users.FirstOrDefault(u => string.Equals(u.Username, username, StringComparison.OrdinalIgnoreCase));

    public bool Add(User user, string password)
    {
        if (GetByUsername(user.Username) is not null) return false;

        user.Id = _users.Count == 0 ? 1 : _users.Max(u => u.Id) + 1;
        user.PasswordHash = _hasher.HashPassword(user, password);
        _users.Add(user);
        return true;
    }

    public bool Update(User user, string? newPassword = null)
    {
        var existing = GetById(user.Id);
        if (existing is null) return false;

        var sameName = GetByUsername(user.Username);
        if (sameName is not null && sameName.Id != existing.Id) return false;

        existing.Username = user.Username;
        existing.FullName = user.FullName;
        if (!string.IsNullOrEmpty(newPassword))
        {
            existing.PasswordHash = _hasher.HashPassword(existing, newPassword);
        }
        return true;
    }

    public bool Delete(int id)
    {
        var existing = GetById(id);
        if (existing is null) return false;

        _users.Remove(existing);
        return true;
    }

    public User? ValidateCredentials(string username, string password)
    {
        var user = GetByUsername(username);
        if (user is null) return null;

        var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, password);
        return result == PasswordVerificationResult.Failed ? null : user;
    }
}
