using InternetForum.Administration.DAL.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Cryptography;

namespace InternetForum.Administration.DAL
{
    public static class IdentityExtension
    {
        private static byte[] GenerateSalt(int salt_length = 32)
        {
            var salt = new byte[salt_length];
            using (var random = new RNGCryptoServiceProvider())
            {
                random.GetNonZeroBytes(salt);
            }

            return salt;
        }
        public static void SeedData(this ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(new IdentityRole[5]
            {
                new IdentityRole("Owner")
                {
                    Id = "5",
                    NormalizedName = "Owner".ToUpper()
                },
                new IdentityRole("Administrator"){
                    Id = "1", NormalizedName = "Administrator".ToUpper()
                },
                new IdentityRole("User"){ Id = "2", NormalizedName = "User".ToUpper() },
                new IdentityRole("BannedUser"){ Id = "3", NormalizedName = "BannedUser".ToUpper() },
                new IdentityRole("PremiumUser"){ Id = "4", NormalizedName = "PremiumUser".ToUpper() }
            });

            builder.Entity<AuthUser>().HasData(new AuthUser[6]
            {
                new AuthUser()
                {
                    Id = "1",
                    UserName = "anton_1990",
                    NormalizedUserName = "anton_1990".ToUpper(),
                    NormalizedEmail = "anton@gmail.com".ToUpper(),
                    Email = "anton@gmail.com",
                    EmailConfirmed = true,
                    PasswordHash = "7bi7gJ3/LNEKOkqCvCF8T5MJWjI23GBrxYiPMTmGV1U=",
                    CodeWords = "good_summer_hollidays",
                    salt = GenerateSalt()
                },
                new AuthUser()
                {
                    Id = "2",
                    UserName = "dmidro",
                    Email = "dmitro_kovalcuk@gmail.com",
                    NormalizedUserName = "dmidro".ToUpper(),
                    NormalizedEmail = "dmitro_kovalcuk@gmail.com".ToUpper(),
                    EmailConfirmed = true,
                    PasswordHash = "5FaVRUYDNlmA5fspxYnObyeP8PlfTXdDHH08ILn9XfU=",
                    salt = GenerateSalt()
                },
                new AuthUser()
                {
                    Id = "3",
                    UserName = "user1984",
                    Email = "My_mail84@gmail.com",
                     NormalizedUserName = "user1984".ToUpper(),
                    NormalizedEmail = "My_mail84@gmail.com".ToUpper(),
                    EmailConfirmed = true,
                    PasswordHash = "BnGrZd3DgF/LpNAVRLy/yMEQF60jJotJgioc/k36Aaw=",
                    CodeWords = "Whiski",
                    salt = GenerateSalt()

                },
                new AuthUser()
                {
                    Id = "4",
                    UserName = "Have_A_Nice_Day",
                     NormalizedUserName = "Have_A_Nice_Day".ToUpper(),
                    NormalizedEmail = "GoodLuck11@gmail.com".ToUpper(),
                    Email = "GoodLuck11@gmail.com",
                    EmailConfirmed = true,
                    PasswordHash = "+fw/IThmwaKuhDduV2zTNzLD/yyS+wQZZDnLZW6KQLg=",
                    salt = GenerateSalt()
                },
                new AuthUser()
                {
                    Id = "5",
                    UserName = "mike_2002",
                    Email = "t_mike2002_11@gmail.com",
                    EmailConfirmed = true,
                     NormalizedUserName = "mike_2002".ToUpper(),
                    NormalizedEmail = "t_mike2002_11@gmail.com",
                    PasswordHash = "fXBqIKok+nPPn+/PvFD6q0sdO/Hr63iULr0G+PmLwJE=",
                    CodeWords = "Veni, vidi, vici",
                    salt = GenerateSalt()
                },
                 new AuthUser()
                {
                    Id = "6",
                    UserName = "danil_owner",
                    Email = "danil.t404@gmail.com",
                    EmailConfirmed = true,
                     NormalizedUserName = "danil_owner".ToUpper(),
                    NormalizedEmail = "danil.t404@gmail.com",
                    PasswordHash = "D6E8Lon6PMsdI+nk2+V9rKxbEVRfWJsxvesYrt0Pl2U=",
                    CodeWords = "owner of this project",
                    salt = GenerateSalt()
                }
            });

            builder.Entity<IdentityUserRole<string>>()
                .HasData(new IdentityUserRole<string>[8]
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
                    },
                    new IdentityUserRole<string>()
                    {
                        UserId ="6",
                        RoleId = "5"
                    },
                    new IdentityUserRole<string>()
                    {
                        UserId ="6",
                        RoleId = "1"
                    },
                    new IdentityUserRole<string>()
                    {
                        UserId ="6",
                        RoleId = "2"
                    }
                }
            );
        }
    }
}
