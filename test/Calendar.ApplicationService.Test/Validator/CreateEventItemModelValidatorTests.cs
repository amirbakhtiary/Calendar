using Calendar.Endpoints.WebAPI.Models;
using Calendar.Endpoints.WebAPI.Validators;
using FluentValidation.TestHelper;
using System;
using Xunit;

namespace Calendar.Endpoints.WebAPI.Test.Validator
{
    public class CreateEventItemModelValidatorTests
    {
        private readonly CreateEventItemModelValidator _testee;
        public CreateEventItemModelValidatorTests()
        {
            _testee = new CreateEventItemModelValidator();
        }

        [Theory]
        [InlineData("")]
        [InlineData("a")]
        public void EventName_WhenShorterThanTwoCharacter_ShouldHaveValidationError(string eventName)
        {
            var model = new CreateEventItemModel { Name = eventName };

            var result = _testee.TestValidate(model);
            result.ShouldHaveValidationErrorFor(m => m.Name);
        }

        [Theory]
        [InlineData("")]
        [InlineData("a")]
        public void Location_WhenShorterThanTwoCharacter_ShouldHaveValidationError(string location)
        {
            var model = new CreateEventItemModel { Location = location };

            var result = _testee.TestValidate(model);
            result.ShouldHaveValidationErrorFor(m => m.Location);
        }

        [Theory]
        [MemberData(nameof(EventTimeTestData.InvalidTestData), MemberType = typeof(EventTimeTestData))]
        public void EventTime_WhenLongerAfterThan180DaysOrInThePastOrNull_ShouldHaveValidationError(DateTime eventTime)
        {
            var model = new CreateEventItemModel { Time = eventTime };

            var result = _testee.TestValidate(model);
            result.ShouldHaveValidationErrorFor(person => person.Time);
        }

        [Theory]
        [MemberData(nameof(EventTimeTestData.ValidTestData), MemberType = typeof(EventTimeTestData))]
        public void EventTime_WhenBetween0And180_ShouldNotHaveValidationError(DateTime eventTime)
        {
            var model = new CreateEventItemModel { Time = eventTime };

            var result = _testee.TestValidate(model);
            result.ShouldHaveValidationErrorFor(person => person.Time);
        }
    }
}
