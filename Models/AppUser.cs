
using System.ComponentModel.DataAnnotations.Schema;

namespace SoundSphere.Models;
using Microsoft.AspNetCore.Identity;
public class AppUser : IdentityUser
{

    [ForeignKey("Rating")]
    public int? RatingId { get; set; }
    public Rating? Rating { get; set; }
    public ICollection<MusicTrack> Tracks { get; set; }
    public ICollection<MusicConcert> Concerts { get; set; }
}