using System.ComponentModel.DataAnnotations;

namespace banking_control_panel.Models;

public class Users
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }

    public string Role { get; set; }
}

public static class UserRoles
{
    public static string ADMIN = "admin";
    public static string USER = "user";
}