using banking_control_panel.Models;

namespace banking_control_panel.Data;

public class UserRepo(AppDbContext context): BaseRepository<User>(context), IUserRepo
{
}