
namespace SoundSphere.Helpers;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SoundSphere.Models;

public class AppDatabaseContext : IdentityDbContext<AppUser>
{
    public AppDatabaseContext(DbContextOptions<AppDatabaseContext> options) : base(options)
    {

    }
    public DbSet<MusicTrack> Tracks { get; set; }
    public DbSet<MusicConcert> Concerts { get; set; }
    public DbSet<Rating> Ratings { get; set; }
}