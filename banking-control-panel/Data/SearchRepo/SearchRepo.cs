using banking_control_panel.Models;
using Microsoft.EntityFrameworkCore;

namespace banking_control_panel.Data.SearchRepo;

public class SearchRepo(AppDbContext context): BaseRepository<Search>(context), ISearchRepo
{
    public async Task<List<Search>> GetLastThree(Guid id)
    {
        return await context.Set<Search>().Where(c => c.UserId == id).OrderByDescending(c => c.CreatedAt).Take(3).ToListAsync();
    }
}