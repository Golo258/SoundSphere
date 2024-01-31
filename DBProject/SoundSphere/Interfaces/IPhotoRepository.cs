
using CloudinaryDotNet.Actions;

namespace SoundSphere.Interfaces;

public interface IPhotoRepository
{
    Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
    Task<DeletionResult> DeletePhotoAsync(string publicId);

}