using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Repository.Persistence.Models;

namespace Repository.Persistence.Configuration
{
    public class DatasetConfiguration : IEntityTypeConfiguration<Dataset>
    {
        public void Configure(EntityTypeBuilder<Dataset> builder)
        {
            builder.HasKey(dataset => new { dataset.Id });
            builder.Property(dataset => dataset.Id)
                .IsRequired();
            builder.Property(dataset => dataset.UserId)
                .IsRequired();
            builder.Property(dataset => dataset.Title)
                .IsRequired()
                .HasMaxLength(24);
            builder.Property(dataset => dataset.Public)
                .IsRequired();
            builder.Property(dataset => dataset.LastModification)
                .IsRequired();
            builder.HasOne(dataset => dataset.UserProfile)
                .WithMany(userProfile => userProfile.Datasets)
                .HasForeignKey(dataset => dataset.UserId)
                .HasPrincipalKey(userProfile => userProfile.Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}