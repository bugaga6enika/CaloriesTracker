using AutoMapper;
using CaloriesTracker.Application.InternalAuth.RegistrationSteps.BodyShape;
using CaloriesTracker.Application.InternalAuth.RegistrationSteps.Credentials;

namespace CaloriesTracker.Application.Configuration.Mappings.Profiles
{
    internal class RegistrationProfile : Profile
    {
        public RegistrationProfile()
        {
            CreateMap<RegistrationCommand, Domain.InternalAuth.RegistrationRequest>()
                .ConstructUsing(registrationCommand => Domain.InternalAuth.RegistrationRequest.Create(registrationCommand.Email,
                registrationCommand.Goal,
                registrationCommand.Gender,
                registrationCommand.CurrentWeight,
                registrationCommand.TargetWeight,
                registrationCommand.Height,
                registrationCommand.DateOfBirth));

            CreateMap<SaveBodyShapeCommand, Domain.InternalAuth.BodyShape>();
        }
    }
}
