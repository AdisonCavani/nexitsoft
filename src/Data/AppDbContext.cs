﻿using Application.Data.Converters;
using Application.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Data;

public class AppDbContext : DbContext
{
    public required DbSet<CandidateEntity> Candidate { get; set; }
    public required DbSet<PreviousJobEntity> PreviousJob { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<CandidateEntity>()
            .HasKey(x => x.Id);

        builder.Entity<CandidateEntity>()
            .Property(x => x.FirstName)
            .HasMaxLength(255);

        builder.Entity<CandidateEntity>()
            .Property(x => x.LastName)
            .HasMaxLength(255);

        builder.Entity<CandidateEntity>()
            .Property(x => x.Email)
            .HasMaxLength(255);

        builder.Entity<CandidateEntity>()
            .HasMany(x => x.JobExperience)
            .WithOne()
            .HasForeignKey(x => x.CandidateId);

        builder.Entity<PreviousJobEntity>()
            .HasKey(x => x.Id);

        builder.Entity<PreviousJobEntity>()
            .Property(x => x.CompanyName)
            .HasMaxLength(255);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder builder)
    {
        builder.Properties<DateOnly>()
            .HaveConversion<DateOnlyConverter, DateOnlyComparer>();
    }
}