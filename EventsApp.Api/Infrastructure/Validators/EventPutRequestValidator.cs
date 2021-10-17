using EventsApp.Api.Models.Requests;
using FluentValidation;
using System;

namespace EventsApp.Mvc.Validators
{
    public class EventPutRequestValidator : AbstractValidator<EventPutRequest>
    {
        public EventPutRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .Length(5, 60);

            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(400);

            When(x => !x.IsArchived, () =>
            {
                RuleFor(x => x.PlannedAt)
                    .GreaterThan(x => DateTime.Now)
                    .WithMessage("There must be future date");
            });

            RuleFor(x => x.Address)
                .Length(5, 60);
        }
    }
}
