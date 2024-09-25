namespace banking_control_panel.Dtos.UserDto;

public class CreateAccountFromClientDto
{
    public decimal Balance { get; set; } = 0;
}
public class CreateAccountDto
{
    public Guid? ClientId { get; set; }
    public decimal Balance { get; set; } = 0;
}
public class ReadAccountDto
{
    public Guid Id { get; set; }
    public decimal Balance { get; set; } = 0;
}