using banking_control_panel.Data;
using banking_control_panel.Dtos.UserDto;
using banking_control_panel.Models;

namespace banking_control_panel.Services.ClientServices;

public interface IClientService
{
    Task<ReadClientDto> AddAsync(CreateClientDto createClient);
    Task<ReadClientDto> GetByIdAsync(Guid id);
    Task<PagedResultDto<ReadClientDto>> GetAll(Guid userId, QueryDto query, int pageIndex = 1, int pageSize = 10);
}