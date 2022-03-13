using Application.Models.Registration;
using AutoMapper;

namespace Application.MappingProfiles
{
    public class RequestToResponseProfile : Profile
    {
        public RequestToResponseProfile()
        {
            CreateMap<RegistrationRequestModel, RegistrationResponseModel>();
        }
    }
}
