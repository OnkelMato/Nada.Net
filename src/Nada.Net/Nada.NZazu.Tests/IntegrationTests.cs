using System.Windows;
using Nada.NZazu.Contracts;
using NUnit.Framework;

namespace Nada.NZazu.Tests;

[TestFixture]
public class IntegrationTests
{
    [Test]
    [Explicit]
    [Apartment(ApartmentState.STA)]
    public void Be_Creatable()
    {
        var def = new[]
        {
            new FieldDefinition { Key = "k1", Type = "dateonly", Prompt = "Date only" },
            new FieldDefinition { Key = "k2", Type = "string", Prompt = "Some Text" }
        };

        var view = new NZazuView();
        view.FormDefinition = new FormDefinition { Fields = def };

        var window = new Window();
        window.Content = view;

        window.ShowDialog();

        var res = view.FormData.Values;
    }
}