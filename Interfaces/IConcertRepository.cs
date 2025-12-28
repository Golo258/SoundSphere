
using SoundSphere.Models;

namespace SoundSphere.Interfaces;


public interface IConcertRepository {
    Task<IEnumerable<MusicConcert>> GetAllConcerts();
    Task<MusicConcert> GetConcertById(int id);
    Task<MusicConcert> GetConcertByIdAsyncNoTracking(int id);
    
    bool AddNewConcert(MusicConcert musicConcert);
    bool UpdateExistingConcert(MusicConcert musicConcert);
    bool DeleteConcert(MusicConcert musicConcert);
    bool Save();
}