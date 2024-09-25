using System.ComponentModel.DataAnnotations;
using banking_control_panel.Data;
using banking_control_panel.Dtos.UserDto;
using banking_control_panel.Models;
using banking_control_panel.Services.AccountService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace banking_control_panel.Controllers;

[ApiController]
[Route("api/v1/accounts")]
public class AccountsController(IAccountService _accountService, ILogger<Account> _logger): ControllerBase
{
    [HttpPost()]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ReadAccountDto>> Create(CreateAccountDto client)
    {
        try
        {
            var result = await _accountService.AddAsync(client);
            return Ok(result);
        }
        catch (ValidationException e)
        {
            if (e.Message == Errors.INVALID_ACCOUNT)
            {
                ModelState.AddModelError("ClientId", "ClientId does not exist");
                _logger.LogInformation("ClientId does not exist");
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