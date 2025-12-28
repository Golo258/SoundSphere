
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoundSphere.Models;

public class MusicConcert {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public string Name { get; set; }
    public string Artist { get; set; }
    public string Venue { get; set; }
    public string Image { get; set; }
    public string Description { get; set; }
    
    [ForeignKey("Rating")]
    public int RatingId { get; set; }
    
    public Rating Rating { get; set; }

    [ForeignKey("AppUser")]
    public string? AppUserId { get; set; }
    
    public AppUser? AppUser { get; set; }

}