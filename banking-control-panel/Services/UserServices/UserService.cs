using System.ComponentModel.DataAnnotations;
using AutoMapper;
using banking_control_panel.Data;
using banking_control_panel.Dtos.UserDto;
using banking_control_panel.Models;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using banking_control_panel.Exceptions;
using Microsoft.IdentityModel.Tokens;

namespace banking_control_panel.Services.UserServices;

public class UserService(IUserRepo _userRepo, PasswordHasher<User> _hasher, IConfiguration _configuration, IMapper _mapper): IUserService
{
    public async Task<ReadUserDto> RegisterAsync(CreateUserDto user)
    {
        if(!Enum.IsDefined(typeof(UserRoles), user.Role))
        {
            throw new ValidationException();
        }
        if (await _userRepo.GetFirstWhereAsync(c => c.Username == user.Username) != null)
        {
            throw new ValidationException(Errors.DUPLICATE_USERNAME);
        }
        User userObj = new()
        {
            Username = user.Username,
            Role = user.Role,
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

    public async Task<ReadUserDto> GetByIdAsync(Guid id)
    {
        var result = await _userRepo.GetByIdAsync(id);
        if (result == null)
        {
            throw new NotFoundException();
        }
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
