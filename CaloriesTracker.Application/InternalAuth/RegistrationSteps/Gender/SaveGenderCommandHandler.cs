﻿using CaloriesTracker.Domain.User.RegistrationSteps;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CaloriesTracker.Application.User.RegistrationSteps.Gender
{
    public class SaveGenderCommandHandler : IRequestHandler<SaveGenderCommand, bool>
    {
        private readonly IGenderRepository _genderRepository;

        public SaveGenderCommandHandler(IGenderRepository genderRepository)
        {
            _genderRepository = genderRepository;
        }

        public Task<bool> Handle(SaveGenderCommand request, CancellationToken cancellationToken)
            => _genderRepository.Save(request.Gender, cancellationToken);
    }
}
