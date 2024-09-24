using System.Text.Json;

namespace banking_control_panel.Models;

public class Search: BaseModel
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    // Store filter parameters as JSON
    public JsonDocument FilterParameters { get; set; }
    
}