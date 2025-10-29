using Microsoft.EntityFrameworkCore;
using Sharenv.Application.Interfaces;
using Sharenv.Domain.Entities;

namespace Sharenv.Infra.Data
{
    public class SharenvDbContext : DbContext
    {
        private readonly ICurrentUserService _currentUserService;

        public SharenvDbContext( DbContextOptions<SharenvDbContext> options, ICurrentUserService currentUserService) : base(options)
        {
            _currentUserService = currentUserService;
        }

        ///<inheritdoc/>
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AuditEntry();
            return await base.SaveChangesAsync(cancellationToken);
        }

        ///<inheritdoc/>
        public override int SaveChanges()
        {
            AuditEntry();
            return base.SaveChanges();
        }

        /// <summary>
        /// Gets or sets users
        /// </summary>
        public DbSet<User> User { get; set; }

        /// <summary>
        /// Gets or sets circles
        /// </summary>
        public DbSet<Circle> Circle { get; set; }

        /// <summary>
        /// Gets or sets activities
        /// </summary>
        public DbSet<Activity> Activity { get; set; }

        /// <summary>
        /// Gets or sets circleMembers
        /// </summary>
        public DbSet<CircleMember> CircleMember { get; set; }

        /// <summary>
        /// Gets or sets moments
        /// </summary>
        public DbSet<Moment> Moment { get; set; }

        /// <summary>
        /// Apply audit entry rules
        /// </summary>
        protected void AuditEntry()
        {
            var userId = _currentUserService.CurrentUserId;
            var now = DateTime.UtcNow;

            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = now;
                        entry.Entity.CreatedBy = userId;
                        break;

                    case EntityState.Modified:
                        entry.Entity.UpdatedAt = now;
                        entry.Entity.UpdatedBy = userId;

                        entry.Property(p => p.CreatedAt).IsModified = false;
                        entry.Property(p => p.CreatedBy).IsModified = false;
                        break;
                }
            }
        }
    }
}
