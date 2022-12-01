using System.Collections.Generic;
using System.Text.RegularExpressions;
using FluentValidation;

namespace AdventOfCode._2020.Passports
{
    internal class PassportRequiredFieldsValidator : AbstractValidator<Passport>
    {
        public PassportRequiredFieldsValidator()
        {
            RuleFor(p => p.BirthYear).NotNull();

            RuleFor(p => p.IssueYear).NotNull();

            RuleFor(p => p.ExpirationYear).NotNull();

            RuleFor(p => p.HeightInCm)
                .NotNull()
                .When(p => p.HeightInInches == null);

            RuleFor(p => p.HeightInInches)
                .NotNull()
                .When(p => p.HeightInCm == null);
            
            RuleFor(p => p.HairColor).NotNull();

            RuleFor(p => p.EyeColor).NotNull();

            RuleFor(p => p.PassportId).NotNull();
        }
    }

    internal class PassportValidator : AbstractValidator<Passport>
    {
        private static readonly IList<string> ValidEyeColors = new[] {"amb", "blu", "brn", "gry", "grn", "hzl", "oth"};

        public PassportValidator()
        {
            RuleFor(p => p.BirthYear)
                .NotNull()
                .InclusiveBetween(1920, 2002);

            RuleFor(p => p.IssueYear)
                .NotNull()
                .InclusiveBetween(2010, 2020);

            RuleFor(p => p.ExpirationYear)
                .NotNull()
                .InclusiveBetween(2020, 2030);

            RuleFor(p => p.HeightInCm)
                .NotNull()
                .When(p => p.HeightInInches == null);

            RuleFor(p => p.HeightInCm)
                .InclusiveBetween(150, 193)
                .When(p => p.HeightInCm != null);

            RuleFor(p => p.HeightInInches)
                .NotNull()
                .When(p => p.HeightInCm == null);

            RuleFor(p => p.HeightInInches)
                .InclusiveBetween(59, 76)
                .When(p => p.HeightInInches != null);

            RuleFor(p => p.HairColor)
                .NotNull()
                .Must(hcl => Regex.IsMatch(hcl, "^#[0-9a-z]{6}$"));

            RuleFor(p => p.EyeColor)
                .NotNull()
                .Must(ecl => ValidEyeColors.Contains(ecl));

            RuleFor(p => p.PassportId)
                .NotNull()
                .Must(pid => pid?.Length == 9)
                .Must(pid => Regex.IsMatch(pid, "[0-9]"));
        }
    }
}