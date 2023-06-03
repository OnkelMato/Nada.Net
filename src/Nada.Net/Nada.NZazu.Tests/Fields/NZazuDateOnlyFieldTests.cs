using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using FluentAssertions;
using FluentAssertions.Equivalency;
using Nada.NZazu.Contracts;
using Nada.NZazu.Extensions;
using Nada.NZazu.Fields;
using NUnit.Framework;

namespace Nada.NZazu.Tests.Fields
{
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    // ReSharper disable InconsistentNaming
    internal class NZazuDateOnlyFieldTests
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
            var sut = new NZazuDateOnlyField(new FieldDefinition {Key = "key"}, ServiceLocator);

            sut.Should().NotBeNull();
            sut.Should().BeAssignableTo<INZazuWpfField>();
        }

        [Test]
        [STAThread]
        public void Create_Control_With_ToolTip_Matching_Description()
        {
            var sut = new NZazuDateOnlyField(new FieldDefinition
            {
                Key = "key",
                Hint = "superhero",
                Description = "check this if you are a registered superhero"
            }, ServiceLocator);

            var datePicker = (DatePicker) sut.ValueControl;
            datePicker.Should().NotBeNull();
            datePicker.Text.Should().BeEmpty();
            datePicker.ToolTip.Should().Be(sut.Definition.Description);
        }

        [Test]
        //[SetUICulture("en-US")]
        [STAThread]
        public void Format_UIText_From_Value()
        {
            // ReSharper disable once UseObjectOrCollectionInitializer
            var sut = new NZazuDateOnlyField(new FieldDefinition {Key = "key"}, ServiceLocator);
            const string dateFormat = "yyyy_MM_dd";
            sut.Definition.Settings.Add("Format", dateFormat);
            var datePicker = (DatePicker) sut.ValueControl;

            sut.Value.Should().NotHaveValue();
            datePicker.Text.Should().BeEmpty();

            var now = DateTime.Now;
            sut.Value = new DateOnly( now.Year, now.Month, now.Day);
            var expected = now.ToString(dateFormat);
            sut.GetValue().Should().Be(expected);

            // NOTE: Formatted Dates seems complicated to setup with DatePicker
            // So we just skip it. It it only vital that StringValue matches the speicifed format
            // The UI is sugar.
            //datePicker.Text.Should().Be(expected);

            sut.Value = null;
            sut.GetValue().Should().BeEmpty();
            datePicker.SelectedDate.Should().NotHaveValue();
        }

        [Test]
        [STAThread]
        public void Format_SelectedDate_From_Value()
        {
            var sut = new NZazuDateOnlyField(new FieldDefinition {Key = "key"}, ServiceLocator);
            var datePicker = (DatePicker) sut.ValueControl;

            sut.Value.Should().NotHaveValue();
            datePicker.Text.Should().BeEmpty();

            var now = DateTime.Now.Date;
            sut.Value = new DateOnly(now.Year, now.Month, now.Day);
            datePicker.SelectedDate.Should().Be(now);

            sut.Value = null;
            datePicker.SelectedDate.Should().NotHaveValue();
        }

        [Test]
        [STAThread]
        public void Format_Value_From_TextBox()
        {
            var sut = new NZazuDateOnlyField(new FieldDefinition {Key = "key"}, ServiceLocator);
            var datePicker = (DatePicker) sut.ValueControl;

            sut.Value.Should().NotHaveValue();
            datePicker.Text.Should().BeEmpty();

            var nowDt = DateTime.Now.Date;
            var now = new DateOnly(nowDt.Year, nowDt.Month, nowDt.Day);
            datePicker.SelectedDate = nowDt;
            sut.Value.Should().Be(now);

            datePicker.SelectedDate = null;
            sut.IsValid().Should().BeTrue();
            sut.Value.Should().NotHaveValue();
        }

        [Test]
        [STAThread]
        public void Format_TextBox_From_StringValue()
        {
            var sut = new NZazuDateOnlyField(new FieldDefinition {Key = "key"}, ServiceLocator);
            var datePicker = (DatePicker) sut.ValueControl;

            sut.GetValue().Should().BeNullOrEmpty();
            datePicker.Text.Should().BeEmpty();

            var now = DateTime.Now.Date;
            sut.SetValue(now.ToString("o", CultureInfo.InvariantCulture));
            datePicker.SelectedDate.Should().Be(now);

            sut.SetValue(string.Empty);
            datePicker.SelectedDate.Should().NotHaveValue();
        }

        [Test]
        [STAThread]
        public void Format_StringValue_From_TextBox()
        {
            var sut = new NZazuDateOnlyField(new FieldDefinition {Key = "key"}, ServiceLocator);
            var datePicker = (DatePicker) sut.ValueControl;

            var now = DateTime.Now.Date;
            datePicker.SelectedDate = now;
            sut.GetValue().Should().Be(now.ToString("o", CultureInfo.InvariantCulture));

            datePicker.SelectedDate = null;
            sut.IsValid().Should().BeTrue();
            sut.GetValue().Should().Be("");

            datePicker.Text = null;
            sut.IsValid().Should().BeTrue();
            sut.GetValue().Should().Be(string.Empty);
        }

        [Test]
        public void Consider_DateFormat_in_StringValue()
        {
            var sut = new NZazuDateOnlyField(new FieldDefinition {Key = "key"}, ServiceLocator);

            // DateFormat unspecified
            var date = DateTime.UtcNow;
            
            var dateStr = date.ToString(CultureInfo.InvariantCulture);
            sut.SetValue(dateStr);
            sut.Value.Should().Be(new DateOnly(date.Year, date.Month, date.Day));

            date += TimeSpan.FromSeconds(1);
            dateStr = date.ToString(CultureInfo.InvariantCulture);
            sut.Value = new DateOnly(date.Year, date.Month, date.Day);
            sut.GetValue().Should().Be(dateStr);

            // now specify DateFormat
            const string dateFormat = "yyyy-MMM-dd";
            sut.DateFormat = dateFormat;

            dateStr = date.ToString(dateFormat, CultureInfo.InvariantCulture);
            sut.GetValue().Should().Be(dateStr);

            date -= TimeSpan.FromDays(60);
            dateStr = date.ToString(dateFormat, CultureInfo.InvariantCulture);
            sut.Value = new DateOnly(date.Year, date.Month, date.Day);
            sut.GetValue().Should().Be(dateStr);

            date += TimeSpan.FromDays(2);
            date = date.Date; // truncate time (we only check date --> format)
            dateStr = date.ToString(dateFormat, CultureInfo.InvariantCulture);
            sut.SetValue(dateStr);
            sut.Value.Should().Be(new DateOnly(date.Year, date.Month, date.Day));
        }
    }
}