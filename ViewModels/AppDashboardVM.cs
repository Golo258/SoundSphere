
using System.ComponentModel.DataAnnotations;
using SoundSphere.Models;

namespace SoundSphere.ViewModels;

public class AppDashboardVM {
    public List<MusicTrack> Tracks { get; set; }
    public List<MusicConcert> Concerts { get; set; }
}