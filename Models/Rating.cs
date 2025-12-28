

using System.ComponentModel.DataAnnotations;

namespace SoundSphere.Models;

public class Rating
{
    [Key] // PK
    public int Id { get; set; }
    public string Comment { get; set; }
    public int Points { get; set; }
}