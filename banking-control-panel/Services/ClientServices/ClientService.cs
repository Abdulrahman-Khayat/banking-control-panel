using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.Json;
using AutoMapper;
using banking_control_panel.Data;
using banking_control_panel.Data.ClientRepo;
using banking_control_panel.Data.SearchRepo;
using banking_control_panel.Dtos.UserDto;
using banking_control_panel.Exceptions;
using banking_control_panel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace banking_control_panel.Services.ClientServices;

public class ClientService(IUnitOfWork _unitOfWork, IClientRepo _clientRepo, ISearchRepo _searchRepo, IMapper _mapper): IClientService
{
    public async Task<ReadClientDto> AddAsync(CreateClientDto createClient)
    {
        if(!Enum.IsDefined(typeof(SexTypes), createClient.Sex))
        {
            throw new ValidationException(Errors.INVALID_SEX);
        }
        
        if (await _clientRepo.GetFirstWhereAsync(c => c.Email == createClient.Email) != null)
        {
            throw new ValidationException(Errors.DUPLICATE_EMAIL);
        }
        if (await _clientRepo.GetFirstWhereAsync(c => c.MobileNumber == createClient.MobileNumber) != null)
        {
            throw new ValidationException(Errors.DUPLICATE_MOBILE);
        }
        var client = _mapper.Map<Client>(createClient);
        
        // if no account is provided, create one
        if (createClient.Accounts == null)
        {
            client.Accounts.Add(new Account());
        }
        var result = await _unitOfWork.Repository<Client>().AddAsync(client);
        await _unitOfWork.CompleteAsync();
        return _mapper.Map<ReadClientDto>(result);
    }

    public async Task<ReadClientDto> GetByIdAsync(Guid id)
    {
        var result = await _clientRepo.GetByIdAsync(id);
        if (result == null)
        {
            throw new NotFoundException();
        }
        return _mapper.Map<ReadClientDto>(result);
    }
    public async Task<PagedResultDto<ReadClientDto>> GetAll(Guid userId, QueryDto queryDto, int pageIndex = 1, int pageSize = 10)
    {
        Dictionary<string, string> d = new Dictionary<string, string>();
        foreach (PropertyInfo prop in queryDto.GetType().GetProperties())
        {
            if (!string.IsNullOrWhiteSpace(prop.GetValue(queryDto)?.ToString()))
            {
                d[prop.Name] = prop.GetValue(queryDto).ToString();
            }
        }
        
        if (d.Count > 0)
        {
            string json1 = JsonConvert.SerializeObject(d, Formatting.Indented);

            // Parse the JSON string into a JsonDocument
            JsonDocument jsonDocument = JsonDocument.Parse(json1);
            var searches = new Search()
            {
                UserId = userId,
                FilterParameters = jsonDocument
            };
            await _searchRepo.AddAsync(searches);
            await _searchRepo.SaveChangesAsync();
        }
        var clients = await _clientRepo.GetAllPagedAsync(queryDto, pageIndex, pageSize);
        var result = _mapper.Map<PagedResultDto<ReadClientDto>>(clients);

        return result;
    }
}
