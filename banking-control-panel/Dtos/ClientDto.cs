using System.ComponentModel.DataAnnotations;
using banking_control_panel.Models;

namespace banking_control_panel.Dtos.UserDto;

public class ReadClientDto
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string PersonalId { get; set; }
    public string? ProfilePhoto { get; set; }
    [RegularExpression(@"^(\+[1-9]{1,2}\s?)?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$")]
    public string MobileNumber { get; set; }
    public string Sex { get; set; }
    public ReadAddressDto Address { get; set; }
    public IList<ReadAccountDto> Accounts { get; set; }
    
}
public class CreateClientDto
{
    [EmailAddress]
    public string Email { get; set; }
    [MaxLength(60)]
    public string Firstname { get; set; }
    [MaxLength(60)]
    public string Lastname { get; set; }
    [Length(11, 11)]
    public string PersonalId { get; set; }
    public string? ProfilePhoto { get; set; }
    [RegularExpression(@"^(\+[1-9]{1,2}\s?)?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$")]
    public string MobileNumber { get; set; }
    public SexTypes Sex { get; set; }
    public CreateAddressDto Address { get; set; }
    public IList<CreateAccountDto>? Accounts { get; set; }
}

public class QueryDto
{
    public string? Email { get; set; }
    [MaxLength(60)]
    public string? Firstname { get; set; }
    [MaxLength(60)]
    public string? Lastname { get; set; }
    [Length(0, 11)]
    public string? PersonalId { get; set; }
    public string? MobileNumber { get; set; }

    public string? OrderBy { get; set; }
    public SexTypes?  Sex { get; set; }
}
