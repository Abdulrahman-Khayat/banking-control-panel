using banking_control_panel.Models;

namespace banking_control_panel.Data.SearchRepo;

public interface ISearchRepo: IRepository<Search>
{
    public Task<List<Search>> GetLastThree(Guid id);
}