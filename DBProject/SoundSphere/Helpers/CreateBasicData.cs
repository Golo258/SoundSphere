using Microsoft.AspNetCore.Identity;
using SoundSphere.Models;

namespace SoundSphere.Helpers
{
    public class CreateBasicData
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDatabaseContext>();

                context.Database.EnsureCreated();

                if (!context.Tracks.Any())
                {
                    context.Tracks.AddRange(new List<MusicTrack>()
                    {
                        new MusicTrack()
                        {
                            Title = "Running Track 1",
                            Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                            Artist = "Bracia Golec",
                            Genre = "Nic sie nie boją",
                            ReleaseYear = 2005,
                            Rating = new Rating
                            {
                                Comment = "Super fajny komentarz",
                                Points = 12,
                            }
                         },
                        new MusicTrack
                        {
                                Title = "Bohemian Rhapsody",
                                Image = "https://i.imgur.com/abc123.jpg",
                                Artist = "Queen",
                                Genre = "Rock",
                                ReleaseYear = 1975,
                                Rating = new Rating
                                {
                                    Comment = "Epic song!",
                                    Points = 5
                                }
                        },
                        new MusicTrack()
                        {
                            Title = "Shape of You",
                            Image = "https://i.imgur.com/xyz456.jpg",
                            Artist = "Ed Sheeran",
                            Genre = "Pop",
                            ReleaseYear = 2017,
                            Rating = new Rating
                            {
                                Comment = "Catchy!",
                                Points = 4
                            }
                        },
                        new MusicTrack()
                        {
                            Title = "Imagine",
                            Image = "https://i.imgur.com/ijk789.jpg",
                            Artist = "John Lennon",
                            Genre = "Soft Rock",
                            ReleaseYear = 1971,
                            Rating = new Rating
                            {
                              Comment = "Timeless classic!",
                              Points = 5
                            }
                        }
                    });
                    context.SaveChanges();
                }
                if (!context.Concerts.Any())
                {
                    context.Concerts.AddRange(new List<MusicConcert>()
                    {
                        new MusicConcert()
                        {
                            Name = "Live Aid",
                            Artist = "Various Artists",
                            Venue = "Wembley Stadium, London",
                            Image = "https://i.imgur.com/concert1.jpg",
                            Description = "Historic charity concert featuring multiple artists.",
                            Rating = new Rating
                            {
                                Comment = "Unforgettable performance!",
                                Points = 5
                            }
                        },
                        new MusicConcert()
                        {
                            Name = "Coachella",
                            Artist = "Various Artists",
                            Venue = "Empire Polo Club, Indio",
                            Image = "https://i.imgur.com/concert2.jpg",
                            Description = "Annual music and arts festival.",
                            Rating = new Rating
                            {
                                Comment = "Amazing atmosphere!",
                                Points = 4
                            }
                        },
                         new MusicConcert()
                        {
                            Name = "Woodstock Festival",
                            Artist = "Various Artists",
                            Venue = "Bethel, New York",
                            Image = "https://i.imgur.com/concert3.jpg",
                            Description = "Iconic festival of the 1960s.",
                            Rating = new Rating
                            {
                                Comment = "A piece of history!",
                                Points = 5
                            }
                        },
                         new MusicConcert()
                        {
                            Name = "Glastonbury Festival",
                            Artist = "Various Artists",
                            Venue = "Worthy Farm, Pilton",
                            Image = "https://i.imgur.com/concert4.jpg",
                            Description = "One of the largest greenfield festivals in the world.",
                            Rating = new Rating
                            {
                                Comment = "Magical experience!",
                                Points = 4
                            }
                        }
                    });
                    context.SaveChanges();
                }
            }
        }

        public static async Task CreateBasicDataForUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                string adminUserEmail = "grzesiuniunia123@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new AppUser()
                    {
                        UserName = "teddysmithdev",
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                        Rating = new Rating()
                        {
                            Comment = "Super User. Super Powers!",
                            Points = 1000
                        }
                    };
                    await userManager.CreateAsync(newAdminUser, "#123ABCgolo52");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string appUserEmail = "user1@gmail.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new AppUser()
                    {
                        UserName = "app-user",
                        Email = appUserEmail,
                        EmailConfirmed = true,
                        Rating = new Rating()
                        {
                            Comment = "Normal User. Normal Powers!",
                            Points = 1000
                        }
                    };
                    await userManager.CreateAsync(newAppUser, "#superFajneHaslo123");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }

                string adminUserEmail2 = "admin2@gmail.com";
                var adminUser2 = await userManager.FindByEmailAsync(adminUserEmail2);
                if (adminUser2 == null)
                {
                    var newAdminUser2 = new AppUser()
                    {
                        UserName = "admin-user2",
                        Email = adminUserEmail2,
                        EmailConfirmed = true,
                        Rating = new Rating()
                        {
                            Comment = "Another Super User. More Super Powers!",
                            Points = 1500
                        }
                    };
                    await userManager.CreateAsync(newAdminUser2, "#AdminPass123");
                    await userManager.AddToRoleAsync(newAdminUser2, UserRoles.Admin);
                }

                string appUserEmail2 = "user2@gmail.com";

                var appUser2 = await userManager.FindByEmailAsync(appUserEmail2);
                if (appUser2 == null)
                {
                    var newAppUser2 = new AppUser()
                    {
                        UserName = "app-user2",
                        Email = appUserEmail2,
                        EmailConfirmed = true,
                        Rating = new Rating()
                        {
                            Comment = "Another Normal User. More Normal Powers!",
                            Points = 900
                        }
                    };
                    await userManager.CreateAsync(newAppUser2, "#UserPass456");
                    await userManager.AddToRoleAsync(newAppUser2, UserRoles.User);
                }
            }
        }
    }

}