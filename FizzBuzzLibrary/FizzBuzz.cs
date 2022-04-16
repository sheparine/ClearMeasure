using FizzBuzzLibrary.Exceptions;
using FizzBuzzLibrary.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FizzBuzzLibrary
{
    public class FizzBuzz : IFizzBuzz
    {
        private readonly ILogger<FizzBuzz> _logger;
        private readonly FizzBuzzOptions _options;

        public FizzBuzz(ILogger<FizzBuzz> logger, IOptions<FizzBuzzOptions> options)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _options = options?.Value ?? throw new ArgumentNullException(nameof(options));

            ValidateOptions(_options);
        }

        public void ValidateOptions(FizzBuzzOptions fizzBuzzOptions)
        {
            // options not specified
            if(fizzBuzzOptions == null)
            {
                throw new ArgumentNullException(nameof(fizzBuzzOptions));
            }

            // number substititions null
            if(fizzBuzzOptions.NumberSubstitutions == null)
            {
                throw new ArgumentNullException(nameof(fizzBuzzOptions.NumberSubstitutions));
            }

            // number substititions empty
            if (!fizzBuzzOptions.NumberSubstitutions.Any())
            {
                throw new NumberSubstitutionsEmptyException();
            }

            // throw if user specified a duplicate number
            int numbersCount = fizzBuzzOptions.NumberSubstitutions.Select(ns => ns.Number).Count();
            int distinctNumbersCount = fizzBuzzOptions.NumberSubstitutions.Select(ns => ns.Number).Distinct().Count();
            if (distinctNumbersCount < numbersCount)
            {
                throw new DuplicateNumberException();
            }

            // throw if user did not specify substitution
            var hasAnyEmptySubstitutions = fizzBuzzOptions.NumberSubstitutions.Any(ns => ns.Substitution == "");
            if(hasAnyEmptySubstitutions)
            {
                throw new EmptySubstitutionException();
            }
        }

        public void Work(int limit)
        {
            if(limit <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(limit));
            }
            
            // work on numbers 1 to limit
            for (int i = 1; i <= limit; i++)
            {
                // perform number substitution
                var substitution = Evaluate(i);

                // log result
                _logger.LogInformation(substitution);
            }
        }

        public string Evaluate(int number)
        {
            // result of number substitutions
            string result = "";

            // loop all potential number substitutions
            foreach (var numberSubstitution in _options.NumberSubstitutions)
            {
                // if number matches, then substitute number for substitution text
                if (number % numberSubstitution.Number == 0)
                {
                    result += numberSubstitution.Substitution + " ";
                }
            }

            // trim off potential spacer
            result = result.TrimEnd();

            // if no matches found, then display number
            if (result == "")
            {
                result = number.ToString();
            }

            return result;
        }
    }
}