using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class HabitDbContext : DbContext
{
    public DbSet<Habit> Habits { set; get; }
    public DbSet<HabitCheck> HabitChecks { set; get; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseSqlite("Filename=habits.db");
}