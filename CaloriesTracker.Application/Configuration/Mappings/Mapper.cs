using AutoMapper;
using CaloriesTracker.Application.Configuration.Mappings.Profiles;
using System;

namespace CaloriesTracker.Application.Configuration.Mappings
{
    internal static class Mapper
    {
        private static readonly Lazy<IMapper> _lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<RegistrationProfile>();
            });

            var mapper = config.CreateMapper();

            return mapper;
        });

        public static IMapper Current => _lazy.Value;
    }
}
