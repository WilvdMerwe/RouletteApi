using Microsoft.EntityFrameworkCore;
using RouletteApi.Models.Entities;

namespace RouletteApi.Repositories;

public class RouletteDbContext : DbContext
{
    public RouletteDbContext(DbContextOptions options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Round> Rounds { get; set; }
    public DbSet<Bet> Bets { get; set; }
    public DbSet<UserRound> UserRounds { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserRound>()
            .HasMany(e => e.Bets)
            .WithOne(e => e.UserRound)
            .HasForeignKey(e => e.UserRoundId);

        modelBuilder.Entity<UserRound>()
            .HasOne(e => e.User)
            .WithMany(e => e.UserRounds)
            .HasForeignKey(e => e.UserId);

        modelBuilder.Entity<UserRound>()
            .HasOne(e => e.Round)
            .WithMany(e => e.UserRounds)
            .HasForeignKey(e => e.RoundId);

    }

}
