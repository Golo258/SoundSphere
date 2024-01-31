
using SoundSphere.Models;

namespace SoundSphere.Interfaces;

public interface IAppDashboardRepository{
    
    Task<List<MusicTrack>> GetAllUserTracks();
    Task<List<MusicConcert>> GetAllUserConcerts();
}