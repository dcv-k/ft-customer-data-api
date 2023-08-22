public interface IAuthService
{
    Task<string> CreateUser();

    Task<bool> ValidateUser(LoginUserDTO dto);
}