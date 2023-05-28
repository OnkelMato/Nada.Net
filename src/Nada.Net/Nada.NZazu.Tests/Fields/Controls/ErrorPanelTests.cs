using System.Windows.Controls;
using FluentAssertions;
using Nada.NZazu.Fields.Controls;
using NUnit.Framework;

namespace Nada.NZazu.Tests.Fields.Controls
{
    [TestFixture]
    // ReSharper disable once InconsistentNaming
    internal class ErrorPanelTests
    {
        [Test]
        [Apartment(ApartmentState.STA)]
        public void Be_A_Control()
        {
            var sut = new ErrorPanel();

            sut.Should().NotBeNull();
            sut.Should().BeAssignableTo<UserControl>();
        }
    }
}