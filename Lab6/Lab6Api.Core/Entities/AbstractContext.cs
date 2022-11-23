using Lab6Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab6Api.Core.Entities
{
    public abstract class AbstractContext<TContext> : DbContext, IContext where TContext : DbContext
    {
/*        public const int DefaultMaxStringLength = 4000;

        public const int MaxPackageIdLength = 128;
        public const int MaxPackageVersionLength = 64;
        public const int MaxPackageMinClientVersionLength = 44;
        public const int MaxPackageLanguageLength = 20;
        public const int MaxPackageTitleLength = 256;
        public const int MaxPackageTypeNameLength = 512;
        public const int MaxPackageTypeVersionLength = 64;
        public const int MaxRepositoryTypeLength = 100;
        public const int MaxTargetFrameworkLength = 256;

        public const int MaxPackageDependencyVersionRangeLength = 256;*/

        public AbstractContext(DbContextOptions<TContext> options)
            : base(options)
        { }

        public DbSet<Location> Locations { get; set; }


        public Task<int> SaveChangesAsync() => SaveChangesAsync(default);

        public virtual async Task RunMigrationsAsync(CancellationToken cancellationToken)
            => await Database.MigrateAsync(cancellationToken);

        public abstract bool IsUniqueConstraintViolationException(DbUpdateException exception);

        public virtual bool SupportsLimitInSubqueries => true;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Location>(BuildLocationEntity);
        }

        private void BuildLocationEntity(EntityTypeBuilder<Location> location)
        {
            location.HasKey(p => p.LocId);
            location.HasIndex(p => p.LocId);
            location.Property(p => p.LocId)
                .IsRequired();

            location.Property(d => d.Description)
                .HasMaxLength(500);
            location.Property(i => i.OtherInfo).HasMaxLength(1000);
           /* location.HasKey(p => p.Key);
            location.HasIndex(p => p.Id);
            location.HasIndex(p => new { p.Id, p.NormalizedVersionString })
                .IsUnique();

            location.Property(p => p.Id)
                .HasMaxLength(MaxPackageIdLength)
                .IsRequired();

            location.Property(p => p.NormalizedVersionString)
                .HasColumnName("Version")
                .HasMaxLength(MaxPackageVersionLength)
                .IsRequired();

            location.Property(p => p.OriginalVersionString)
                .HasColumnName("OriginalVersion")
                .HasMaxLength(MaxPackageVersionLength);

            location.Property(p => p.ReleaseNotes)
                .HasColumnName("ReleaseNotes");

            location.Property(p => p.Authors)
                .HasMaxLength(DefaultMaxStringLength)
                .HasConversion(StringArrayToJsonConverter.Instance)
                .Metadata.SetValueComparer(StringArrayComparer.Instance);

            location.Property(p => p.IconUrl)
                .HasConversion(UriToStringConverter.Instance)
                .HasMaxLength(DefaultMaxStringLength);

            location.Property(p => p.LicenseUrl)
                .HasConversion(UriToStringConverter.Instance)
                .HasMaxLength(DefaultMaxStringLength);

            location.Property(p => p.ProjectUrl)
                .HasConversion(UriToStringConverter.Instance)
                .HasMaxLength(DefaultMaxStringLength);

            location.Property(p => p.RepositoryUrl)
                .HasConversion(UriToStringConverter.Instance)
                .HasMaxLength(DefaultMaxStringLength);

            location.Property(p => p.Tags)
                .HasMaxLength(DefaultMaxStringLength)
                .HasConversion(StringArrayToJsonConverter.Instance)
                .Metadata.SetValueComparer(StringArrayComparer.Instance);

            location.Property(p => p.Description).HasMaxLength(DefaultMaxStringLength);
            location.Property(p => p.Language).HasMaxLength(MaxPackageLanguageLength);
            location.Property(p => p.MinClientVersion).HasMaxLength(MaxPackageMinClientVersionLength);
            location.Property(p => p.Summary).HasMaxLength(DefaultMaxStringLength);
            location.Property(p => p.Title).HasMaxLength(MaxPackageTitleLength);
            location.Property(p => p.RepositoryType).HasMaxLength(MaxRepositoryTypeLength);

            location.Ignore(p => p.Version);
            location.Ignore(p => p.IconUrlString);
            location.Ignore(p => p.LicenseUrlString);
            location.Ignore(p => p.ProjectUrlString);
            location.Ignore(p => p.RepositoryUrlString);

            // TODO: This is needed to make the dependency to package relationship required.
            // Unfortunately, this would generate a migration that drops a foreign key, which
            // isn't supported by SQLite. The migrations will be need to be recreated for this.
            // Consumers will need to recreate their database and reindex all their packages.
            // To make this transition easier, I'd like to finish this change:
            // https://github.com/loic-sharma/BaGet/pull/174
            //package.HasMany(p => p.Dependencies)
            //    .WithOne(d => d.Package)
            //    .IsRequired();

            location.HasMany(p => p.PackageTypes)
                .WithOne(d => d.Package)
                .IsRequired();

            location.HasMany(p => p.TargetFrameworks)
                .WithOne(d => d.Package)
                .IsRequired();

            location.Property(p => p.RowVersion).IsRowVersion();*/
        }
    }
}
