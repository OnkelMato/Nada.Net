using FluentAssertions;
using MagicBox.Extensions;
using NUnit.Framework;

namespace Nada.Tests.Extensions;

[TestFixture]
internal class DictionaryExtensionsTests
{
    [Test]
    public void AddOrUpdate_On_Existing()
    {
        var dct = new Dictionary<string, object> { { "Hulk", "Bruce" }, { "Iron Man", "Tony Stark" } };

        dct.AddOrUpdate("Hulk", "Bruce Banner");

        dct.Count.Should().Be(2);
        dct["Hulk"].Should().Be("Bruce Banner");
    }

    [Test]
    public void AddOrUpdate_On_new()
    {
        var dct = new Dictionary<string, object> { { "Hulk", "Bruce Banner" }, { "Iron Man", "Tony Stark" } };

        dct.AddOrUpdate("Spiderman", "Peter Parker");

        dct.Count.Should().Be(3);
        dct["Spiderman"].Should().Be("Peter Parker");
    }
}