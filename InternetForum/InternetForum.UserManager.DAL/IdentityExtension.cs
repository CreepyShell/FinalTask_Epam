using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace InternetForum.Administration.DAL
{
    public static class IdentityExtension
    {
        public static void SeedData(this ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(new IdentityRole[4]
            {
                new IdentityRole("Administrator"){
                    Id = "1"
                },
                new IdentityRole("User"){ Id = "2" },
                new IdentityRole("BannedUser"){ Id = "3" },
                new IdentityRole("PremiumUser"){ Id = "4" }
            });

            builder.Entity<AuthUser>().HasData(new AuthUser[5]
            {
                new AuthUser()
                {
                    Id = "1",
                    UserName = "anton_1990",
                    Email = "anton@gmail.com",
                    EmailConfirmed = true,
                    PasswordHash = "1111",
                    CodeWords = "good_summer_hollidays"
                },
                new AuthUser()
                {
                    Id = "2",
                    UserName = "dmidro",
                    Email = "dmitro_kovalcuk@gmail.com",
                    EmailConfirmed = true,
                    PasswordHash = "1111"
                },
                new AuthUser()
                {
                    Id = "3",
                    UserName = "user1984",
                    Email = "My_mail84@gmail.com",
                    EmailConfirmed = true,
                    PasswordHash = "1111",
                    CodeWords = "Whiski"

                },
                new AuthUser()
                {
                    Id = "4",
                    UserName = "Have_A_Nice_Day",
                    Email = "GoodLuck11@gmail.com",
                    EmailConfirmed = true,
                    PasswordHash = "1111"
                },
                new AuthUser()
                {
                    Id = "5",
                    UserName = "mike_2002",
                    Email = "t_mike2002_11@gmail.com",
                    EmailConfirmed = true,
                    PasswordHash = "1111",
                    CodeWords = "Veni, vidi, vici"
                }
            });

            builder.Entity<IdentityUserRole<string>>()
                .HasData(new IdentityUserRole<string>[5]
                {
                    new IdentityUserRole<string>()
                    {
                        UserId = "1",
                        RoleId = "1"
                    },
                    new IdentityUserRole<string>()
                    {
                        UserId ="2",
                        RoleId = "2"
                    },
                    new IdentityUserRole<string>()
                    {
                        UserId ="3",
                        RoleId = "3"
                    },
                    new IdentityUserRole<string>()
                    {
                        UserId ="4",
                        RoleId = "4"
                    },
                    new IdentityUserRole<string>()
                    {
                        UserId ="5",
                        RoleId = "2"
                    }
                }
            );
        }
    }
}
