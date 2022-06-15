using Microsoft.EntityFrameworkCore;
using RouletteApi.Models.Entities;

namespace RouletteApi.Repositories;

public class BetRepo : Repository<Bet>
{
    public BetRepo(RouletteDbContext rouletteDbContext) : base(rouletteDbContext)
    {
    }

    public async Task<List<Bet>> GetByRoundId(int roundId)
    {
        return await DbContext.Bets
            .Include(b => b.UserRound).Where(b => b.UserRound.RoundId == roundId)
            .ToListAsync();
    }

    public async Task<List<Bet>> GetByUserId(int userId)
    {
        return await DbContext.Bets
            .Include(b => b.UserRound).Where(b => b.UserRound.UserId == userId)
            .ToListAsync();
    }
}
