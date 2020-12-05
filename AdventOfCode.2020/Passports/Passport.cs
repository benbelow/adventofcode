using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentValidation.Results;

namespace AdventOfCode._2020.Passports
{
    public class Passport
    {
        private static readonly Dictionary<string, FieldInfo> PropertiesByPassportKey = typeof(Passport)
            .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
            .Where(m => m.CustomAttributes.Any(x => x.AttributeType == typeof(PassportKeyAttribute)))
            .ToDictionary(
                m => m.CustomAttributes.Single(x => x.AttributeType == typeof(PassportKeyAttribute)).ConstructorArguments.Single().Value as string,
                m => m
            );

        [PassportKey("byr")]
        private string birthYearString;

        public int? BirthYear => int.TryParse(birthYearString, out var year) ? year as int? : null;

        [PassportKey("iyr")]
        private string issueYearString;

        public int? IssueYear => int.TryParse(issueYearString, out var year) ? year as int? : null;

        [PassportKey("eyr")]
        private string expirationYearString;

        public int? ExpirationYear => int.TryParse(expirationYearString, out var year) ? year as int? : null;

        [PassportKey("hgt")]
        private string heightString;

        /// <summary>
        /// Only set if source data specified in cm - will not convert from inches
        /// </summary>
        public int? HeightInCm => int.TryParse(heightString?.Split("cm").FirstOrDefault(), out var height) ? height as int? : null;

        /// <summary>
        /// Only set if source data specified in inches - will not convert from cm
        /// </summary>
        public int? HeightInInches => int.TryParse(heightString?.Split("in").FirstOrDefault(), out var height) ? height as int? : null;

        [PassportKey("hcl")]
        private string hairColorString;

        public string HairColor => hairColorString;

        [PassportKey("ecl")]
        private string eyeColorString;

        public string EyeColor => eyeColorString;

        [PassportKey("pid")]
        private string passportIdString;

        public string PassportId => passportIdString;

        [PassportKey("cid")]
        private string countryIdString;

        public string CountryId => countryIdString;

        public Passport(IEnumerable<string> inputLines)
        {
            var keyValuePairs = inputLines.SelectMany(l => l.Split()).Select(i =>
            {
                var splitStrings = i.Split(":");
                return new KeyValuePair<string, string>(splitStrings[0], splitStrings[1]);
            });

            foreach (var (key, value) in keyValuePairs)
            {
                var field = PropertiesByPassportKey.GetValueOrDefault(key);
                if (field == null)
                {
                    Console.WriteLine($"Unexpected property found: {key}: Ignoring");
                }

                field?.SetValue(this, value);
            }
        }

        public ValidationResult Validate() {
            var requiredFieldsResult = new PassportRequiredFieldsValidator().Validate(this);
            return !requiredFieldsResult.IsValid ? requiredFieldsResult : new PassportValidator().Validate(this);
        }
        
        public bool IsValid() => HasAllRequiredFields() && new PassportValidator().Validate(this).IsValid;

        public bool HasAllRequiredFields() => new PassportRequiredFieldsValidator().Validate(this).IsValid;
    }
}