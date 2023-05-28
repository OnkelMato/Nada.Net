using System.Collections;
using System.Globalization;
using FluentAssertions;
using Nada.Replacer;
using Nada.Replacer.Handlers;
using NUnit.Framework;

namespace Nada.Tests.Replacer.Handlers;

public class DateTimeTests
{
    private static IEnumerable GetTestCaseDataFor_Replace_With_HumanReadable()
    {
        yield return new TestCaseData(DateTime.Now).SetName("Replace_With_HumanReadable (now)");
        yield return new TestCaseData(DateTime.Now.AddSeconds(35)).SetName("Replace_With_HumanReadable (now + 35s)");
        yield return new TestCaseData(DateTime.Now.AddMinutes(35)).SetName("Replace_With_HumanReadable (now + 35min)");
        yield return new TestCaseData(DateTime.Now.AddHours(5)).SetName("Replace_With_HumanReadable (now + 5h)");
        yield return new TestCaseData(DateTime.Now.AddDays(5)).SetName("Replace_With_HumanReadable (now + 5days)");
        yield return new TestCaseData(DateTime.Now.AddMonths(5)).SetName("Replace_With_HumanReadable (now + 5months)");
        yield return new TestCaseData(DateTime.Now.AddYears(5)).SetName("Replace_With_HumanReadable (now + 5years)");
        yield return new TestCaseData(DateTime.Now.AddSeconds(-35)).SetName("Replace_With_HumanReadable (now - 35s)");
        yield return new TestCaseData(DateTime.Now.AddMinutes(-35)).SetName("Replace_With_HumanReadable (now - 35min)");
        yield return new TestCaseData(DateTime.Now.AddHours(-5)).SetName("Replace_With_HumanReadable (now - 5h)");
        yield return new TestCaseData(DateTime.Now.AddDays(-5)).SetName("Replace_With_HumanReadable (now - 5days)");
        yield return new TestCaseData(DateTime.Now.AddMonths(-5)).SetName("Replace_With_HumanReadable (now - 5months)");
        yield return new TestCaseData(DateTime.Now.AddYears(-5)).SetName("Replace_With_HumanReadable (now - 5years)");
    }


    [Test]
    public void Replace_Without_Format_Specified()
    {
        var loginDate = DateTime.Now;
        var dict = new Dictionary<string, string>
        {
            { "loginDate", loginDate.ToString("o") }
        };

        // Sample template
        var template = "Last login at {dateTime|loginDate}";

        var ci = new CultureInfo("de");
        var t = new PropertyParser(new[] { new DateTimeTokenTypeHandler(ci) });

        var outcome = t.Parse(template, dict);

        outcome.Should().Be($"Last login at {loginDate.ToString(ci)}");
    }

    [Test]
    public void Replace_With_DateOnly_Format_Specified()
    {
        var loginDate = DateTime.Now;
        var dict = new Dictionary<string, string>
        {
            { "loginDate", loginDate.ToString("o") }
        };

        // Sample template
        var template = "Last login at {dateTime|loginDate|d}";

        var ci = new CultureInfo("de");
        var t = new PropertyParser(new[] { new DateTimeTokenTypeHandler(ci) });

        var outcome = t.Parse(template, dict);

        outcome.Should().Be($"Last login at {loginDate.ToString("d", ci)}");
    }

    [Test]
    public void Replace_With_TimeOnly_Format_Specified()
    {
        var loginDate = DateTime.Now;
        var dict = new Dictionary<string, string>
        {
            { "loginDate", loginDate.ToString("o") }
        };

        // Sample template
        var template = "Last login at {dateTime|loginDate|T}";

        var ci = new CultureInfo("de");
        var t = new PropertyParser(new[] { new DateTimeTokenTypeHandler(ci) });

        var outcome = t.Parse(template, dict);

        outcome.Should().Be($"Last login at {loginDate.ToString("T", ci)}");
    }

    [Test]
    public void Replace_With_TimeOnly2_Format_Specified()
    {
        var loginDate = DateTime.Now;
        var dict = new Dictionary<string, string>
        {
            { "loginDate", loginDate.ToString("o") }
        };

        // Sample template
        var template = "Last login at {dateTime|loginDate|t}";

        var ci = new CultureInfo("de");
        var t = new PropertyParser(new[] { new DateTimeTokenTypeHandler(ci) });

        var outcome = t.Parse(template, dict);

        outcome.Should().Be($"Last login at {loginDate.ToString("t", ci)}");
    }

    [Test]
    public void Replace_With_Custom_Format_Specified()
    {
        var loginDate = DateTime.Now;
        var dict = new Dictionary<string, string>
        {
            { "loginDate", loginDate.ToString("o") }
        };

        // Sample template
        var template = "Last login at {dateTime|loginDate|yyyy-MM-ddThh:mm:ss}";

        var ci = new CultureInfo("de");
        var t = new PropertyParser(new[] { new DateTimeTokenTypeHandler(ci) });

        var outcome = t.Parse(template, dict);

        outcome.Should().Be($"Last login at {loginDate.ToString("yyyy-MM-ddThh:mm:ss", ci)}");
    }

    [Test]
    public void Replace_When_Missing_Value()
    {
        var dict = new Dictionary<string, string>();

        // Sample template
        var template = "Last login at {datetime|loginDate}";

        var ci = new CultureInfo("de");
        var t = new PropertyParser(new[] { new DateTimeTokenTypeHandler(ci) });

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
        var template = "Last login at {datetime|loginDate}";

        var ci = new CultureInfo("de");
        var t = new PropertyParser(new[] { new DateTimeTokenTypeHandler(ci) });

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
        var template = "Last login at {datetime|loginDate}";

        var ci = new CultureInfo("de");
        var t = new PropertyParser(new[] { new DateTimeTokenTypeHandler(ci) });

        var outcome = t.Parse(template, dict);

        outcome.Should().Be(template);
    }


    [Test]
    public void Replace_UTC_Date_Time()
    {
        var loginDate = DateTime.UtcNow;
        var dict = new Dictionary<string, string>
        {
            { "loginDate", loginDate.ToString("o") }
        };

        // Sample template
        var template = "Last login at {dateTime|loginDate}";

        var ci = new CultureInfo("de");
        var t = new PropertyParser(new[] { new DateTimeTokenTypeHandler(ci) });

        var outcome = t.Parse(template, dict);

        outcome.Should().Be($"Last login at {loginDate.ToLocalTime().ToString(ci)}");
    }
}