using FluentAssertions;
using Nada.Core.Replacer;
using Nada.Core.Replacer.Handlers;
using NUnit.Framework;

namespace Nada.Core.Tests.Replacer.Handlers;

public class GuidTests
{
    [Test]
    public void Replace_With_Format_Specified()
    {
        var guid = Guid.NewGuid();
        var dict = new Dictionary<string, string>
        {
            { "token", guid.ToString() }
        };

        // Sample template
        var template = "Task id: {guid|token|P}";

        var t = new PropertyParser(new[] { new GuidTokenTypeHandler() });

        var outcome = t.Parse(template, dict);

        outcome.Should().Be($"Task id: {guid:P}");
    }

    [Test]
    public void Replace_Without_Format_Specified()
    {
        var guid = Guid.NewGuid();
        var dict = new Dictionary<string, string>
        {
            { "token", guid.ToString() }
        };

        // Sample template
        var template = "Task id: {guid|token}";

        var t = new PropertyParser(new[] { new GuidTokenTypeHandler() });

        var outcome = t.Parse(template, dict);

        outcome.Should().Be($"Task id: {guid}");
    }

    [Test]
    public void Replace_When_Missing_Value()
    {
        var dict = new Dictionary<string, string>();

        // Sample template
        var template = "Task id: {guid|token}";

        var t = new PropertyParser(new[] { new GuidTokenTypeHandler() });

        var outcome = t.Parse(template, dict);

        outcome.Should().Be(template);
    }

    [Test]
    public void Replace_With_Null_Value()
    {
        var dict = new Dictionary<string, string>
        {
            { "token", null! }
        };

        // Sample template
        var template = "Task id: {guid|token}";

        var t = new PropertyParser(new[] { new GuidTokenTypeHandler() });

        var outcome = t.Parse(template, dict);

        outcome.Should().Be(template);
    }

    [Test]
    public void Replace_With_Invalid_Value()
    {
        var dict = new Dictionary<string, string>
        {
            { "token", "zzz" }
        };

        // Sample template
        var template = "Task id: {guid|token}";

        var t = new PropertyParser(new[] { new GuidTokenTypeHandler() });

        var outcome = t.Parse(template, dict);

        outcome.Should().Be(template);
    }

    [Test]
    public void Replace_With_Invalid_Format_Specified()
    {
        var guid = Guid.NewGuid();
        var dict = new Dictionary<string, string>
        {
            { "token", guid.ToString() }
        };

        // Sample template
        var template = "Task id: {guid|token|LKL}";

        var t = new PropertyParser(new[] { new GuidTokenTypeHandler() });

        var outcome = t.Parse(template, dict);

        outcome.Should().Be(template);
    }
}