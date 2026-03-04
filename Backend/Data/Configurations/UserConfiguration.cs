using Backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.PublicId).IsRequired(true);
        builder.HasIndex(x => x.PublicId).IsUnique();

        builder.Property(x => x.FullName).IsRequired(true).HasMaxLength(50);

        builder.Property(x => x.Email).IsRequired(true).HasMaxLength(255);
        builder.HasIndex(x => x.Email).IsUnique();

        builder.Property(x => x.Password).IsRequired(false).HasMaxLength(255);

        builder.Property(x => x.Provider).IsRequired(true).HasConversion<string>().HasMaxLength(20);
    }
}
