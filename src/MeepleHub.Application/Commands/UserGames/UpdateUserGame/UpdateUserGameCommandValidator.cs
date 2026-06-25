using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace MeepleHub.Application.Commands.UserGames.UpdateUserGame
{
    public class UpdateUserGameCommandValidator : AbstractValidator<UpdateUserGameCommand>
    {
        public UpdateUserGameCommandValidator() 
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.UserId).GreaterThan(0);
            RuleFor(x => x.PersonalRating).InclusiveBetween(1, 10).When(x => x.PersonalRating.HasValue);
            RuleFor(x => x.PurchasePrice).GreaterThanOrEqualTo(0).When(x => x.PurchasePrice.HasValue);
            RuleFor(x => x.SellingPrice).GreaterThanOrEqualTo(0).When(x => x.SellingPrice.HasValue);
            RuleFor(x => x.Status).IsInEnum();
            RuleFor(x => x.Condition).IsInEnum();
        }
    }
}
