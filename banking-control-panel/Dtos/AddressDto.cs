namespace banking_control_panel.Dtos.UserDto;

public class ReadAddressDto
{
    public string Country { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public string ZipCode { get; set; }
}
public class CreateAddressDto
{
    public string Country { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public string ZipCode { get; set; }
}