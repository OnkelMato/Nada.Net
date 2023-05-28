using System.Collections;
using System.Globalization;
using FluentAssertions;
using Nada.Replacer;
using Nada.Replacer.Handlers;
using NUnit.Framework;

namespace Nada.Tests.Replacer.Handlers;

public class TimeOnlyTests
{
    private static IEnumerable GetTestCaseDataFor_Replace_With_HumanReadable()
    {
        yield return new TestCaseData(TimeOnly.FromDateTime(DateTime.Now)).SetName("Replace_With_HumanReadable (now)");
        yield return new TestCaseData(TimeOnly.FromDateTime(DateTime.Now.AddSeconds(35))).SetName(
            "Replace_With_HumanReadable (now + 35s)");
        yield return new TestCaseData(TimeOnly.FromDateTime(DateTime.Now.AddMinutes(35))).SetName(
            "Replace_With_HumanReadable (now + 35min)");
        yield return new TestCaseData(TimeOnly.FromDateTime(DateTime.Now.AddHours(5))).SetName(
            "Replace_With_HumanReadable (now + 5h)");
        yield return new TestCaseData(TimeOnly.FromDateTime(DateTime.Now.AddSeconds(-35))).SetName(
            "Replace_With_HumanReadable (now -35s)");
        yield return new TestCaseData(TimeOnly.FromDateTime(DateTime.Now.AddMinutes(-35))).SetName(
            "Replace_With_HumanReadable (now - 35min)");
        yield return new TestCaseData(TimeOnly.FromDateTime(DateTime.Now.AddHours(-5))).SetName(
            "Replace_With_HumanReadable (now - 5h)");
    }


    [Test]
    public void Replace_Without_Format_Specified()
    {
        var loginDate = TimeOnly.FromDateTime(DateTime.Now);
        var dict = new Dictionary<string, string>
        {
            { "loginDate", loginDate.ToString("o") }
        };

        // Sample template
        var template = "Last login at {timeOnly|loginDate}";

        var ci = new CultureInfo("de");
        var t = new PropertyParser(new[] { new TimeOnlyTokenTypeHandler(ci) });

        var outcome = t.Parse(template, dict);

        outcome.Should().Be($"Last login at {loginDate.ToString(ci)}");
    }

    [Test]
    public void Replace_With_TimeOnly_Format_Specified()
    {
        var loginDate = TimeOnly.FromDateTime(DateTime.Now);
        var dict = new Dictionary<string, string>
        {
            { "loginDate", loginDate.ToString("o") }
        };

        // Sample template
        var template = "Last login at {timeOnly|loginDate|t}";

        var ci = new CultureInfo("de");
        var t = new PropertyParser(new[] { new TimeOnlyTokenTypeHandler(ci) });

        var outcome = t.Parse(template, dict);

        outcome.Should().Be($"Last login at {loginDate.ToString("t", ci)}");
    }

    [Test]
    public void Replace_With_Custom_Format_Specified()
    {
        var loginDate = TimeOnly.FromDateTime(DateTime.Now);
        var dict = new Dictionary<string, string>
        {
            { "loginDate", loginDate.ToString("o") }
        };

        // Sample template
        var template = "Last login at {timeOnly|loginDate|hh:mm:ss}";

        var ci = new CultureInfo("de");
        var t = new PropertyParser(new[] { new TimeOnlyTokenTypeHandler(ci) });

        var outcome = t.Parse(template, dict);

        outcome.Should().Be($"Last login at {loginDate.ToString("hh:mm:ss", ci)}");
    }

    [Test]
    public void Replace_When_Missing_Value()
    {
        var dict = new Dictionary<string, string>();

        // Sample template
        var template = "Last login at {timeOnly|loginDate}";

        var ci = new CultureInfo("de");
        var t = new PropertyParser(new[] { new TimeOnlyTokenTypeHandler(ci) });

        var outcome = t.Parse(template, dict);

        outcome.Should().Be(template);
    }

    [Test]
    public void Replace_With_Null_Value()
    {
        var dict = new Dictionary<string, string>
        {
            { "loginDate", null! }
        };

        // Sample template
        var template = "Last login at {timeOnly|loginDate}";

        var ci = new CultureInfo("de");
        var t = new PropertyParser(new[] { new TimeOnlyTokenTypeHandler(ci) });

        var outcome = t.Parse(template, dict);

        outcome.Should().Be(template);
    }

    [Test]
    public void Replace_With_Invalid_Value()
    {
        var dict = new Dictionary<string, string>
        {
            { "loginDate", "zzzz" }
        };

        // Sample template
        var template = "Last login at {timeonly|loginDate}";

        var ci = new CultureInfo("de");
        var t = new PropertyParser(new[] { new TimeOnlyTokenTypeHandler(ci) });

        var outcome = t.Parse(template, dict);

        outcome.Should().Be(template);
    }
}