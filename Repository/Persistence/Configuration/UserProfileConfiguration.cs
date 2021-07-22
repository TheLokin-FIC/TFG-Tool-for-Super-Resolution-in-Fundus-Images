using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Repository.Persistence.Models;

namespace Repository.Persistence.Configuration
{
    public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder.HasKey(userProfile => new { userProfile.Id });
            builder.Property(userProfile => userProfile.Role)
                .IsRequired()
                .HasMaxLength(16);
            builder.Property(userProfile => userProfile.Username)
                .IsRequired()
                .HasMaxLength(24);
            builder.HasIndex(userProfile => userProfile.Username)
                .IsUnique();
            builder.Property(userProfile => userProfile.EncryptedPassword)
                .IsRequired()
                .HasMaxLength(44);
        }
    }
}