using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.JavaScript;
using System.Security.Claims;
using banking_control_panel.Data;
using banking_control_panel.Dtos.UserDto;
using banking_control_panel.Exceptions;
using banking_control_panel.Models;
using banking_control_panel.Services.ClientServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;

namespace banking_control_panel.Controllers;

[ApiController]
[Route("api/v1/clients")]
public class ClientsController(IClientService _clientService, ILogger<Client> _logger): ControllerBase
{
    [HttpGet("{id}", Name = "[controller]/GetById")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ReadUserDto>> GetById(Guid id)
    {
        try
        {
            var result = await _clientService.GetByIdAsync(id);
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
    
    [HttpGet()]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<PagedResultDto<ReadClientDto>>> GetAll([FromQuery] QueryDto query, [FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
    {
        try
        {
            var userId = HttpContext.User.FindFirst(JwtRegisteredClaimNames.Sub).Value;
            var result = await _clientService.GetAll(new Guid(userId), query, pageIndex, pageSize);

            return Ok(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return Problem();
        }
    }
    
    [HttpPost()]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ReadClientDto>> Create(CreateClientDto client)
    {
        try
        {
            var result = await _clientService.AddAsync(client);
            var route = this.ControllerContext.ActionDescriptor?.ControllerName + "/GetById";
            return CreatedAtRoute(route, new { result.Id }, result);
        }
        catch (ValidationException e)
        {
            if (e.Message == Errors.INVALID_SEX)
            {
                ModelState.AddModelError("Sex", "Invalid sex type [0: male, 1: female]");
                _logger.LogInformation("Invalid sex type [0: male, 1: female]");

            }
            else if (e.Message == Errors.DUPLICATE_EMAIL)
            {
                ModelState.AddModelError("Email", "Email already exists");
                _logger.LogInformation("Email already exists");

            }
            else if (e.Message == Errors.DUPLICATE_MOBILE)
            {
                ModelState.AddModelError("MobileNumber", "MobileNumber already exists");
                _logger.LogInformation("MobileNumber already exists");
            }

            return ValidationProblem();
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return Problem();
        }
        
    }
    
    
}