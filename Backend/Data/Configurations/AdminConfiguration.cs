using Backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Data.Configurations;

public class AdminConfiguration : IEntityTypeConfiguration<Admin>
{
    public void Configure(EntityTypeBuilder<Admin> builder)
    {
        builder.ToTable("admins");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.PublicId).HasDefaultValue("gen_random_uuid()").IsRequired();
        builder.HasIndex(x => x.PublicId).IsUnique();

        builder.HasIndex(x => x.Email).IsUnique();

        builder.Property(x => x.Password).HasMaxLength(255).IsRequired(false);

        builder.Property(x => x.Provider).HasConversion<string>().HasMaxLength(15).IsRequired();
    }
}
