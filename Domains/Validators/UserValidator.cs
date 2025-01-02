using Domains.ViewModels.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.Validators
{
    public class UserValidator : AbstractValidator<users>
    {
        public UserValidator()
        {
            RuleSet("Required", () =>
            {
                RuleFor(x => x.FirstName).NotEmpty().WithMessage("FirstName is required!");
                RuleFor(x => x.FirstName).NotNull().WithMessage("FirstName is required!");
                RuleFor(x => x.LastName).NotEmpty().WithMessage("LastName is required!");
                RuleFor(x => x.LastName).NotNull().WithMessage("LastName is required!");
                RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required!");
                RuleFor(x => x.Email).NotNull().WithMessage("Email is required!");
                RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required!");
                RuleFor(x => x.Password).NotNull().WithMessage("Password is required!");
            });
            RuleSet("Length", () =>
            {
                RuleFor(x => x.FirstName).Length(3, 50).WithMessage("Invalid length for FirstName!");
                RuleFor(x => x.LastName).Length(3, 50).WithMessage("Invalid length for LastName!");
                RuleFor(x => x.Password).MinimumLength(8).WithMessage("Invalid length for Password!");
            });
            RuleSet("RegX", () =>
            {
                RuleFor(x => x.Password)
                .Matches(@"^(?=.*[!@#$%^&*(),.?""{}|<>])(?=.*[A-Z])(?=.*[a-z])(?=.*\d).{8,}$")
                .WithMessage("Password must be valid.");
                RuleFor(x => x.Email)
                .Matches(@"^[^\s@]+@[^\s@]+\.[^\s@]+$")
                .WithMessage("Please provide a valid Email.");

            });

        }
    }
}
