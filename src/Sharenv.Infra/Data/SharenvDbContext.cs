using Microsoft.EntityFrameworkCore;
using Sharenv.Domain.Entities;

namespace Sharenv.Infra.Data
{
    public class SharenvDbContext : DbContext
    {
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
    }
}
