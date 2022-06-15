using Microsoft.EntityFrameworkCore;
using RouletteApi.Enums;
using RouletteApi.Models.Entities;

namespace RouletteApi.Repositories;

public class RoundRepo : Repository<Round>
{
    public RoundRepo(RouletteDbContext rouletteDbContext) : base(rouletteDbContext)
    {
    }

    public async Task<Round> GetOpenRound()
    {
        return await FindBy(r => r.Status == RoundStatus.Open)
            .Include(r => r.UserRounds)
            .ThenInclude(ur => ur.Bets)
            .FirstOrDefaultAsync();
    }

    public async Task<bool> IsAnyOpen()
    {
        return await FindBy(r => r.Status == RoundStatus.Open).AnyAsync();
    }
}
