using Concert.Domain.Entities;
using Concert.Infra.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Concert.Infra.Context
{
    public class ConcertDataContext : DbContext
    {
        public DbSet<User> Investor { get; set; }
        public DbSet<UserStory> UserStory { get; set; }
        public DbSet<Card> Card { get; set; }
        public DbSet<Votes> Votes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new UserStoryMap());
            modelBuilder.ApplyConfiguration(new CardMap());
            modelBuilder.ApplyConfiguration(new VotesMap());
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // get the configuration from the app settings
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // define the database to use
            optionsBuilder.UseNpgsql(config.GetConnectionString("DefaultConnection"));
        }
    }
}
