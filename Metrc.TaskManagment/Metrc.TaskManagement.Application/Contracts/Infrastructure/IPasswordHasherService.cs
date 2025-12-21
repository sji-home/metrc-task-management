namespace Metrc.TaskManagement.Application.Contracts.Infrastructure;

public interface IPasswordHasherService
{
    string HashPassword(string password);
    bool PasswordMatches(string providedPassword, string passwordHash);
}
