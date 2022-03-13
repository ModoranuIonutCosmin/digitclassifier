using Application.Models.Image;
using System.Threading.Tasks;
using DataAcces.Entities;

namespace Application.Services
{
    public interface IImageService
    {
        Task<ImageResponse> Predict(User user, ImageRequest request);
    }
}
