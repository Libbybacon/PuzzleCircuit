using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PuzzleCircuit.DAL.Entities;
using PuzzleCircuit.DAL.Entities.Admin;

namespace PuzzleCircuit.DAL;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<AppUser>(options) {
    #region Admin
    public DbSet<AppUser> AppUsers { get; set; }
    DbSet<HostOrganization> Hosts { get; set; }
    DbSet<HostLicense> HostLicenses { get; set; }
    DbSet<Competition> Competitions { get; set; }
    DbSet<CompetitionEvent> CompetitionEvents { get; set; }
    DbSet<RegistrationGroup> RegistrationGroups { get; set; }
    DbSet<EventRegistration> EventRegistrations { get; set; }
    DbSet<EventResult> EventResults { get; set; }
    DbSet<Puzzle> Puzzles { get; set; }
    DbSet<PuzzleResult> PuzzleResults { get; set; }
    #endregion

    protected override void OnModelCreating(ModelBuilder builder) {
        base.OnModelCreating(builder);

        // =========================
        // ApplicationUser
        // =========================
        builder.Entity<AppUser>(e => {
            e.ToTable("AppUsers");

            e.Property(x => x.DisplayName)
                .HasMaxLength(200)
                .IsRequired();

            e.Property(x => x.IsActive)
                .IsRequired();
        });

        // =========================
        // HostOrganization
        // =========================
        builder.Entity<HostOrganization>(e => {
            e.ToTable("HostOrganizations");

            e.HasKey(x => x.Id);

            e.Property(x => x.Name)
             .HasMaxLength(200)
             .IsRequired();

            e.HasOne(x => x.AdminUser)
             .WithMany(x => x.HostOrganizations)
             .HasForeignKey(x => x.AdminUserId)
             .OnDelete(DeleteBehavior.Restrict);
        });

        // =========================
        // HostLicense
        // =========================
        builder.Entity<HostLicense>(e => {
            e.ToTable("HostLicenses");

            e.HasKey(x => x.Id);

            e.Property(x => x.LicenseKey)
             .HasMaxLength(200)
             .IsRequired();

            e.Property(x => x.ExpirationUtc)
             .IsRequired();

            e.HasOne(x => x.HostOrganization)
             .WithMany(x => x.Licenses)
             .HasForeignKey(x => x.HostId)
             .OnDelete(DeleteBehavior.Cascade);
        });

        // =========================
        // Competition
        // =========================
        builder.Entity<Competition>(e => {
            e.ToTable("Competitions");

            e.HasKey(x => x.Id);

            e.Property(x => x.Name)
             .HasMaxLength(200)
             .IsRequired();

            e.Property(x => x.StartUtc).IsRequired();
            e.Property(x => x.EndUtc).IsRequired();

            e.HasOne(x => x.HostOrganization)
             .WithMany(x => x.Competitions)
             .HasForeignKey(x => x.HostOrganizationId)
             .OnDelete(DeleteBehavior.Cascade);
        });

        // =========================
        // CompetitionEvent
        // =========================
        builder.Entity<CompetitionEvent>(e => {
            e.ToTable("CompetitionEvents");

            e.HasKey(x => x.Id);

            e.Property(x => x.Name)
             .HasMaxLength(200)
             .IsRequired();

            e.Property(x => x.ParticipantType)
             .IsRequired();

            e.Property(x => x.MaxEntries)
             .IsRequired();

            e.HasOne(x => x.Competition)
             .WithMany(x => x.Events)
             .HasForeignKey(x => x.CompetitionId)
             .OnDelete(DeleteBehavior.Cascade);
        });

        // =========================
        // RegistrationGroup
        // =========================
        builder.Entity<RegistrationGroup>(e => {
            e.ToTable("RegistrationGroups");

            e.HasKey(x => x.Id);

            e.Property(x => x.DisplayName)
                .HasMaxLength(200);

            e.HasOne(x => x.Event)
             .WithMany(x => x.RegistrationGroups)
             .HasForeignKey(x => x.EventId)
             .OnDelete(DeleteBehavior.NoAction);
        });

        // =========================
        // EventRegistration
        // =========================
        builder.Entity<EventRegistration>(e => {
            e.ToTable("EventRegistrations");

            e.HasKey(x => x.Id);

            e.Property(x => x.RegisteredUtc)
             .IsRequired();

            e.HasOne(x => x.User)
             .WithMany(x => x.EventRegistrations)
             .HasForeignKey(x => x.UserId)
             .OnDelete(DeleteBehavior.Restrict);

            e.HasOne(x => x.RegistrationGroup)
             .WithMany(x => x.Registrations)
             .HasForeignKey(x => x.RegistrationGroupId)
             .OnDelete(DeleteBehavior.Cascade);

            e.HasIndex(x => new { x.UserId, x.EventId })
             .IsUnique(); // prevent duplicate registration per division
        });

        // =========================
        // EventResult
        // =========================
        builder.Entity<EventResult>(e => {
            e.ToTable("EventResults");

            e.HasKey(x => x.Id);

            e.HasOne(x => x.RegistrationGroup)
             .WithMany(x => x.Results)
             .HasForeignKey(x => x.RegistrationGroupId)
             .OnDelete(DeleteBehavior.Cascade);

            e.HasOne(x => x.Puzzle)
             .WithMany()
             .HasForeignKey(x => x.PuzzleId)
             .OnDelete(DeleteBehavior.Restrict);

            e.HasOne(x => x.Scorekeeper)
             .WithMany(x => x.ScorekeepingResults)
             .HasForeignKey(x => x.ScorekeeperId)
             .OnDelete(DeleteBehavior.Restrict);
        });

        // =========================
        // Puzzle
        // =========================
        builder.Entity<Puzzle>(e => {
            e.ToTable("Puzzles");

            e.HasKey(x => x.Id);

            e.Property(x => x.Title)
             .HasMaxLength(200);

            e.Property(x => x.Description)
             .HasMaxLength(200)
             .IsRequired();

            e.Property(x => x.PieceCount)
             .IsRequired();

            e.HasOne(x => x.PuzzleCompany)
             .WithMany(x => x.Puzzles)
             .HasForeignKey(x => x.PuzzleCompanyId);
        });

        // =========================
        // PuzzleCompany
        // =========================
        builder.Entity<PuzzleCompany>(e => {
            e.ToTable("PuzzleCompanies");

            e.HasKey(x => x.Id);

            e.Property(x => x.Name)
             .HasMaxLength(200);

            e.Property(x => x.Description)
             .HasMaxLength(200)
             .IsRequired();
        });

        // =========================
        // PuzzleResult
        // =========================
        builder.Entity<PuzzleResult>(e => {
            e.ToTable("PuzzleResults");

            e.HasKey(x => x.Id);

            e.Property(x => x.DateCompleted)
             .IsRequired();

            e.HasOne(x => x.User)
             .WithMany(x => x.PuzzleResults)
             .HasForeignKey(x => x.UserId)
             .OnDelete(DeleteBehavior.Cascade);

            e.HasOne(x => x.Puzzle)
             .WithMany()
             .HasForeignKey(x => x.PuzzleId)
             .OnDelete(DeleteBehavior.Restrict);
        });
    }
}


