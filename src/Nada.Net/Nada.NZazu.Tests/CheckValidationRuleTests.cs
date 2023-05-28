﻿using System.Globalization;
using FluentAssertions;
using Nada.NZazu.Contracts.Checks;
using NSubstitute;
using NUnit.Framework;

namespace Nada.NZazu.Tests
{
    [TestFixture]
    // ReSharper disable InconsistentNaming
    internal class CheckValidationRuleTests
    {
        [Test]
        public void Delegate_validation_to_check()
        {
            var check = Substitute.For<IValueCheck>();

            var sut = new CheckValidationRule(check);

            // validation is true
            const string input = "foobar";
            var cultureInfo = CultureInfo.InvariantCulture;
            check.Validate(input, input, cultureInfo).Returns(ValueCheckResult.Success);
            var result = sut.Validate(input, cultureInfo);
            result.IsValid.Should().BeTrue();
            result.ErrorContent.Should().Be(string.Empty);

            // validation is false
            var error = new ValueCheckResult(false, new Exception("test"));
            check.Validate(input, input, cultureInfo).Returns(error);
            result = sut.Validate(input, cultureInfo);
            result.IsValid.Should().BeFalse();
            result.ErrorContent.Should().Be(error.Exception.Message);

            var exception = new ArgumentException("test");
            error = new ValueCheckResult(false, exception);
            check.Validate(input, input, cultureInfo).Returns(error);
            result = sut.Validate(input, cultureInfo);
            result.IsValid.Should().BeFalse();
            result.ErrorContent.Should().Be(exception.Message);
        }
    }
}