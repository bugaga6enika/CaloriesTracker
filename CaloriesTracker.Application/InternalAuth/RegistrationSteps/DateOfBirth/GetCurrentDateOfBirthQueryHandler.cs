﻿using CaloriesTracker.Domain.User.RegistrationSteps;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CaloriesTracker.Application.User.RegistrationSteps.DateOfBirth
{
    public class GetCurrentDateOfBirthQueryHandler : IRequestHandler<GetCurrentDateOfBirthQuery, DateTimeOffset>
    {
        private readonly IDateOfBirthRepository _dateOfBirthRepository;

        public GetCurrentDateOfBirthQueryHandler(IDateOfBirthRepository dateOfBirthRepository)
        {
            _dateOfBirthRepository = dateOfBirthRepository;
        }

        public Task<DateTimeOffset> Handle(GetCurrentDateOfBirthQuery request, CancellationToken cancellationToken)
            => _dateOfBirthRepository.GetCurrent();
    }
}
