using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.Json;
using AutoMapper;
using banking_control_panel.Data;
using banking_control_panel.Data.ClientRepo;
using banking_control_panel.Data.SearchRepo;
using banking_control_panel.Dtos.UserDto;
using banking_control_panel.Models;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace banking_control_panel.Services.SearchService;

public class SearchService(ISearchRepo _searchRepo, IMapper _mapper): ISearchService
{
    public async Task<List<ReadSearchDto>> GetByUserId(Guid id)
    {
        var result = await _searchRepo.GetLastThree(id);
        
        return _mapper.Map<List<ReadSearchDto>>(result);
    }
    
}
