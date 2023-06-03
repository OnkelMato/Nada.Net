using FluentAssertions;
using Nada.NZazu.Contracts.Checks;
using NUnit.Framework;

namespace Nada.NZazu.Contracts.Tests.Checks
{
    [TestFixture]
    // ReSharper disable InconsistentNaming
    public class ValueCheckResultTests
    {
        [Test]
        public void Be_Creatable()
        {
            var sut = new ValueCheckResult(true);
            sut.IsValid.Should().BeTrue();
            sut.Exception.Should().BeNull();
        }

        [Test]
        public void Be_Creatable_With_Exception()
        {
            var exception = new InvalidCastException();
            var sut = new ValueCheckResult(false, exception);
            sut.IsValid.Should().BeFalse();
            sut.Exception.Should().Be(exception);
        }

        [Test]
        public void Be_Creatable_With_Exception_Only()
        {
            var exception = new InvalidCastException();
            var sut = new ValueCheckResult(exception);
            sut.IsValid.Should().BeFalse();
            sut.Exception.Should().Be(exception);
        }
    }
}