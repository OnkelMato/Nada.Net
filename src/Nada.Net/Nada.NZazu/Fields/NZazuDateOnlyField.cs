using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using Nada.NZazu.Contracts;
using Nada.NZazu.Fields.Controls;

namespace Nada.NZazu.Fields;

public class NZazuDateOnlyField : NZazuField<DateOnly?>
{
    public NZazuDateOnlyField(FieldDefinition definition, Func<Type, object> serviceLocatorFunc)
        : base(definition, serviceLocatorFunc)
    {
    }

    /// <summary>
    /// Format to display the date in the UI
    /// </summary>
    public string DateUIFormat { get; protected internal set; }

    /// <summary>
    /// Format to serialize and deserialize the date
    /// </summary>
    public string DateInternalFormat { get; protected internal set; }

    public override DependencyProperty ContentProperty => DateOnlyPicker.ValueProperty;

    protected override Control CreateValueControl()
    {
        DateUIFormat = Definition.Settings.Get("UiFormat", CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern);
        DateInternalFormat = Definition.Settings.Get("InternalFormat", "o");
        return new DateOnlyPicker
        {
            ToolTip = Definition.Description,
        };
    }

    public override void SetValue(string value)
    {
        var parsed = false;
        var parsedDt = new DateTime();

        if (!string.IsNullOrWhiteSpace(value))
        {
            const DateTimeStyles dateTimeStyles = DateTimeStyles.AssumeLocal;
            parsed = string.IsNullOrWhiteSpace(DateInternalFormat)
                ? DateTime.TryParse(value, FormatProvider, dateTimeStyles, out parsedDt)
                : DateTime.TryParseExact(value, DateInternalFormat, FormatProvider, dateTimeStyles, out parsedDt);
        }

        if (parsed)
            Value = new DateOnly(parsedDt.Year, parsedDt.Month, parsedDt.Day);
        else
            Value = null;
    }

    public override string GetValue()
    {
        if (!Value.HasValue) return string.Empty;

        var dateOnly = Value.Value;
        if (string.IsNullOrWhiteSpace(DateInternalFormat))
            return dateOnly.ToString(FormatProvider);
        return dateOnly.ToDateTime(new TimeOnly(0,0)).ToString(DateInternalFormat, FormatProvider);
    }
}