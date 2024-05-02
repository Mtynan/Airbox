using Application.Features.Location.Queries;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Location.Validators
{
    public sealed class GetMostRecentLocationQueryValidator : AbstractValidator<GetMostRecentLocationQuery>
    {
        public GetMostRecentLocationQueryValidator()
        {
            RuleFor(x => x.UserId)
                .GreaterThan(0)
                .WithMessage("UserId must be greater than 0");
        }
    }
}
