using System.ComponentModel.DataAnnotations;

namespace banking_control_panel.Models;

public class Client: BaseModel
{
    public Guid Id { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    [MaxLength(60)]
    public string Firstname { get; set; }
    [MaxLength(60)]
    public string Lastname { get; set; }
    [StringLength(11)]
    public string PersonalId { get; set; }
    public string ProfilePhoto { get; set; }
    [RegularExpression(@"^(\+[1-9]{1,2}\s?)?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$")]
    public string MobileNumber { get; set; }
    public SexTypes Sex { get; set; }
    public Guid AddressId { get; set; }
    public Address Address { get; set; }
    [MinLength(1)]
    public IList<Account> Accounts { get; set; } = new List<Account>();
}

public enum SexTypes
{
    Male,
    Female
}