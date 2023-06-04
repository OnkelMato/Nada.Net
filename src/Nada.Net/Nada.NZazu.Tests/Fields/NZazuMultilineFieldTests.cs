using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using FluentAssertions;
using Nada.NZazu.Contracts;
using Nada.NZazu.Extensions;
using Nada.NZazu.Fields;
using NUnit.Framework;

namespace Nada.NZazu.Tests.Fields;

[TestFixture]
[Apartment(ApartmentState.STA)]
// ReSharper disable InconsistentNaming
public class NZazuMultilineFieldTests
{
    [ExcludeFromCodeCoverage]
    private object ServiceLocator(Type type)
    {
        if (type == typeof(IValueConverter)) return NoExceptionsConverter.Instance;
        if (type == typeof(IFormatProvider)) return CultureInfo.InvariantCulture;
        throw new NotSupportedException($"Cannot lookup {type.Name}");
    }

    [Test]
    public void Be_Creatable()
    {
        var sut = new NZazuMultilineField(new FieldDefinition { Key = "test" }, ServiceLocator);

        sut.Should().NotBeNull();
        sut.Should().BeAssignableTo<INZazuWpfField>();
    }

    [Test]
    [STAThread]
    public void Create_TextBox_with_ToolTip_Matching_Description()
    {
        var sut = new NZazuMultilineField(new FieldDefinition
        {
            Key = "test",
            Hint = "superhero",
            Description = "check this if you are a registered superhero"
        }, ServiceLocator);

        var textBox = (TextBox)sut.ValueControl;
        textBox.Should().NotBeNull();
        textBox.Text.Should().BeEmpty();
        textBox.AcceptsReturn.Should().Be(true);
        textBox.VerticalAlignment.Should().Be(VerticalAlignment.Stretch);
        textBox.HorizontalAlignment.Should().Be(HorizontalAlignment.Stretch);
        textBox.MinHeight.Should().Be(85);
        textBox.ToolTip.Should().Be(sut.Definition.Description);
    }

    [Test]
    [STAThread]
    public void Create_ValueControl_Even_If_Empty_Hint()
    {
        var sut = new NZazuMultilineField(new FieldDefinition { Key = "test" }, ServiceLocator);

        var textBox = (TextBox)sut.ValueControl;
        textBox.Should().NotBeNull();
        textBox.Text.Should().BeEmpty();
    }

    [Test]
    [STAThread]
    public void Get_Set_Value_should_propagate_to_ValueControl_Without_LostFocus()
    {
        var sut = new NZazuMultilineField(new FieldDefinition { Key = "test" }, ServiceLocator);
        var textBox = (TextBox)sut.ValueControl;
        textBox.Should().NotBeNull();

        sut.GetValue().Should().BeNull();
        textBox.Text.Should().BeNullOrEmpty();

        sut.SetValue("test");
        sut.GetValue().Should().Be("test");
        textBox.Text.Should().Be("test");

        textBox.Text = string.Empty;
        sut.GetValue().Should().BeNullOrEmpty();
    }
}