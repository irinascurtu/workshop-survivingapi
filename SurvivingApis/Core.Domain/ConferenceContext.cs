using System;
using System.Collections.Generic;
using System.Text;
using Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Core.Domain
{
    public class ConferenceContext : DbContext
    {
        public ConferenceContext(DbContextOptions<ConferenceContext> options)
          : base(options)
        {
            this.Database.EnsureCreated();
        }

        public DbSet<Speaker> Speakers { get; set; }
        public DbSet<Talk> Talks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // seed the database with dummy data

            
            modelBuilder.Entity<Speaker>().HasData(
                new Speaker()
                {
                    Id = 1,
                    FirstName = "Berry",
                    LastName = "Griffin Beak Eldritch",
                    CompanyName = "Griffin Corp"
                },
                new Speaker()
                {
                    Id = 2,
                    FirstName = "Nancy",
                    LastName = "Swashbuckler Rye",
                    CompanyName = "Swash Corp"
                },
                new Speaker()
                {
                    Id = 3,
                    FirstName = "Cici",
                    LastName = "Ryee",
                    CompanyName = "Swash Corp"

                }

             );

            modelBuilder.Entity<Talk>().HasData(
                new Talk()
                {
                    Id = 1,
                    SpeakerId = 1,
                    Title = "Commandeering a Ship Without Getting Caught",
                    Description =
                        "Commandeering a ship in rough waters isn't easy. " +
                        " Commandeering it without getting caught is even harder.  " +
                        "In this course you'll learn how to sail away and avoid those pesky musketeers."
                },
                   new Talk()
                   {
                       Id = 2,
                       SpeakerId = 1,
                       Title = "Avoiding Brawls While Drinking as Much Rum as You Desire",
                       Description = "Every good pirate loves rum, but it also has a tendency to get you into trouble.  In this course you'll learn how to avoid that.  This new exclusive edition includes an additional chapter on how to run fast without falling while drunk."
                   },
                   new Talk
                   {
                       Id = 3,
                       SpeakerId = 2,
                       Title = "Overthrowing Mutiny",
                       Description = "In this course, the author provides tips to avoid, or, if needed, overthrow pirate mutiny."
                   }

                );


            base.OnModelCreating(modelBuilder);
        }
    }
}
