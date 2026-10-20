using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace MeepleHub.Application.Queries.Games.GetGames
{
    public class GetGamesQueryValidator : AbstractValidator<GetGamesQuery>
    {
        public GetGamesQueryValidator()
        {
            RuleFor(x => x.MinPlayersFilter).GreaterThan(0).When(x => x.MinPlayersFilter.HasValue);
            RuleFor(x => x.MaxPlayersFilter).GreaterThan(0).When(x => x.MaxPlayersFilter.HasValue);
            RuleFor(x => x.NumberPlayersFilter).GreaterThan(0).When(x => x.NumberPlayersFilter.HasValue);
            RuleFor(x => x.NumberPlayersFilter).Null().When(x => x.MinPlayersFilter.HasValue || x.MaxPlayersFilter.HasValue).WithMessage("Use either NumberPlayersFilter or MinPlayersFilter/MaxPlayersFilter.");
            RuleFor(x => x.MaxPlayersFilter).GreaterThanOrEqualTo(x => x.MinPlayersFilter).When(x => x.MinPlayersFilter.HasValue && x.MaxPlayersFilter.HasValue);
            RuleForEach(x => x.CategoryIds).GreaterThan(0).WithMessage("Category IDs must be greater than 0");
            RuleForEach(x => x.MechanicIds).GreaterThan(0);
            RuleForEach(x => x.PublisherIds).GreaterThan(0);
            RuleForEach(x => x.DesignerIds).GreaterThan(0);
            RuleFor(x => x.YoungestPlayerAge).GreaterThan(0);
        }
    }
}
