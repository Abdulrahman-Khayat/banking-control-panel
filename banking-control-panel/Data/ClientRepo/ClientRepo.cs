using System.Linq.Dynamic.Core;
using System.Reflection;
using banking_control_panel.Dtos.UserDto;
using banking_control_panel.Models;
using Microsoft.EntityFrameworkCore;

namespace banking_control_panel.Data.ClientRepo;

public class ClientRepo(AppDbContext context): BaseRepository<Client>(context), IClientRepo
{
    public override async Task<PagedResult<Client>> GetAllPagedAsync(QueryDto query, int pageIndex, int pageSize)
    {
        var items = context.Set<Client>().AsQueryable();
        if (!string.IsNullOrWhiteSpace(query.Email))
        {
            items = items.Where(s => s.Email.Contains(query.Email));
        }
        if (!string.IsNullOrWhiteSpace(query.Firstname))
        {
            items = items.Where(s => s.Firstname.Contains(query.Firstname));
        }
        if (!string.IsNullOrWhiteSpace(query.Lastname))
        {
            items = items.Where(s => s.Lastname.Contains(query.Lastname));
        }
        if (!string.IsNullOrWhiteSpace(query.PersonalId))
        {
            items = items.Where(s => s.PersonalId.Contains(query.PersonalId));
        }
        if (!string.IsNullOrWhiteSpace(query.OrderBy))
        {
            // when using one bindingAttr, the other will be overwritten
            var propertyInfo = typeof(Client).GetProperty(query.OrderBy, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (propertyInfo != null)
            {
                items = items.OrderBy(query.OrderBy);
            }
        }
        
        var count = await items.CountAsync();
        
        items = items.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        return new PagedResult<Client>(items, count, pageIndex, pageSize);
    }
}