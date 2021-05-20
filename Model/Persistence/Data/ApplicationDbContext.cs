using Microsoft.EntityFrameworkCore;
using Model.Persistence.Data.Extensions;
using Model.Persistence.Models;

namespace Model.Persistence.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<MachineLearningModel> MachineLearningModels { get; set; }
        public DbSet<SuperResolutionModel> SuperResolutionModels { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();
        }
    }
}