using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TravelBot.Domain.Entities;

namespace TravelBot.Infrastructure
{
    public class TravelBotDbContext : DbContext
    {
        public TravelBotDbContext(DbContextOptions<TravelBotDbContext> options) : base(options) { }

        public DbSet<ChatSession> Chats { get; set; }
        public DbSet<ChatMessage> Mensajes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChatSession>()
                .HasMany(c => c.Mensajes)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
