using System.Net;
using Microsoft.EntityFrameworkCore;

namespace DraftSimulator;

public class ApiDbContext : DbContext
{
    public virtual DbSet<DraftEntity> Drafts { get; set; } = null!;

    public virtual DbSet<DraftTeamEntity> DraftTeams { get; set; } = null!;
    
    public virtual DbSet<PlayerEntity> Players { get; set; } = null!;

    public virtual DbSet<TeamEntity> SportsTeams { get; set; } = null!;

    public virtual DbSet<PositionEntity> PlayerPositions { get; set; } = null!;

    public ApiDbContext(DbContextOptions<ApiDbContext> options)
        : base(options)
    {  

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<DraftTeamEntity>(entity => {
            entity.HasOne(d => d.Draft)
                .WithMany(d => d.DraftTeams)
                .HasForeignKey(d => d.DraftId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_DraftTeam_Draft");
        });

        modelBuilder.Entity<PlayerEntity>(entity => {
            entity.HasOne(p => p.DraftTeam)
                .WithMany(p => p.Players)
                .HasForeignKey(p => p.DraftTeamId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Player_DraftTeam");

            entity.HasOne(p => p.Position)
                .WithOne(p => p.Player)
                .HasForeignKey<PositionEntity>(x => x.PlayerId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Player_Position");

            entity.HasOne(p => p.Team)
                .WithOne(p => p.Player)
                .HasForeignKey<TeamEntity>(t => t.PlayerId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Player_SportsTeam");  
        });
    }
}
