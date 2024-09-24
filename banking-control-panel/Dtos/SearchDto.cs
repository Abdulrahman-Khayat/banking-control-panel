using System.Text.Json;

namespace banking_control_panel.Dtos.UserDto;

public class ReadSearchDto
{
    public JsonDocument FilterParameters { get; set; }

}