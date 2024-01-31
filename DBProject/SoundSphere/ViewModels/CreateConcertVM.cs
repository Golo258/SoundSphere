
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoundSphere.Models;

public class CreateConcertVM
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Artist { get; set; }
    public string Venue { get; set; }
    public IFormFile Image { get; set; }
    public string? URL { get; set; }
    public string Description { get; set; }
    public int RatingId { get; set; }
    public Rating? Rating { get; set; }
    public string? AppUserId { get; set; }

}