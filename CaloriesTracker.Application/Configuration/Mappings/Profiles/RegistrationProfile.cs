using AutoMapper;
using CaloriesTracker.Application.InternalAuth;

namespace CaloriesTracker.Application.Configuration.Mappings.Profiles
{
    internal class RegistrationProfile : Profile
    {
        public RegistrationProfile()
        {
            CreateMap<RegistrationCommand, Domain.InternalAuth.RegistrationRequest>();
        }
    }
}
