using Microsoft.EntityFrameworkCore;
using Repository.Persistence.Configuration;
using Repository.Persistence.Extensions;
using Repository.Persistence.Models;

namespace Repository.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<MachineLearningModel> MachineLearningModels { get; set; }
        public DbSet<SuperResolutionModel> SuperResolutionModels { get; set; }

        public DbSet<UserProfile> UserProfiles { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            new MachineLearningModelConfiguration().Configure(builder.Entity<MachineLearningModel>());
            new SuperResolutionModelConfiguration().Configure(builder.Entity<SuperResolutionModel>());
            new UserProfileConfiguration().Configure(builder.Entity<UserProfile>());

            builder.Seed();
        }
    }
}