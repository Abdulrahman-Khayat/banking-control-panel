using banking_control_panel.Models;

namespace banking_control_panel.Data.ClientRepo;

public class ClientRepo(AppDbContext context): BaseRepository<Client>(context), IClientRepo
{
}