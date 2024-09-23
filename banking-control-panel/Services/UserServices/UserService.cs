using System.ComponentModel.DataAnnotations;
using AutoMapper;
using banking_control_panel.Data;
using banking_control_panel.Dtos.UserDto;
using banking_control_panel.Models;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace banking_control_panel.Services.UserServices;

public class UserService(IUserRepo _userRepo, PasswordHasher<User> _hasher, IConfiguration _configuration, IMapper _mapper): IUserService
{
    public async Task<ReadUserDto> RegisterAsync(CreateUserDto user)
    {
        if(!Enum.TryParse(user.Role, true, out UserRoles role))
        {
            throw new ValidationException();
        }

        User userObj = new()
        {
            Username = user.Username,
            Role = role,
        };
        userObj.Password = _hasher.HashPassword(userObj, user.Password);

        var result = await _userRepo.AddAsync(userObj);
        await _userRepo.SaveChangesAsync();
        return _mapper.Map<ReadUserDto>(result);
    }

    public async Task<string> LoginAsync(LoginDto login)
    {
        var user = await _userRepo.GetFirstWhereAsync(m => m.Username == login.Username);
        if (user != null)
        {
            var verified = _hasher.VerifyHashedPassword(user, user.Password, login.Password);
            if (verified == PasswordVerificationResult.Success)
            {
                var claims = new List<Claim>();
                claims.AddRange([
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.PreferredUsername, user.Username),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                ]);

                return GenerateJwtToken(claims);
            }
        }
        throw new UnauthorizedAccessException();
    }

    public async Task<ReadUserDto> GetById(Guid id)
    {
        var result = await _userRepo.GetByIdAsync(id);
        return _mapper.Map<ReadUserDto>(result);
    }

    private string GenerateJwtToken(List<Claim> claims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            
        var token = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddHours(24),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
