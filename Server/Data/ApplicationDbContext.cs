using System;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Options;
using Duende.IdentityServer.EntityFramework.Options;
using kassablad.app.Shared.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace kassablad.app.Server.Data;

public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
{
    public ApplicationDbContext(
        DbContextOptions options,
        IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
    {
    }
    public DbSet<KassaContainer>? KassaContainers { get; set; }
    public DbSet<Kassa>? Kassas { get; set; }
    public DbSet<Consumptie>? Consumpties { get; set; }
    public DbSet<ConsumptieCount>? ConsumptieCounts { get; set; }
    public DbSet<Nomination>? Nominations { get; set; }
    public DbSet<KassaNomination>? KassaNominations { get; set; }
    public DbSet<KassaTemplate>? KassaTemplates { get; set; }
    public DbSet<Tapper>? Tappers { get; set; }
    public DbSet<KassaContainerApplicationUser>? KassaContainerApplicationUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // Ownership of keyless value object definition -> nomination.cs
        builder.Entity<Nomination>()
            .OwnsOne(nomination => nomination.Nom);

        // Ownership of keyless value object definition -> KassaNomination.cs
        builder.Entity<KassaNomination>()
            .OwnsOne(nomination => nomination.Nom);
        
        // If following id conventions 
        builder.Entity<ApplicationUser>()
            .HasMany(k => k.KassaContainers)
            .WithMany(a => a.ApplicationUsers)
            .UsingEntity(j => j.ToTable("KassaContainerApplicationUsers"));

        //KassaNominationTotal
        builder.Entity<KassaNomination>()
            .Property(kn => kn.Total)
            .HasComputedColumnSql("[Nom_Multiplier] + [Amount]", stored: true);

        // many-to-many relationship between kassaContainers and ApplicationUsers
        //Because there is more data saved in the KassaContainerApplicationTable we define it here explicitly
        // more info: https://docs.microsoft.com/en-us/ef/core/modeling/relationships?tabs=fluent-api%2Cfluent-api-simple-key%2Csimple-key#other-relationship-patterns
        builder.Entity<ApplicationUser>()
            .HasMany(k => k.KassaContainers)
            .WithMany(a => a.ApplicationUsers)
            .UsingEntity<KassaContainerApplicationUser>(
                j => j
                    .HasOne(ka => ka.KassaContainer)
                    .WithMany(k => k.KassaContainerApplicationUsers)
                    .HasForeignKey(ka => ka.KassaContainerId)
                    .HasConstraintName("FK_KassaContainerApplicationUsers_KassaContainers_KassaContainerId")
                    .OnDelete(DeleteBehavior.ClientCascade),
                j => j
                    .HasOne(ka => ka.ApplicationUser)
                    .WithMany(a => a.KassaContainerApplicationUsers)
                    .HasForeignKey(ka => ka.ApplicationUserId)
                    .HasConstraintName("FK_KassaContainerApplicationUsers_ApplicationUsers_Id")
                    .OnDelete(DeleteBehavior.Cascade),
                j =>
                {
                    j.Property(ka => ka.StartTime).HasDefaultValueSql("CURRENT_TIMESTAMP");
                    j.HasKey(k => new { k.ApplicationUserId, k.KassaContainerId });
                }
            );

        base.OnModelCreating(builder);
    }
}
