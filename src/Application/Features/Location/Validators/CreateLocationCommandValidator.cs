using Application.Features.Location.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Location.Validators
{
    public sealed class CreateLocationCommandValidator : AbstractValidator<CreateLocationCommand>
    {
        public CreateLocationCommandValidator()
        {
            RuleFor(x => x.UserId)
                 .GreaterThan(0)
                 .WithMessage("UserId must be greater than 0");

            RuleFor(x => x.Latitude)
                 .InclusiveBetween(-90, 90)
                 .WithMessage("Latitude must be between -90 and 90 degrees");

            RuleFor(x => x.Longitude)
                .InclusiveBetween(-180, 180)
                .WithMessage("Longitude must be between -180 and 180 degrees");
        }
    }
}
