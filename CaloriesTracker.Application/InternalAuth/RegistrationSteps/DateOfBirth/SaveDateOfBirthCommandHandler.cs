﻿using CaloriesTracker.Domain.User.RegistrationSteps;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CaloriesTracker.Application.User.RegistrationSteps.DateOfBirth
{
    public class SaveDateOfBirthCommandHandler : IRequestHandler<SaveDateOfBirthCommand, bool>
    {
        private readonly IDateOfBirthRepository _dateOfBirthRepository;

        public SaveDateOfBirthCommandHandler(IDateOfBirthRepository dateOfBirthRepository)
        {
            _dateOfBirthRepository = dateOfBirthRepository;
        }

        public Task<bool> Handle(SaveDateOfBirthCommand request, CancellationToken cancellationToken)
            => _dateOfBirthRepository.Save(request.DateOfBirth, cancellationToken);
    }
}
