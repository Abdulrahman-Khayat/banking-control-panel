using System.ComponentModel.DataAnnotations;

namespace banking_control_panel.Dtos.UserDto;

public class ReadClientDto
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    
}
public class CreateClientDto
{
    [EmailAddress]
    public string Email { get; set; }
    // [Length(0, 60)]
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    [Length(11, 11, ErrorMessage = "Length must be exactly 11 characters")]
    public string PersonalId { get; set; }
    [RegularExpression(@"^(\+[1-9]{1,2}\s?)?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$", ErrorMessage = "Invalid Phone Number format")]
    public string MobileNumber { get; set; }
}