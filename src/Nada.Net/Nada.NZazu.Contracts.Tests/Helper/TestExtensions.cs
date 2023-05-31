using System.Globalization;
using FluentAssertions;
using Nada.NZazu.Contracts.Checks;

namespace Nada.NZazu.Contracts.Tests.Helper
{
    internal static class TestExtensions
    {
        public static void ShouldFailWith<TError>(this IValueCheck check, string value, object parsedValue,
            Predicate<TError> matchError = null)
            where TError : Exception
        {
            var vr = check.Validate(value, parsedValue, CultureInfo.CurrentCulture);
            vr.IsValid.Should().BeFalse();
            var error = (TError) vr.Exception;
            error.Should().NotBeNull();
            matchError?.Invoke(error).Should().BeTrue();
        }

        public static void ShouldPass(this IValueCheck check, object parsedValue, string value)
        {
            var vr = check.Validate(value, parsedValue, CultureInfo.CurrentCulture);
            vr.IsValid.Should().BeTrue();
            vr.Exception.Should().BeNull();
        }
    }
}