using Core.Enities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace infrastrcture.Configuration
{
    public static class UserConfiguration
    {

        public static void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Username).HasMaxLength(100);
            builder.Property(u => u.Email);
            builder.Property(u => u.Password);

        }
    }
}
