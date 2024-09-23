using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace banking_control_panel.Models;

public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }

    public UserRoles Role { get; set; }
}

public enum UserRoles
{
    Admin,
    User
}