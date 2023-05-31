using System.Globalization;
using FluentAssertions;
using Nada.Core.Replacer;
using Nada.Core.Replacer.Handlers;
using NUnit.Framework;

namespace Nada.Core.Tests.Replacer.Handlers;

public class DateOnlyTests
{
    [Test]
    public void Replace_Without_Format_Specified()
    {
        var loginDate = DateOnly.FromDateTime(DateTime.Now);
        var dict = new Dictionary<string, string>
        {
            { "loginDate", loginDate.ToString("o") }
        };

        // Sample template
        var template = "Last login at {dateOnly|loginDate}";

        var ci = new CultureInfo("de");
        var t = new PropertyParser(new[] { new DateOnlyTokenTypeHandler(ci) });

        var outcome = t.Parse(template, dict);

        outcome.Should().Be($"Last login at {loginDate.ToString(ci)}");
    }

    [Test]
    public void Replace_With_DateOnly_Format_Specified()
    {
        var loginDate = DateOnly.FromDateTime(DateTime.Now);
        var dict = new Dictionary<string, string>
        {
            { "loginDate", loginDate.ToString("o") }
        };

        // Sample template
        var template = "Last login at {dateOnly|loginDate|d}";

        var ci = new CultureInfo("de");
        var t = new PropertyParser(new[] { new DateOnlyTokenTypeHandler(ci) });

        var outcome = t.Parse(template, dict);

        outcome.Should().Be($"Last login at {loginDate.ToString("d", ci)}");
    }

    [Test]
    public void Replace_With_Custom_Format_Specified()
    {
        var loginDate = DateOnly.FromDateTime(DateTime.Now);
        var dict = new Dictionary<string, string>
        {
            { "loginDate", loginDate.ToString("o") }
        };

        // Sample template
        var template = "Last login at {dateOnly|loginDate|yyyy-MM-dd}";

        var ci = new CultureInfo("de");
        var t = new PropertyParser(new[] { new DateOnlyTokenTypeHandler(ci) });

        var outcome = t.Parse(template, dict);

        outcome.Should().Be($"Last login at {loginDate.ToString("yyyy-MM-dd", ci)}");
    }

    [Test]
    public void Replace_When_Missing_Value()
    {
        DateOnly.FromDateTime(DateTime.Now);
        var dict = new Dictionary<string, string>();

        // Sample template
        var template = "Last login at {dateOnly|loginDate}";

        var ci = new CultureInfo("de");
        var t = new PropertyParser(new[] { new DateOnlyTokenTypeHandler(ci) });

        var outcome = t.Parse(template, dict);

        outcome.Should().Be(template);
    }

    [Test]
    public void Replace_With_Null_Value()
    {
        DateOnly.FromDateTime(DateTime.Now);
        var dict = new Dictionary<string, string>
        {
            { "loginDate", null! }
        };

        // Sample template
        var template = "Last login at {dateOnly|loginDate}";

        var ci = new CultureInfo("de");
        var t = new PropertyParser(new[] { new DateOnlyTokenTypeHandler(ci) });

        var outcome = t.Parse(template, dict);

        outcome.Should().Be(template);
    }

    [Test]
    public void Replace_With_Invalid_Value()
    {
        DateOnly.FromDateTime(DateTime.Now);
        var dict = new Dictionary<string, string>
        {
            { "loginDate", "zzzz" }
        };

        // Sample template
        var template = "Last login at {dateOnly|loginDate}";

        var ci = new CultureInfo("de");
        var t = new PropertyParser(new[] { new DateOnlyTokenTypeHandler(ci) });

        var outcome = t.Parse(template, dict);

        outcome.Should().Be(template);
    }
}