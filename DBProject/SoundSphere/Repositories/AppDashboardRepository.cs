
using SoundSphere.Helpers;
using SoundSphere.Interfaces;
using SoundSphere.Models;
using Tutorial;

namespace SoundSphere.Repositories;

public class AppDashboardRepository : IAppDashboardRepository
{

    private readonly AppDatabaseContext _context;
    private readonly IHttpContextAccessor _httpContextAccesser;
    public AppDashboardRepository(AppDatabaseContext context, IHttpContextAccessor httpContextAccesser)
    {
        _context = context;
        _httpContextAccesser = httpContextAccesser;
    }
    public async Task<List<MusicTrack>> GetAllUserTracks()
    {
        var currentUser = _httpContextAccesser.HttpContext?.User.GetUserId();
        var userTracks = _context.Tracks.Where(r => r.AppUser.Id == currentUser);
        return userTracks.ToList();
    }
    public async Task<List<MusicConcert>> GetAllUserConcerts()
    {
        var currentUser = _httpContextAccesser.HttpContext?.User.GetUserId();
        var userConcerts = _context.Concerts.Where(r => r.AppUser.Id == currentUser);
        return userConcerts.ToList();
    }
}