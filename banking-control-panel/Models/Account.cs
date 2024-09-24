namespace banking_control_panel.Models;

public class Account: BaseModel
{
    public Guid Id { get; set; }
    public decimal Balance { get; set; } = 0;

    public Guid ClientId { get; set; }
    public Client Client { get; set; }
}