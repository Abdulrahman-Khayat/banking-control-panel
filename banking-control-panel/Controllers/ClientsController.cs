using banking_control_panel.Dtos.UserDto;
using banking_control_panel.Services.ClientServices;
using Microsoft.AspNetCore.Mvc;

namespace banking_control_panel.Controllers;

[ApiController]
[Route("api/v1/clients")]
public class ClientsController(IClientService _clientService): ControllerBase
{
    [HttpGet("{id}", Name = "[controller]/GetById")]
    public async Task<ActionResult<ReadUserDto>> GetById(Guid id)
    {
        var result = await _clientService.GetByIdAsync(id);
        return Ok(result);
    }
    [HttpGet()]
    public async Task<ActionResult<PagedResultDto<ReadClientDto>>> GetAll()
    {
        var result = await _clientService.GetAll();

        return Ok(result);
    }
    
    [HttpPost()]
    public async Task<ActionResult<ReadClientDto>> Create(CreateClientDto client)
    {
        var result = await _clientService.AddAsync(client); 
        
        var route = this.ControllerContext.ActionDescriptor?.ControllerName + "/GetById";
        return CreatedAtRoute(route, new{result.Id}, result);
    }
}