using EventsApp.Api.Models.Requests;
using FluentValidation;
using System;

namespace EventsApp.Api.Infrastructure.Validators
{
    public class EventRequestValidator : AbstractValidator<EventRequest>
    {
        public EventRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .Length(5, 60);

            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(400);

            RuleFor(x => x.PlannedAt)
                .GreaterThan(x => DateTime.Now)
                .WithMessage("There must be future date");


            RuleFor(x => x.Address)
                .Length(5, 60);
        }
    }
}
