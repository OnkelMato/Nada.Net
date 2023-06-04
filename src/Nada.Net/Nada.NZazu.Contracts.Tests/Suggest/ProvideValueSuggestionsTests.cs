using FluentAssertions;
using Nada.NZazu.Contracts.Suggest;
using NUnit.Framework;

namespace Nada.NZazu.Contracts.Tests.Suggest;

[TestFixture]
// ReSharper disable once InconsistentNaming
public class ProvideValueSuggestionsTests
{
    [Test]
    public void Be_Creatable()
    {
        var sut = new ProvideValueSuggestions();

        sut.Should().NotBeNull();
        sut.Should().BeAssignableTo<IProvideSuggestions>();
    }

    [Test]
    public void Return_Prefixes()
    {
        const string dataConnection = "value://anton|adam|abraham|anna|annika|astrid";
        var prefix = "an";
        var expected = new[] { "anton", "anna", "annika" };
        var sut = new ProvideValueSuggestions();

        var actual = sut.For(prefix, dataConnection);
        actual.Should().BeEquivalentTo(expected);

        prefix = "ann";
        expected = new[] { "anna", "annika" };
        actual = sut.For(prefix, dataConnection);
        actual.Should().BeEquivalentTo(expected);
    }

    [Test]
    public void Return_Empty_If_Not_Match()
    {
        const string dataConnection = "this is a crappy data connection";
        var prefix = "thomas";
        var expected = new string[] { };
        var sut = new ProvideValueSuggestions();

        var actual = sut.For(prefix, dataConnection);
        actual.Should().BeEquivalentTo(expected);
    }
}