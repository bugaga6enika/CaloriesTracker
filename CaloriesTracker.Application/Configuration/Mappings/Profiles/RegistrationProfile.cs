using AutoMapper;
using CaloriesTracker.Application.User.RegistrationSteps.BodyShape;
using CaloriesTracker.Application.User.RegistrationSteps.Credentials;

namespace CaloriesTracker.Application.Configuration.Mappings.Profiles
{
    internal class RegistrationProfile : Profile
    {
        public RegistrationProfile()
        {
            CreateMap<RegistrationCommand, Domain.User.RegistrationRequest>()
                .ConstructUsing(registrationCommand => Domain.User.RegistrationRequest.Create(registrationCommand.Email,
                registrationCommand.Password,
                registrationCommand.Goal,
                registrationCommand.Gender,
                registrationCommand.CurrentWeight,
                registrationCommand.TargetWeight,
                registrationCommand.Height,
                registrationCommand.DateOfBirth,
                registrationCommand.WeightUnit,
                registrationCommand.HeightUnit));

            CreateMap<SaveBodyShapeCommand, Domain.User.BodyShape>();
        }
    }
}
