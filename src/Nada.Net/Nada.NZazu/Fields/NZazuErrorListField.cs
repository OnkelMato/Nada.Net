using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Nada.NZazu.Contracts;
using Nada.NZazu.Contracts.Checks;
using Nada.NZazu.Fields.Controls;

namespace Nada.NZazu.Fields;

internal class NZazuErrorListField : NZazuField
{
    private readonly INZazuView _view;

    public NZazuErrorListField(FieldDefinition definition, Func<Type, object> serviceLocatorFunc)
        : base(definition, serviceLocatorFunc)
    {
        IsEditable = false;
        _view = serviceLocatorFunc(typeof(INZazuWpfView)) as INZazuWpfView;
    }

    public override DependencyProperty ContentProperty => null;

    public override void SetValue(string value)
    {
    }

    public override string GetValue()
    {
        return null;
    }

    public override ValueCheckResult Validate()
    {
        return ValueCheckResult.Success;
    }

    protected override Control CreateLabelControl()
    {
        var btn = new Button { Content = Definition.Prompt };
        btn.Click += (sender, e) => { _view.Validate(); };
        return btn;
    }

    protected override Control CreateValueControl()
    {
        return new ErrorPanel { Errors = Enumerable.Empty<string>() };
    }
}