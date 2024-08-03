using Core.Enities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infrastrcture.Configuration
{
    public static class ChatConfiguration
    {
        public static void Configure(EntityTypeBuilder<Chat> builder)
        {

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Message);
            builder.Property(c => c.Timestamp);
            builder.HasOne(c => c.User)
                   .WithMany(u => u.Chats)
                   .HasForeignKey(c => c.UserId);
        }

    }
}
