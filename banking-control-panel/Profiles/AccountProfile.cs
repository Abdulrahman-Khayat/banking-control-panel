using AutoMapper;
using banking_control_panel.Data;
using banking_control_panel.Dtos.UserDto;
using banking_control_panel.Models;

namespace banking_control_panel.Profiles;

public class AccountProfile: Profile
{
    public AccountProfile()
    {
        CreateMap<Account, ReadAccountDto>();
        CreateMap<CreateAccountDto, Account>();
        CreateMap<CreateAccountFromClientDto, Account>();
    }
}