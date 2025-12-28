
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

public class ConcertController : Controller {

    private readonly IConcertRepository _concertRepository;
    private readonly IPhotoRepository _photoRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ConcertController(
        IConcertRepository concertRepository,
        IPhotoRepository photoRepository,
        IHttpContextAccessor httpContextAccessor
    ) {
        _concertRepository = concertRepository;
        _photoRepository = photoRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<IActionResult> Index() {
        IEnumerable<MusicConcert> concerts = await _concertRepository.GetAllConcerts();
        return View(concerts);
    }

    public async Task<IActionResult> Detail(int id) {
        MusicConcert searched_concert = await _concertRepository.GetConcertById(id);
        return View(searched_concert);
    }

    public IActionResult Create() {
        var currenUserId = _httpContextAccessor.HttpContext.User.GetUserId();
        var createConcertVM = new CreateConcertVM { AppUserId = currenUserId };
        return View(createConcertVM);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateConcertVM concertVM) {
        if (ModelState.IsValid) {
            var result = await _photoRepository.AddPhotoAsync(concertVM.Image);
            if (result.Error == null) {
                var concert = new MusicConcert {
                    Name = concertVM.Name,
                    Artist = concertVM.Artist,
                    Venue = concertVM.Venue,
                    Image = result.Url.ToString(),
                    AppUserId = concertVM.AppUserId,
                    Description = concertVM.Description,
                    Rating = new Rating {
                        Comment = concertVM.Rating.Comment,
                        Points = concertVM.Rating.Points,
                    }
                };
                _concertRepository.AddNewConcert(concert);
                return RedirectToAction("Index");
            }
            else {
                ModelState.AddModelError(
                    "", 
                    $"Photo upload failed: {result.Error.Message}"
                );
            }
        }
        return View(concertVM);
    }

    public async Task<IActionResult> Edit(int id) {
        var concert = await _concertRepository.GetConcertById(id);
        if (concert == null) {
            return View("Error");
        }

        var concertVM = new EditConcertVM {
            Name = concert.Name,
            Artist = concert.Artist,
            URL = concert.Image,
            Venue = concert.Venue,
            Description = concert.Description,
            RatingId = concert.RatingId,
            Rating = concert.Rating
        };
        return View(concertVM);

    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, EditConcertVM concertVM) {
        if (!ModelState.IsValid) {
            ModelState.AddModelError("", "Failed to edit club");
            return View("Edit", concertVM);
        }

        var userConcert = await _concertRepository.GetConcertByIdAsyncNoTracking(id);
        if (userConcert != null) {
            try {
                await _photoRepository.DeletePhotoAsync(userConcert.Image);
            }
            catch (Exception ex) {
                ModelState.AddModelError("", "Cannot delete photo");
                return View(concertVM);
            }

            var photoResult = await _photoRepository.AddPhotoAsync(concertVM.Image);
            var concert = new MusicConcert {
                Id = id,
                Name = concertVM.Name,
                Artist = concertVM.Artist,
                Venue = concertVM.Venue,
                Description = concertVM.Description,
                Image = photoResult.Url.ToString(),
                RatingId = concertVM.RatingId,
                Rating = concertVM.Rating

            };
            _concertRepository.UpdateExistingConcert(concert);
            return RedirectToAction("Index");
        }
        else {
            return View("Error");
        }
    }

    public async Task<IActionResult> Delete(int id) {
        var concertDetails = await _concertRepository.GetConcertById(id);
        if (concertDetails == null) {
            return View("Error");
        }
        return View(concertDetails);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteClub(int id) {
        var concertDetails = await _concertRepository.GetConcertById(id);
        if (concertDetails == null) {
            return View("Error");
        }

        _concertRepository.DeleteConcert(concertDetails);
        return RedirectToAction("Index");
    }
}