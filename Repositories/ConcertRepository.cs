
using Microsoft.EntityFrameworkCore;
using SoundSphere.Helpers;
using SoundSphere.Interfaces;
using SoundSphere.Models;

namespace SoundSphere.Repositories;

public class ConcertRepository : IConcertRepository{

    public AppDatabaseContext _context;
    public ConcertRepository(AppDatabaseContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<MusicConcert>> GetAllConcerts()
    {
        return await _context.Concerts.ToListAsync();
    }
    public async Task<MusicConcert> GetConcertById(int id)
    {
        return await _context.Concerts.Include(concert => concert.Rating).FirstOrDefaultAsync(concert => concert.Id == id);
    }
    public async Task<MusicConcert> GetConcertByIdAsyncNoTracking(int id)
    {
        return await _context.Concerts.Include(concert => concert.Rating).AsNoTracking().FirstOrDefaultAsync(concert => concert.Id == id);

    }
    public bool AddNewConcert(MusicConcert musicConcert)
    {
        _context.Add(musicConcert);
        return Save();
    }
    public bool UpdateExistingConcert(MusicConcert musicConcert)
    {
        _context.Update(musicConcert);
        return Save();
    }
    public bool DeleteConcert(MusicConcert musicConcert)
    {
        _context.Remove(musicConcert);
        return Save();
    }
    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;
    }
    
}