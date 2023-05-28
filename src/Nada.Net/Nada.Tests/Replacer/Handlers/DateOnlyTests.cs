using System.Collections;
using System.Globalization;
using FluentAssertions;
using Nada.Replacer;
using Nada.Replacer.Handlers;
using NUnit.Framework;

namespace Nada.Tests.Replacer.Handlers;

public class DateOnlyTests
{
    private static IEnumerable GetTestCaseDataFor_Replace_With_HumanReadable()
    {
        yield return new TestCaseData(DateOnly.FromDateTime(DateTime.Now)).SetName("Replace_With_HumanReadable (now)");
        yield return new TestCaseData(DateOnly.FromDateTime(DateTime.Now.AddDays(5))).SetName(
            "Replace_With_HumanReadable (now + 5days)");
        yield return new TestCaseData(DateOnly.FromDateTime(DateTime.Now.AddMonths(5))).SetName(
            "Replace_With_HumanReadable (now + 5months)");
        yield return new TestCaseData(DateOnly.FromDateTime(DateTime.Now.AddYears(5))).SetName(
            "Replace_With_HumanReadable (now + 5years)");
        yield return new TestCaseData(DateOnly.FromDateTime(DateTime.Now.AddDays(-5))).SetName(
            "Replace_With_HumanReadable (now - 5 days)");
        yield return new TestCaseData(DateOnly.FromDateTime(DateTime.Now.AddMonths(-5))).SetName(
            "Replace_With_HumanReadable (now - 5 months)");
        yield return new TestCaseData(DateOnly.FromDateTime(DateTime.Now.AddYears(-5))).SetName(
            "Replace_With_HumanReadable (now - 5years)");
    }

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