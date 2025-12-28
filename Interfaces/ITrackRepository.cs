
using SoundSphere.Models;

namespace SoundSphere.Interfaces;

public interface ITrackRepository {
    Task<IEnumerable<MusicTrack>> GetAllTracks();
    Task<MusicTrack> GetTrackById(int id);
    Task<MusicTrack> GetTrackByIdAsyncNoTracking(int id);
    bool AddNewTrack(MusicTrack musicTrack);
    bool UpdateExistingTrack(MusicTrack musicTrack);
    bool DeleteTrack(MusicTrack musicTrack);
    bool Save();
}