using System.ComponentModel.DataAnnotations;
using banking_control_panel.Data;
using banking_control_panel.Dtos.UserDto;
using banking_control_panel.Exceptions;
using banking_control_panel.Models;
using banking_control_panel.Services.UserServices;
using Microsoft.AspNetCore.Mvc;

namespace banking_control_panel.Controllers;

[ApiController]
[Route("api/v1/users")]
public class UsersController(IUserService _userService, ILogger<User> _logger): ControllerBase
{
    [HttpGet("{id}", Name = "[controller]/GetById")]
    public async Task<ActionResult<ReadUserDto>> GetById(Guid id)
    {
        try
        {
            var result = await _userService.GetByIdAsync(id);
            return Ok(result);
        }
        catch (NotFoundException e)
        {
            _logger.LogInformation("Not Found");
            return NotFound();
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return Problem();
        }
    }
    [HttpPost("register")]
    public async Task<ActionResult<ReadUserDto>> Create(CreateUserDto user)
    {
        try
        {
            var result = await _userService.RegisterAsync(user);
            var route = this.ControllerContext.ActionDescriptor?.ControllerName + "/GetById";
            return CreatedAtRoute(route, new { result.Id }, result);
        }
        catch (ValidationException e)
        {
            if (e.Message == Errors.INVALID_ROLE)
            {
                ModelState.AddModelError("role", "Invalid role type [0: admin, 1: user]");
                _logger.LogInformation("Invalid role type [0: admin, 1: user]");
            }
            else if (e.Message == Errors.DUPLICATE_USERNAME)
            {
                ModelState.AddModelError("username", "Username already exists");
                _logger.LogInformation("Username already exists");
            }
            return ValidationProblem();
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return Problem();
        }
        
    }
    [HttpPost("login")]
    public async Task<ActionResult<string>> Login(LoginDto login)
    {
        try
        {
            var result = await _userService.LoginAsync(login);
            return Ok(result);
        }
        catch (UnauthorizedAccessException e)
        {
            _logger.LogInformation("Unauthorized");
            return Unauthorized();
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return Problem();
        }
        
    }
}
