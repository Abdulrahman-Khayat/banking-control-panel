using AutoMapper;
using banking_control_panel.Data;
using banking_control_panel.Data.ClientRepo;
using banking_control_panel.Dtos.UserDto;
using banking_control_panel.Models;

namespace banking_control_panel.Services.ClientServices;

public class ClientService(IClientRepo _clientRepo, IMapper _mapper): IClientService
{
    public async Task<ReadClientDto> AddAsync(CreateClientDto createClient)
    {
        var client = new Client()
        {
            Email = "sdfsdfsdf",
            Firstname = createClient.Firstname,
            Lastname = "sdfsdfsdf",
            PersonalId = "sdfsdsdfds"
        };

        var result = await _clientRepo.AddAsync(client);
        await _clientRepo.SaveChangesAsync();
        return _mapper.Map<ReadClientDto>(client);
    }

    public Task<Client> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<PagedResultDto<ReadClientDto>> GetAll()
    {
        var clients = await _clientRepo.GetAllPagedAsync(1, 10);
        var result = _mapper.Map<PagedResultDto<ReadClientDto>>(clients);

        return result;
    }
}
