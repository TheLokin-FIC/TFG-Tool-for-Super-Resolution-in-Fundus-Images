using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Repository.Persistence.Models;

namespace Repository.Persistence.Configuration
{
    public class SuperResolutionModelConfiguration : IEntityTypeConfiguration<SuperResolutionModel>
    {
        public void Configure(EntityTypeBuilder<SuperResolutionModel> builder)
        {
            builder.HasKey(superResolutionModel => new { superResolutionModel.ModelId, superResolutionModel.UpscaleFactor });
            builder.Property(superResolutionModel => superResolutionModel.ModelId)
                .IsRequired();
            builder.Property(superResolutionModel => superResolutionModel.UpscaleFactor)
                .IsRequired();
            builder.Property(superResolutionModel => superResolutionModel.Path)
                .IsRequired()
                .HasMaxLength(260);
            builder.HasIndex(superResolutionModel => superResolutionModel.Path)
                .IsUnique();
            builder.HasOne(superResolutionModel => superResolutionModel.MachineLearningModel)
                .WithMany(machineLearningModel => machineLearningModel.SuperResolutionModels)
                .HasForeignKey(superResolutionModel => superResolutionModel.ModelId)
                .HasPrincipalKey(machineLearningModel => machineLearningModel.Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}