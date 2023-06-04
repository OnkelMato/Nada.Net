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

    public string DateFormat { get; protected internal set; }

    public override DependencyProperty ContentProperty => DateOnlyPicker.ValueProperty;

    protected override Control CreateValueControl()
    {
        DateFormat = ""; // Definition.Settings.Get("Format", "o");
        return new DateOnlyPicker { ToolTip = Definition.Description };
    }

    public override void SetValue(string value)
    {
        var parsed = false;
        var result = new DateOnly();

        if (!string.IsNullOrWhiteSpace(value))
        {
            const DateTimeStyles dateTimeStyles = DateTimeStyles.AssumeLocal;
            parsed = string.IsNullOrWhiteSpace(DateFormat)
                ? DateOnly.TryParse(value, FormatProvider, dateTimeStyles, out result)
                : DateOnly.TryParseExact(value, DateFormat, FormatProvider, dateTimeStyles, out result);
        }

        if (parsed)
            Value = new DateOnly(result.Year, result.Month, result.Day);
        else
            Value = null;
    }

    public override string GetValue()
    {
        if (!Value.HasValue) return string.Empty;

        var dateOnly = Value.Value;
        if (string.IsNullOrWhiteSpace(DateFormat))
            return dateOnly.ToString(FormatProvider);
        return dateOnly.ToString(DateFormat, FormatProvider);
    }
}