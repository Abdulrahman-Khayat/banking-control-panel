using System.ComponentModel.DataAnnotations;
using banking_control_panel.Models;

namespace banking_control_panel.Dtos.UserDto;

public class ReadUserDto
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Role { get; set; }
}
public class CreateUserDto
{
    public string Username { get; set; }
    public UserRoles Role { get; set; }
    public string Password { get; set; }
}

public class LoginDto
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}
public class PagedResultDto<T>
{
    public int TotalCount { get; set; }
    public int PageSize { get; set; }
    public int PageIndex { get; set; }
    public int TotalPages { get; set; }
    public int HasPreviousPage { get; set; }
    public int HasNextPage { get; set; }
    public IEnumerable<T> Items { get; set; }
}