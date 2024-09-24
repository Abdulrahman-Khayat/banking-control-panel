using AutoMapper;
using banking_control_panel.Data;
using banking_control_panel.Dtos.UserDto;
using banking_control_panel.Models;

namespace banking_control_panel.Profiles;

public class AddressProfile: Profile
{
    public AddressProfile()
    {
        CreateMap<Address, ReadAddressDto>();
        // CreateMap<PagedResult<Address>, PagedResult<ReadAddressDto>>();
        CreateMap<CreateAddressDto, Address>();
    }
}