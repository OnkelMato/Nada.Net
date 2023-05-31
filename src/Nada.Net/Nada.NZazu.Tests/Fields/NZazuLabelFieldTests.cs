using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using FluentAssertions;
using Nada.NZazu.Contracts;
using Nada.NZazu.Extensions;
using Nada.NZazu.Fields;
using NUnit.Framework;

namespace Nada.NZazu.Tests.Fields
{
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    // ReSharper disable InconsistentNaming
    internal class NZazuLabelFieldTests
    {
        [ExcludeFromCodeCoverage]
        private object ServiceLocator(Type type)
        {
            if (type == typeof(IValueConverter)) return NoExceptionsConverter.Instance;
            if (type == typeof(IFormatProvider)) return CultureInfo.InvariantCulture;
            throw new NotSupportedException($"Cannot lookup {type.Name}");
        }

        [Test]
        [STAThread]
        public void Create_ValueControl_Matching_Description()
        {
            var sut = new NZazuLabelField(new FieldDefinition {Key = "key", Description = "superhero is alive"},
                ServiceLocator);

            var label = (Label) sut.ValueControl;
            label.Should().NotBeNull();
            label.Content.Should().Be(sut.Definition.Description);
        }

        [Test]
        public void Not_Create_ValueControl_On_Empty_Description()
        {
            var sut = new NZazuLabelField(new FieldDefinition {Key = "key"}, ServiceLocator);
            sut.Definition.Description.Should().BeNullOrWhiteSpace();
            var label = (Label) sut.ValueControl;
            label.Should().BeNull();
        }

        [Test]
        public void Return_null_StringValue_and_not_set_StringValue()
        {
            var sut = new NZazuLabelField(new FieldDefinition {Key = "key"}, ServiceLocator);
            sut.GetValue().Should().BeNull();
            sut.SetValue("foobar");
            sut.GetValue().Should().BeNull();
        }

        [Test]
        public void Not_be_Editable()
        {
            var sut = new NZazuLabelField(new FieldDefinition {Key = "key"}, ServiceLocator);
            sut.IsEditable.Should().BeFalse();
        }
    }
}