
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SoundSphere.Helpers;
using SoundSphere.Interfaces;
using SoundSphere.Models;
using SoundSphere.ViewModels;
using Tutorial;

namespace SoundSphere.Controllers;

public class TrackController : Controller
{
    private readonly ITrackRepository _trackRepository;
    private readonly IPhotoRepository _photoRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public TrackController(ITrackRepository trackRepository, IPhotoRepository photoRepository, IHttpContextAccessor httpContextAccessor)
    {
        _trackRepository = trackRepository;
        _photoRepository = photoRepository;
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task<IActionResult> Index()
    {
        IEnumerable<MusicTrack> concerts = await _trackRepository.GetAllTracks();
        return View(concerts);
    }
    public async Task<IActionResult> Detail(int id)
    {
        MusicTrack searched_concert = await _trackRepository.GetTrackById(id);
        return View(searched_concert);

    }
    public IActionResult Create()
    {
        var currenUserId = _httpContextAccessor.HttpContext.User.GetUserId();
        var createTrackVM = new CreateTrackVM { AppUserId = currenUserId };
        return View(createTrackVM);
    }
    [HttpPost]
    public async Task<IActionResult> Create(CreateTrackVM createTrackVM)
    {
        if (ModelState.IsValid)
        {
            var result = await _photoRepository.AddPhotoAsync(createTrackVM.Image);
            if (result.Error == null)  // Sprawdź, czy przesyłanie pliku było udane
            {
                var track = new MusicTrack
                {
                    Title = createTrackVM.Title,
                    Artist = createTrackVM.Artist,
                    Image = result.Url.ToString(),
                    AppUserId = createTrackVM.AppUserId,
                    Genre = createTrackVM.Genre,
                    ReleaseYear = createTrackVM.ReleaseYear,
                    Rating = new Rating
                    {
                        Comment = createTrackVM.Rating.Comment,
                        Points = createTrackVM.Rating.Points,
                    }
                };
                _trackRepository.AddNewTrack(track);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", $"Photo upload failed: {result.Error.Message}");
            }
        }
        return View(createTrackVM);
    }
    public async Task<IActionResult> Edit(int id)
    {
        var track = await _trackRepository.GetTrackById(id);
        if (track == null)
        {
            return View("Error");
        }
        var editTrackVM = new EditTrackVM
        {
            Title = track.Title,
            Artist = track.Artist,
            URL = track.Image,
            Genre = track.Genre,
            ReleaseYear = track.ReleaseYear,
            RatingId = track.RatingId,
            Rating = track.Rating
        };
        return View(editTrackVM);

    }
    [HttpPost]
    public async Task<IActionResult> Edit(int id, EditTrackVM editTrackVM)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("", "Failed to edit club");
            return View("Edit", editTrackVM);
        }

        var userTrack = await _trackRepository.GetTrackByIdAsyncNoTracking(id);
        if (userTrack != null)
        {
            try
            {
                await _photoRepository.DeletePhotoAsync(userTrack.Image);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Cannot delete photo");
                return View(editTrackVM);
            }
            var photoResult = await _photoRepository.AddPhotoAsync(editTrackVM.Image);
            var track = new MusicTrack
            {
                Id = id,
                Title = editTrackVM.Title,
                Artist = editTrackVM.Artist,
                Genre = editTrackVM.Genre,
                ReleaseYear = editTrackVM.ReleaseYear,
                Image = photoResult.Url.ToString(),
                RatingId = editTrackVM.RatingId,
                Rating = editTrackVM.Rating

            };
            _trackRepository.UpdateExistingTrack(track);
            return RedirectToAction("Index");
        }
        else
        {
            return View("Error");
        }
    }
    public async Task<IActionResult> Delete(int id)
    {
        var trackDetails = await _trackRepository.GetTrackById(id);
        if (trackDetails == null)
        {
            return View("Error");
        }
        return View(trackDetails);

    }
    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteClub(int id)
    {
        var trackDetails = await _trackRepository.GetTrackById(id);
        if (trackDetails == null)
        {
            return View("Error");
        }
        _trackRepository.DeleteTrack(trackDetails);
        return RedirectToAction("Index");

    }

}