using Application.Models.Registration;
using AutoMapper;
using DataAcces.Entities;
using System;

namespace Application.MappingProfiles
{
    public class RequestToDomainProfile : Profile
    {
        public RequestToDomainProfile()
        {
            CreateMap<RegistrationRequestModel, User>();
        }
    }
}
