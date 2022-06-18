using Microsoft.EntityFrameworkCore;
using RouletteApi.Models.Entities;

namespace RouletteApi.Repositories;

public class UserRepo : Repository<User>
{
    public UserRepo(RouletteDbContext rouletteDbContext) : base(rouletteDbContext)
    {
    }

    public async Task<bool> DoesEmailExist(string email)
    {
        return await DbContext.Users.AnyAsync(user => user.Email == email);
    }
}
