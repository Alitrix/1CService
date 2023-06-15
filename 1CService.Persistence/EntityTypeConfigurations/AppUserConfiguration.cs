using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using _1CService.Application.DTO;

namespace _1CService.Persistence.EntityTypeConfigurations
{
    internal class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasKey(user => user.Id);
            builder.HasIndex(user => user.Id).IsUnique();
            builder.Property(user => user.UserName).HasMaxLength(50);
            builder.HasAlternateKey(user => user.Email);
            builder.HasIndex(user => user.Email).IsUnique();
        }
    }
}
