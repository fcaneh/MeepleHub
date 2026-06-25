using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace MeepleHub.Application.Commands.Games.CreateGame
{
    public class CreateGameCommandValidator : AbstractValidator<CreateGameCommand>
    {
        public CreateGameCommandValidator() 
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.MinPlayers).GreaterThan(0);
            RuleFor(x => x.MaxPlayers).GreaterThanOrEqualTo(x => x.MinPlayers);
            RuleFor(x => x.MinPlayTime).GreaterThan(0);
            RuleFor(x => x.MaxPlayTime).GreaterThanOrEqualTo(x => x.MinPlayTime);
            RuleFor(x => x.PublishedYear).InclusiveBetween(1800, DateTime.UtcNow.Year).When(x => x.PublishedYear.HasValue);
            RuleFor(x => x.Complexity).InclusiveBetween(0, 5);
        }
    }
}
