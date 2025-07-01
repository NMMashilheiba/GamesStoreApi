using System;
using GameStore.api.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.api.Data;

public class GameStoreContext(DbContextOptions<GameStoreContext> options) : DbContext(options)
{
    public DbSet<Game> Games => Set<Game>();
    public DbSet<Genre> Genres => Set<Genre>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Genre>().HasData(
            new {Id = 1, name = "Racing"},
            new {Id = 2, name = "Fighting"},
            new {Id = 3, name = "Simulation"},
            new {Id = 4, name = "Roleplaying"},
            new {Id = 5, name = "Sports"}
        );
    }
}
