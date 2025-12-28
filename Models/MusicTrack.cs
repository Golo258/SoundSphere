
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoundSphere.Models;

public class MusicTrack {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Title { get; set; }
    public string Artist { get; set; }
    public string Genre { get; set; }
    public int ReleaseYear { get; set; }
    public string Image { get; set; }
    [ForeignKey("Rating")]
    public int RatingId { get; set; }

    public Rating Rating { get; set; }
    [ForeignKey("AppUser")]
    public string? AppUserId { get; set; }

    public AppUser? AppUser { get; set; }
}