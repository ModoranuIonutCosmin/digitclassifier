using System;
using System.Threading.Tasks;
using Application.Models.History;
using DataAcces.Entities;

namespace Application.Services
{
    public interface IFavoritesService
    {
        Task<FavoritesResponseFull> GetFavoritesAsync(User currentUser, FavoritesRequest request);
        Task<DeleteFavoriteResponse> DeleteFavoriteEntry(Guid id);
        Task<AddFavoriteResponse> AddFavoriteEntry(Guid id);
    }
}