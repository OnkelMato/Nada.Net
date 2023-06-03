using System.Windows.Controls;
using FluentAssertions;
using Nada.NZazu.Extensions;
using NUnit.Framework;

namespace Nada.NZazu.Tests.Extensions
{
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    // ReSharper disable InconsistentNaming
    public class FocusExtensionsTests
    {
        [Test]
        [STAThread]
        public void SetFocus()
        {
            var control = new TextBox();
            control.IsFocused.Should().BeFalse();

            control.SetFocus();

            control.IsFocused.Should().BeTrue();
        }
    }
}