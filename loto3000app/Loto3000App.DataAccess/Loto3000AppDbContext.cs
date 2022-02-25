using Loto3000App.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Loto3000App.DataAccess
{
    public class Loto3000AppDbContext:DbContext
    {
        public Loto3000AppDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketDetails> TicketDetails { get; set; }
        
        public DbSet<Draw> Draws { get; set; }
        public DbSet<DrawDetails> DrawDetails { get; set; }

        public DbSet<Session> Sessions { get; set; }
        public DbSet<Prize> Prizes { get; set; }
        public DbSet<Winning> Winnings { get; set; }

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

            base.OnModelCreating(modelBuilder);

            //ticket
            modelBuilder.Entity<Ticket>()
                .HasOne(x => x.User)
                .WithMany(x => x.Tickets)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);



            modelBuilder.Entity<Prize>()
                .HasData(
                    new Prize()
                    {
                        Id = 1,
                        PrizeType = "50$ Gift Card"
                    },
                    new Prize()
                    {
                        Id = 2,
                        PrizeType = "100$ Gift Card"
                    }, new Prize()
                    {
                        Id = 3,
                        PrizeType = "TV"
                    }, new Prize()
                    {
                        Id = 4,
                        PrizeType = "Vacation"
                    }, new Prize()
                    {
                        Id = 5,
                        PrizeType = "Car"
                    });

            modelBuilder.Entity<Role>()
                .HasData(
                    new Role()
                    {
                        Id = 1,
                        RoleType = "Admin"
                    }, new Role()
                    {
                        Id = 2,
                        RoleType = "Participant"
                    });

            modelBuilder.Entity<User>()
                .HasData(
                    new User()
                    {
                        Id = 1,
                        FirstName = "Ceca",
                        LastName = "Vasileva",
                        Username = "ceca20",
                        Password = "?B??:q?m?0???eV"

                    });

            modelBuilder.Entity<UserRole>()
                .HasData(
                    new UserRole()
                    {
                        Id = 1,
                        UserId = 1,
                        RoleId = 1


                    },
                    new UserRole()
                    {
                        Id = 2,
                        UserId = 1,
                        RoleId = 2


                    });


        }
    }
}
