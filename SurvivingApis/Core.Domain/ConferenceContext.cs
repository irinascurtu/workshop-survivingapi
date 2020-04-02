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
                    FirstName = "Mickey",
                    LastName = "McMouse",
                    CompanyName = "Whole Corp"
                },
                new Speaker()
                {
                    Id = 2,
                    FirstName = "Donatella",
                    LastName = "McSharp",
                    CompanyName = "REST Ltd"
                },
                new Speaker()
                {
                    Id = 3,
                    FirstName = "Minnie",
                    LastName = "McPatch",
                    CompanyName = "Mouse Org"

                }

             );

            modelBuilder.Entity<Talk>().HasData(
                new Talk()
                {
                    Id = 1,
                    SpeakerId = 1,
                    Title = "Delivering a bad product",
                    Description =
                        "Delivering a good product in isn't easy. " +
                        " Delivering a bad one without getting caught is even harder.  " 
                },
                   new Talk()
                   {
                       Id = 2,
                       SpeakerId = 1,
                       Title = "Avoiding CodeReviews Disasters",
                       Description = "Every good developer loves code reviews, but it also can get you into trouble.  In this talk you'll learn how to avoid that."
                   },
                   new Talk
                   {
                       Id = 3,
                       SpeakerId = 2,
                       Title = "Overthrowing bad code",
                       Description = "In this talk, the speaker provides tips to avoid, or, if needed, overthrow bad code."
                   }

                );


            base.OnModelCreating(modelBuilder);
        }
    }
}
