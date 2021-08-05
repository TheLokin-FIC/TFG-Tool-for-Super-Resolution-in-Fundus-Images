using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Repository.Persistence.Models;

namespace Repository.Persistence.Configuration
{
    public class ImageConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.HasKey(image => new { image.DatasetId, image.Name });
            builder.Property(image => image.DatasetId)
                .IsRequired();
            builder.Property(image => image.Name)
                .IsRequired()
                .HasMaxLength(256);
            builder.Property(image => image.Size)
                .IsRequired();
            builder.Property(image => image.File)
                .IsRequired();
            builder.HasOne(image => image.Dataset)
                .WithMany(dataset => dataset.Images)
                .HasForeignKey(image => image.DatasetId)
                .HasPrincipalKey(dataset => dataset.Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}