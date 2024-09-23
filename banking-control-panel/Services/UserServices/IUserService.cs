using banking_control_panel.Dtos.UserDto;
using banking_control_panel.Models;

namespace banking_control_panel.Services.UserServices;

public interface IUserService
{
    Task<ReadUserDto> RegisterAsync(CreateUserDto user);
    Task<string> LoginAsync(LoginDto login);
    Task<ReadUserDto> GetById(Guid id);
}