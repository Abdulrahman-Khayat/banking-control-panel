using banking_control_panel.Data;
using banking_control_panel.Dtos.UserDto;
using banking_control_panel.Models;

namespace banking_control_panel.Services.SearchService;

public interface ISearchService
{
    public Task<List<ReadSearchDto>> GetByUserId(Guid id);
}