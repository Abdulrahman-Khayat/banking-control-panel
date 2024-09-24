using AutoMapper;
using banking_control_panel.Dtos.UserDto;
using banking_control_panel.Models;

namespace banking_control_panel.Profiles;

public class SearchProfile: Profile
{
    public SearchProfile()
    {
        CreateMap<Search, ReadSearchDto>();
    }
}