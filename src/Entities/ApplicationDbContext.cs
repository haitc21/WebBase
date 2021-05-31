using Entities.Configurations;
using Entities.Entities;
using Entities.Extensions;
using Entities.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Entities
{
    public class ApplicationDbContext : IdentityDbContext<AppUser,AppRole,Guid>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            IEnumerable<EntityEntry> modified = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Modified || e.State == EntityState.Added);
            foreach (EntityEntry item in modified)
            {
                if (item.Entity is IDateTracking changedOrAddedItem)
                {
                    if (item.State == EntityState.Added)
                    {
                        changedOrAddedItem.CreateDate = DateTime.Now;
                    }
                    else
                    {
                        changedOrAddedItem.LastModifiedDate = DateTime.Now;
                    }
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Custom entities by Fluent API
            builder.Entity<IdentityRole>().Property(x => x.Id).HasMaxLength(50).IsUnicode(false);
            builder.ApplyConfiguration(new CommandInFunctionConfig());
            builder.ApplyConfiguration(new PermissionConfig());
            builder.ApplyConfiguration(new AppUserConfig());
            builder.ApplyConfiguration(new AppRoleConfig());

            // identity
            builder.Entity<IdentityUserClaim<Guid>>().ToTable("AppUserClaims");
            builder.Entity<IdentityRoleClaim<Guid>>().ToTable("AppRoleClaims");
            builder.Entity<IdentityUserRole<Guid>>().ToTable("AppUserRoles")
                .HasKey(x => new { x.UserId, x.RoleId });
            builder.Entity<IdentityUserLogin<Guid>>().ToTable("AppUserLogins")
                .HasKey(x => x.UserId);
            builder.Entity<IdentityUserToken<Guid>>().ToTable("AppUserTokens")
                .HasKey(x => x.UserId);

            // Seeding data
            builder.Seed();

            //builder.HasSequence("WebBase");
        }

        public DbSet<Command> Commands { set; get; }
        public DbSet<CommandInFunction> CommandInFunctions { set; get; }

        public DbSet<ActivityLog> ActivityLogs { set; get; }
        public DbSet<Function> Functions { set; get; }
        public DbSet<Permission> Permissions { set; get; }
    }
}
