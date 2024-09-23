using AutoMapper;
using banking_control_panel.Data;
using banking_control_panel.Dtos.UserDto;
using banking_control_panel.Models;

namespace banking_control_panel.Profiles;

public class UserProfile: Profile
{
    public UserProfile()
    {
        CreateMap<User, ReadUserDto>();
        CreateMap<PagedResult<User>, PagedResult<ReadUserDto>>();
        CreateMap<CreateUserDto, User>();
        CreateMap(typeof(PagedResult<>), typeof(PagedResultDto<>));
    }
}