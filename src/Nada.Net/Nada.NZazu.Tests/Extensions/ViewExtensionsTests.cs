using FluentAssertions;
using Nada.NZazu.Contracts.Checks;
using Nada.NZazu.Extensions;
using NSubstitute;
using NUnit.Framework;

namespace Nada.NZazu.Tests.Extensions
{
    [TestFixture]
    // ReSharper disable InconsistentNaming
    internal class ViewExtensionsTests
    {
        [Test]
        public void Return_False_If_Validate_Has_Exception()
        {
            var view = Substitute.For<INZazuWpfView>();
            view.Validate().Returns(new ValueCheckResult(false, new Exception("I am invalid")));

            view.IsValid().Should().BeFalse();

            view.ReceivedWithAnyArgs().Validate();
        }

        [Test]
        public void Return_True_If_Validate()
        {
            var view = Substitute.For<INZazuWpfView>();
            view.Validate().Returns(ValueCheckResult.Success);

            view.IsValid().Should().BeTrue();

            view.ReceivedWithAnyArgs().Validate();
        }
    }
}