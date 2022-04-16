using FizzBuzzLibrary;
using FizzBuzzLibrary.Exceptions;
using FizzBuzzLibrary.Options;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace TestProject
{
    public class FizzBuzzTests
    {
        private FizzBuzz? _fizzBuzz;

        [SetUp]
        public void Setup()
        {
            var logger = new NullLogger<FizzBuzz>();

            var numberSubstitutions = new List<NumberSubstitution>
            {
                new NumberSubstitution()
                {
                    Number = 3,
                    Substitution = "Fizz"
                },
                new NumberSubstitution()
                {
                    Number = 5,
                    Substitution = "Buzz"
                }
            };

            var options = Options.Create(new FizzBuzzOptions() {
                NumberSubstitutions = numberSubstitutions
            });

            _fizzBuzz = new FizzBuzz(logger, options);
        }

        [Test]
        public void TestIsNotNull()
        {
            Assert.IsNotNull(_fizzBuzz);
        }

        [Test]
        public void TestArgumentOutOfRangeException()
        {
            if(_fizzBuzz == null) throw new NullReferenceException(nameof(FizzBuzz));

            Assert.Throws<ArgumentOutOfRangeException>(() => _fizzBuzz.Work(-1));
        }

        [Test]
        public void TestDuplicateNumberException()
        {
            if (_fizzBuzz == null) throw new ArgumentOutOfRangeException(nameof(FizzBuzz));

            var options = new FizzBuzzOptions()
            {
                NumberSubstitutions = new List<NumberSubstitution>
                {
                    new NumberSubstitution()
                    {
                        Number = 3,
                        Substitution = "Fizz"
                    },
                    new NumberSubstitution()
                    {
                        Number = 3,
                        Substitution = "Buzz"
                    }
                }
            };

            Assert.Throws<DuplicateNumberException>(() => _fizzBuzz.ValidateOptions(options));
        }

        [Test]
        public void TestEmptySubstitutionException()
        {
            if (_fizzBuzz == null) throw new NullReferenceException(nameof(FizzBuzz));

            var options = new FizzBuzzOptions()
            {
                NumberSubstitutions = new List<NumberSubstitution>
                {
                    new NumberSubstitution()
                    {
                        Number = 3,
                        Substitution = "Fizz"
                    },
                    new NumberSubstitution()
                    {
                        Number = 5,
                        Substitution = ""
                    }
                }
            };

            Assert.Throws<EmptySubstitutionException>(() => _fizzBuzz.ValidateOptions(options));
        }

        [Test]
        public void TestNumberSubstitutionsEmptyException()
        {
            if (_fizzBuzz == null) throw new NullReferenceException(nameof(FizzBuzz));

            var options = new FizzBuzzOptions()
            {
                NumberSubstitutions = new List<NumberSubstitution>()
            };

            Assert.Throws<NumberSubstitutionsEmptyException>(() => _fizzBuzz.ValidateOptions(options));
        }

        [Test]
        [TestCase(1, "1")]
        [TestCase(3, "Fizz")]
        [TestCase(5, "Buzz")]
        [TestCase(15, "Fizz Buzz")]
        public void TestEvaluate(int number, string result)
        {
            if (_fizzBuzz == null) throw new NullReferenceException(nameof(FizzBuzz));

            Assert.True(_fizzBuzz.Evaluate(number) == result);
        }
    }
}