using Microsoft.EntityFrameworkCore;
using RouletteApi.Repositories;

namespace RouletteApi.Tests;

public static class Options
{
    public static DbContextOptions<RouletteDbContext> MockDbContextOptions => new DbContextOptionsBuilder<RouletteDbContext>()
        .UseInMemoryDatabase(databaseName: "RouletteMockDb")
        .Options;
}
