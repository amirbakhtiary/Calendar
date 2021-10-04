using Calendar.Endpoints.WebAPI.Models;
using FluentValidation;
using System;

namespace Calendar.Endpoints.WebAPI.Validators
{
    public class CreateEventItemModelValidator : AbstractValidator<CreateEventItemModel>
    {
        public CreateEventItemModelValidator()
        {
            RuleFor(x => x.Name)
                .MinimumLength(2).MaximumLength(250)
                .WithMessage("The event name must between 2 and 250 character");

            RuleFor(x => x.Location)
                .MinimumLength(2).MaximumLength(120)
                .WithMessage("The location must between 2 and 120 character");

            RuleFor(x => x.EventOrganizer)
                .MinimumLength(2).MaximumLength(120)
                .WithMessage("The eventOrganizer must between 2 and 120 character");

            RuleFor(x => x.Time)
                .InclusiveBetween(DateTime.Now, DateTime.Now.AddDays(180).Date)
                .WithMessage("The event time must not be longer after than 180 days and can not be in the past");

            RuleForEach(x => x.Members)
                .MinimumLength(2).MaximumLength(120)
                .WithMessage("The member name must between 2 and 120 character");
        }
    }
}
