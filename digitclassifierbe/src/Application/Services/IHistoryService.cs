using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Models.History;
using DataAcces.Entities;

namespace Application.Services
{
    public interface IHistoryService
    {
        Task<HistoryResponseFull> GetHistoryForUserAsync(User currentUser, HistoryRequest request);
        Task<HistoryResponse> DeleteHistoryEntry(Guid id);
    }
}
