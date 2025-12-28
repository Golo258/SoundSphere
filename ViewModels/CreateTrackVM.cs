
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SoundSphere.Models;

namespace SoundSphere.ViewModels;

public class CreateTrackVM {
    public int Id { get; set; }
    public string Title { get; set; }
    public string Artist { get; set; }
    public string Genre { get; set; }
    public int ReleaseYear { get; set; }
    public IFormFile Image { get; set; }
    public string? URL { get; set; }
    public int RatingId { get; set; }
    public Rating? Rating { get; set; }
    public string? AppUserId { get; set; }
}