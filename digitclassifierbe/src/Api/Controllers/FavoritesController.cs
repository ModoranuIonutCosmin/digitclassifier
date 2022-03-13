using System;
using System.Threading.Tasks;
using Api.Attributes;
using Application.Models.History;
using Application.Services;
using DataAcces.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Api.Controllers
{  
    [Route("api/favorites")]
    [ApiController]
    public class FavoritesController: ControllerBase
    {
        private readonly IFavoritesService _favoritesService;

        public FavoritesController(IFavoritesService favoriteService)
        {
            _favoritesService = favoriteService;
        }

        [HttpGet]
        [Authorize]
        public async Task<FavoritesResponseFull> GetFavorites([FromQuery] FavoritesRequest request)
        {
            User user = (User) HttpContext.Items["User"];
            return await _favoritesService.GetFavoritesAsync(user, request);
        }
        [HttpPut("/api/favorites/add/{id}")]
        [Authorize]
        public async Task<AddFavoriteResponse> AddFavorite([FromRoute] Guid id)
        {
            User user = (User) HttpContext.Items["User"];
            return await _favoritesService.AddFavoriteEntry(id);
        }
        [HttpPut("/api/favorites/delete/{id}")]
        [Authorize]
        public async Task<DeleteFavoriteResponse> DeleteFavorite([FromRoute] Guid id)
        {
            User user = (User) HttpContext.Items["User"];
            return await _favoritesService.DeleteFavoriteEntry(id);
        }
        
    }
    
}