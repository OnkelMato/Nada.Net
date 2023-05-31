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
    internal class NZazuGroupFieldTests
    {
        [ExcludeFromCodeCoverage]
        private object ServiceLocator(Type type)
        {
            if (type == typeof(IValueConverter)) return NoExceptionsConverter.Instance;
            if (type == typeof(IFormatProvider)) return CultureInfo.InvariantCulture;
            if (type == typeof(INZazuWpfFieldFactory)) return new NZazuFieldFactory();
            throw new NotSupportedException($"Cannot lookup {type.Name}");
        }

        [Test]
        public void Be_Creatable()
        {
            var definition = new FieldDefinition {Key = "key", Type = "group"};
            var sut = new NZazuGroupField(definition, ServiceLocator);
            sut.ApplySettings(definition);
            sut.Should().NotBeNull();

            sut.Should().BeAssignableTo<INZazuWpfField>();
            sut.Should().BeAssignableTo<INZazuWpfFieldContainer>();

            // at least nobodyd cares about these values
            sut.GetValue().Should().BeNullOrEmpty();
            sut.SetValue("foo");
            sut.GetValue().Should().BeNullOrEmpty();
        }

        [Test]
        public void Not_support_direct_binding_or_validation()
        {
            var sut = new NZazuGroupField(new FieldDefinition {Key = "key"}, ServiceLocator);
            sut.ContentProperty.Should().Be(null);
        }

        [Test]
        [STAThread]
        public void Create_ContentControl()
        {
            var sut = new NZazuGroupField(new FieldDefinition {Key = "key"}, ServiceLocator);
            var contentControl = (ContentControl) sut.ValueControl;

            contentControl.Should().NotBeNull();

            contentControl.Focusable.Should().BeFalse("group fields should not have a tab stop of their own");
        }

        [Test]
        [STAThread]
        public void Create_Groupbox()
        {
            var sut = new NZazuGroupField(new FieldDefinition {Key = "key", Description = "Header"}, ServiceLocator);
            var contentControl = (GroupBox) sut.ValueControl;

            contentControl.Should().NotBeNull();

            contentControl.Focusable.Should().BeFalse("group fields should not have a tab stop of their own");
        }

        [Test]
        public void Not_be_Editable()
        {
            var sut = new NZazuGroupField(new FieldDefinition {Key = "test"}, ServiceLocator);
            sut.IsEditable.Should().BeFalse();
        }
    }
}