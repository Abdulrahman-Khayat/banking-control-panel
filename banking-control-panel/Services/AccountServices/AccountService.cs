using System.ComponentModel.DataAnnotations;
using AutoMapper;
using banking_control_panel.Data;
using banking_control_panel.Data.ClientRepo;
using banking_control_panel.Data.SearchRepo;
using banking_control_panel.Dtos.UserDto;
using banking_control_panel.Models;

namespace banking_control_panel.Services.AccountService;

public class AccountService(IAccountRepo _accountRepo, IClientRepo _clientRepo, IMapper _mapper): IAccountService
{
    public async Task<ReadAccountDto> AddAsync(CreateAccountDto createAccount)
    {
        if (createAccount.ClientId == null || await _clientRepo.GetByIdAsync((Guid)createAccount.ClientId) == null)
        {
            throw new ValidationException(Errors.INVALID_ACCOUNT);
        }
        
        var account = _mapper.Map<Account>(createAccount);
        var result = await _accountRepo.AddAsync(account);
        await _accountRepo.SaveChangesAsync();
        
        return _mapper.Map<ReadAccountDto>(result);
    }
}
