
using Microsoft.EntityFrameworkCore;
using SoundSphere.Helpers;
using SoundSphere.Interfaces;
using SoundSphere.Models;

namespace SoundSphere.Repositories;

public class TrackRepository : ITrackRepository
{

    public AppDatabaseContext _context;
    public TrackRepository(AppDatabaseContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<MusicTrack>> GetAllTracks()
    {
        return await _context.Tracks.ToListAsync();
    }
    public async Task<MusicTrack> GetTrackById(int id)
    {
        return await _context.Tracks.Include(track => track.Rating).FirstOrDefaultAsync(track => track.Id == id);
    }
    public async Task<MusicTrack> GetTrackByIdAsyncNoTracking(int id)
    {
        return await _context.Tracks.Include(track => track.Rating).AsNoTracking().FirstOrDefaultAsync(track => track.Id == id);

    }
    public bool AddNewTrack(MusicTrack musicTrack)
    {
        _context.Add(musicTrack);
        return Save();
    }
    public bool UpdateExistingTrack(MusicTrack musicTrack)
    {
        _context.Update(musicTrack);
        return Save();
    }
    public bool DeleteTrack(MusicTrack musicTrack)
    {
        _context.Remove(musicTrack);
        return Save();
    }
    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;
    }

}