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

        builder.Property(x => x.PublicId);
        builder.HasIndex(x => x.PublicId).IsUnique();

        builder.HasIndex(x => x.Email).IsUnique();

        builder.Property(x => x.Password).HasMaxLength(255).IsRequired(false);

        builder.Property(x => x.Role).HasConversion<string>().HasMaxLength(15);

        builder.Property(x => x.Provider).HasConversion<string>().HasMaxLength(15);
    }
}
