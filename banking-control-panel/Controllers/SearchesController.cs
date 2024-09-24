using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.JavaScript;
using banking_control_panel.Data;
using banking_control_panel.Dtos.UserDto;
using banking_control_panel.Models;
using banking_control_panel.Services.ClientServices;
using banking_control_panel.Services.SearchService;
using Microsoft.AspNetCore.Mvc;

namespace banking_control_panel.Controllers;

[ApiController]
[Route("api/v1/searches")]
public class Searches(ISearchService _searchService, ILogger<Search> _logger): ControllerBase
{
    [HttpGet("{id}/last")]
    public async Task<ActionResult> GetLast(Guid id)
    {
        try
        {
            var result = await _searchService.GetByUserId(id);
            return Ok(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return Problem();
        }
    }
    
}