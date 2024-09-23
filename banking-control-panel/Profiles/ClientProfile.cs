using AutoMapper;
using banking_control_panel.Data;
using banking_control_panel.Dtos.UserDto;
using banking_control_panel.Models;

namespace banking_control_panel.Profiles;

public class ClientProfile: Profile
{
    public ClientProfile()
    {
        CreateMap<Client, ReadClientDto>();
        CreateMap<PagedResult<Client>, PagedResult<ReadClientDto>>();
        CreateMap<CreateClientDto, Client>();
        CreateMap(typeof(PagedResult<>), typeof(PagedResultDto<>));
    }
}