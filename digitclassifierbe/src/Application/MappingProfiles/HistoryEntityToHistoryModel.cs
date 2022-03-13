using Application.Models.History;
using AutoMapper;
using DataAcces.Entities;

namespace Application.MappingProfiles
{
    public class HistoryEntityToHistoryModel : Profile
    {
        public HistoryEntityToHistoryModel()
        {
            CreateMap<History, HistoryResponse>().ReverseMap();
        }
    }
}
