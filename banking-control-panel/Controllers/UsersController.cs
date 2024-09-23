using banking_control_panel.Dtos.UserDto;
using banking_control_panel.Services.UserServices;
using Microsoft.AspNetCore.Mvc;

namespace banking_control_panel.Controllers;

[ApiController]
[Route("api/v1/users")]
public class UsersController(IUserService _userService): ControllerBase
{
    [HttpGet("{id}", Name = "[controller]/GetById")]
    public async Task<ActionResult<ReadUserDto>> GetById(Guid id)
    {
        var result = await _userService.GetById(id);
        return Ok(result);
    }
    [HttpPost("register")]
    public async Task<ActionResult<ReadUserDto>> Create(CreateUserDto user)
    {
        var result = await _userService.RegisterAsync(user); 
        
        var route = this.ControllerContext.ActionDescriptor?.ControllerName + "/GetById";
        return CreatedAtRoute(route, new{result.Id}, result);
    }
    [HttpPost("login")]
    public async Task<ActionResult<string>> Login(LoginDto login)
    {
        var result = await _userService.LoginAsync(login); 
        return Ok(result);
    }
}
