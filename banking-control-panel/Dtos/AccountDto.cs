namespace banking_control_panel.Dtos.UserDto;

public class CreateAccountDto
{
    public decimal Balance { get; set; } = 0;
}
public class ReadAccountDto
{
    public Guid Id { get; set; }
    public decimal Balance { get; set; } = 0;
}