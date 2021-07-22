using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Repository.Persistence.Models;

namespace Repository.Persistence.Configuration
{
    public class MachineLearningModelConfiguration : IEntityTypeConfiguration<MachineLearningModel>
    {
        public void Configure(EntityTypeBuilder<MachineLearningModel> builder)
        {
            builder.HasKey(machineLearningModel => new { machineLearningModel.Id });
            builder.Property(machineLearningModel => machineLearningModel.Name)
                .IsRequired()
                .HasMaxLength(40);
            builder.Property(machineLearningModel => machineLearningModel.Architecture)
                .IsRequired()
                .HasMaxLength(16);
            builder.Property(machineLearningModel => machineLearningModel.Loss)
                .IsRequired()
                .HasMaxLength(16);
            builder.Property(machineLearningModel => machineLearningModel.ShortDescription)
                .IsRequired()
                .HasMaxLength(140);
            builder.Property(machineLearningModel => machineLearningModel.LongDescription)
               .IsRequired()
               .HasMaxLength(280);
            builder.Property(machineLearningModel => machineLearningModel.CreationDate)
                .IsRequired();
        }
    }
}