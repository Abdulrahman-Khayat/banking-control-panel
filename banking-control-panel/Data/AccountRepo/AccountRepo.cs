using banking_control_panel.Models;

namespace banking_control_panel.Data;

public class AccountRepo(AppDbContext context): BaseRepository<Account>(context), IAccountRepo
{
}