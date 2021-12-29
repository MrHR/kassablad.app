using System;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Options;
using Duende.IdentityServer.EntityFramework.Options;
using kassablad.app.Server.Models;
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

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder
            .Entity<Nomination>().OwnsOne(nomination => nomination.Nom);
        
        base.OnModelCreating(builder);
    }
}
