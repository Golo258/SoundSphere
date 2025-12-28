

using Microsoft.AspNetCore.Mvc;
using SoundSphere.Interfaces;
using SoundSphere.ViewModels;

namespace SoundSphere.Controllers;

public class DashboardController : Controller
{
    private readonly IAppDashboardRepository _dashobardRepository;
    public DashboardController(IAppDashboardRepository dashboardRepository)
    {
        _dashobardRepository = dashboardRepository;
    }
    public async Task<IActionResult> Index()
    {
        var userTracks = await _dashobardRepository.GetAllUserTracks();
        var userConcerts = await _dashobardRepository.GetAllUserConcerts();
        var dashboardVM = new AppDashboardVM
        {
            Tracks = userTracks,
            Concerts = userConcerts
        };

        return View(dashboardVM);
    }
}