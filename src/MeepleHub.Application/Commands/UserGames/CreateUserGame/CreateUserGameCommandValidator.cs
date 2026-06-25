using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace MeepleHub.Application.Commands.UserGames.CreateUserGame
{
    public class CreateUserGameCommandValidator : AbstractValidator<CreateUserGameCommand>
    {
        public CreateUserGameCommandValidator() 
        {
            RuleFor(x => x.UserId).GreaterThan(0);
            RuleFor(x => x.GameId).GreaterThan(0);
            RuleFor(x => x.PersonalRating).InclusiveBetween(1, 10).When(x => x.PersonalRating.HasValue);
            RuleFor(x => x.PurchasePrice).GreaterThanOrEqualTo(0).When(x => x.PurchasePrice.HasValue);
            RuleFor(x => x.SellingPrice).GreaterThanOrEqualTo(0).When(x => x.SellingPrice.HasValue);
            RuleFor(x => x.Status).IsInEnum();
            RuleFor(x => x.Condition).IsInEnum();
        }
    }
}
