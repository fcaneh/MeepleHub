using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace MeepleHub.Application.Commands.Users.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator() 
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
        }
    }
}
