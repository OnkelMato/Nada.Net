using FluentAssertions;
using Nada.Extensions;
using Nada.Replacer;
using Nada.Replacer.Handlers;
using NUnit.Framework;

namespace Nada.Tests.Replacer.Handlers;

public class StringTests
{
    // Humanizer allows to truncate in different way that's why we have to truncate in tests on one place
    private static string Trim(string? input, int length)
    {
        return input.Truncate(length);
    }

    [Test]
    public void Replace_One_Token()
    {
        var dict = new Dictionary<string, string>
        {
            { "title", "My Task #1" }
        };

        // Sample template
        var template = "The task {string|title} has been completed";

        var t = new PropertyParser(new[] { new StringTokenTypeHandler() });

        var outcome = t.Parse(template, dict);

        outcome.Should().Be("The task My Task #1 has been completed");
    }

    [Test]
    public void Replace_One_Token_With_Token_Characters()
    {
        var dict = new Dictionary<string, string>
        {
            { "title", "My Task #1" }
        };

        // Sample template
        var template = "The task {{string|title}} has been completed";

        var t = new PropertyParser(new[] { new StringTokenTypeHandler() });

        var outcome = t.Parse(template, dict);

        outcome.Should().Be("The task {My Task #1} has been completed");
    }

    [Test]
    public void Replace_Multiple_Tokens()
    {
        var dict = new Dictionary<string, string>
        {
            { "title", "My Task #1" },
            { "title2", "My Task #2" },
            { "ti", "My Task #3" }
        };

        // Sample template
        var template = "The task {string|title}, {string|title2}, {string|ti} have been completed";

        var t = new PropertyParser(new[] { new StringTokenTypeHandler() });

        var outcome = t.Parse(template, dict);

        outcome.Should().Be("The task My Task #1, My Task #2, My Task #3 have been completed");
    }

    [Test]
    public void Replace_Multiple_Tokens_Without_Spaces_In_The_Text()
    {
        var dict = new Dictionary<string, string>
        {
            { "title", "My Task #1" },
            { "title2", "My Task #2" },
            { "ti", "My Task #3" }
        };

        // Sample template
        var template = "The task {string|title}{string|title2}{string|ti} have been completed";

        var t = new PropertyParser(new[] { new StringTokenTypeHandler() });

        var outcome = t.Parse(template, dict);

        outcome.Should().Be("The task My Task #1My Task #2My Task #3 have been completed");
    }

    [Test]
    public void Replace_One_Token_With_Trim_When_Trim_Actually_Not_Needed()
    {
        var dict = new Dictionary<string, string>
        {
            { "title", "My Task #1" }
        };

        // Sample template
        var template = "The task {string|title|10} has been completed";

        var t = new PropertyParser(new[] { new StringTokenTypeHandler() });

        var outcome = t.Parse(template, dict);

        outcome.Should().Be($"The task {Trim(dict["title"].ToString(), 10)} has been completed");
    }

    [Test]
    public void Replace_Multiple_Tokens_With_Trim_When_Trim_Actually_Not_Needed()
    {
        var dict = new Dictionary<string, string>
        {
            { "title", "My Task #1" },
            { "title2", "My Task #2" },
            { "ti", "My Task #3" }
        };

        // Sample template
        var template = "The task {string|title|10}, {string|title2|10}, {string|ti|10} have been completed";

        var t = new PropertyParser(new[] { new StringTokenTypeHandler() });

        var outcome = t.Parse(template, dict);

        outcome.Should().Be($"The task {Trim(dict["title"].ToString(), 10)}, {Trim(dict["title2"].ToString(), 10)}, {Trim(dict["ti"].ToString(), 10)} have been completed");
    }

    [Test]
    public void Replace_Multiple_Tokens_Without_Spaces_In_The_Text_With_Trim_When_Trim_Actually_Not_Needed()
    {
        var dict = new Dictionary<string, string>
        {
            { "title", "My Task #1" },
            { "title2", "My Task #2" },
            { "ti", "My Task #3" }
        };

        // Sample template
        const string template = "The task {string|title|10}{string|title2|10}{string|ti|10} have been completed";

        var t = new PropertyParser(new[] { new StringTokenTypeHandler() });

        var outcome = t.Parse(template, dict);

        outcome.Should().Be($"The task {Trim(dict["title"].ToString(), 10)}{Trim(dict["title2"].ToString(), 10)}{Trim(dict["ti"].ToString(), 10)} have been completed");
    }

    [Test]
    public void Replace_One_Token_With_Trim_When_Trim_Needed()
    {
        var dict = new Dictionary<string, string>
        {
            { "title", "My Task #35" }
        };

        // Sample template
        var template = "The task {string|title|5} has been completed";

        var t = new PropertyParser(new[] { new StringTokenTypeHandler() });

        var outcome = t.Parse(template, dict);

        outcome.Should().Be($"The task {Trim(dict["title"].ToString(), 5)} has been completed");
    }

    [Test]
    public void Replace_Multiple_Tokens_With_Trim_When_Trim_Needed()
    {
        var dict = new Dictionary<string, string>
        {
            { "title", "My Task #1" },
            { "title2", "My Task #2" },
            { "ti", "My Task #3" }
        };

        // Sample template
        var template = "The task {string|title|6} and {string|title|6} and {string|title|6} have been completed";

        var t = new PropertyParser(new[] { new StringTokenTypeHandler() });

        var outcome = t.Parse(template, dict);

        outcome.Should().Be($"The task {Trim(dict["title"].ToString(), 6)} and {Trim(dict["title2"].ToString(), 6)} and {Trim(dict["ti"].ToString(), 6)} have been completed");
    }

    [Test]
    public void Replace_Multiple_Tokens_Without_Spaces_In_The_Text_With_Trim_Needed()
    {
        var dict = new Dictionary<string, string>
        {
            { "title", "My Task #1" },
            { "title2", "My Task #2" },
            { "ti", "My Task #3" }
        };

        // Sample template
        var template = "The task {string|title|6}{string|title|6}{string|title|6} have been completed";

        var t = new PropertyParser(new[] { new StringTokenTypeHandler() });

        var outcome = t.Parse(template, dict);

        outcome.Should().Be($"The task {Trim(dict["title"].ToString(), 6)}{Trim(dict["title2"].ToString(), 6)}{Trim(dict["ti"].ToString(), 6)} have been completed");
    }

    [Test]
    public void Replace_One_Tokens_With_Trim_Needed()
    {
        var dict = new Dictionary<string, string>
        {
            { "title", "My Task #1" }
        };

        // Sample template
        var template = "The task {string|title|6} has been completed";

        var t = new PropertyParser(new[] { new StringTokenTypeHandler() });

        var outcome = t.Parse(template, dict);

        outcome.Should().Be($"The task {Trim(dict["title"].ToString(), 6)} has been completed");
    }

    [Test]
    public void Replace_One_Tokens_With_Trim_Needed_When_Trim_Not_Needed()
    {
        var dict = new Dictionary<string, string>
        {
            { "title", "My Task #1" }
        };

        // Sample template
        var template = "The task {string|title|15} has been completed";

        var t = new PropertyParser(new[] { new StringTokenTypeHandler() });

        var outcome = t.Parse(template, dict);

        outcome.Should().Be($"The task {Trim(dict["title"].ToString(), 15)} has been completed");
    }

    [Test]
    public void Replace_One_Tokens_With_Trim_Set_To_1()
    {
        var dict = new Dictionary<string, string>
        {
            { "title", "My Task #1" }
        };

        // Sample template
        var template = "The task {string|title|1} has been completed";

        var t = new PropertyParser(new[] { new StringTokenTypeHandler() });

        var outcome = t.Parse(template, dict);

        outcome.Should().Be($"The task {Trim(dict["title"].ToString(), 1)} has been completed");
    }

    [Test]
    public void Replace_One_Tokens_With_Trim_Set_To_0()
    {
        var dict = new Dictionary<string, string>
        {
            { "title", "My Task #1" }
        };

        // Sample template
        var template = "The task {string|title|0} has been completed";

        var t = new PropertyParser(new[] { new StringTokenTypeHandler() });

        var outcome = t.Parse(template, dict);

        outcome.Should().Be($"The task {Trim(dict["title"].ToString(), 0)} has been completed");
    }

    [Test]
    public void Replace_When_Missing_Value()
    {
        var dict = new Dictionary<string, string>();

        // Sample template
        var template = "Last login at {string|test}";

        var t = new PropertyParser(new[] { new StringTokenTypeHandler() });

        var outcome = t.Parse(template, dict);

        outcome.Should().Be(template);
    }

    [Test]
    public void Replace_When_Null_Value()
    {
        var dict = new Dictionary<string, string>
        {
            { "test", null! }
        };

        // Sample template
        var template = "Last login at {string|test}";

        var t = new PropertyParser(new[] { new StringTokenTypeHandler() });

        var outcome = t.Parse(template, dict);

        outcome.Should().Be(template);
    }


    [Test]
    public void Negative_Trim()
    {
        var dict = new Dictionary<string, string>
        {
            { "test", "green" }
        };
        var t = new PropertyParser(new[] { new StringTokenTypeHandler() });

        var outcome = t.Parse("red test {string|test|-5}", dict);

        outcome.Should().Be("red test {string|test|-5}");
    }

    [Test]
    public void Truncate_Html_Not_Needed()
    {
        var dict = new Dictionary<string, string>
        {
            { "test", "<p>This is a <strong>long</strong> string that needs to be truncated.</p>" }
        };

        var t = new PropertyParser(new[] { new StringTokenTypeHandler() });

        var outcome = t.Parse("{string|test|49|html}", dict);

        outcome.Should().Be("<p>This is a <strong>long</strong> string that needs to be truncated.</p>");
    }

    [Test]
    [TestCase(10, "<p>This is a <strong>long</strong> string that needs to be truncated.</p>", "<p>This is a…</p>")]
    [TestCase(14, "<p>This is a <strong>long</strong> string that needs to be truncated.</p>", "<p>This is a <strong>lon…</strong></p>")]
    [TestCase(17, "This is a test <a href='xxx'>This is a link</a>. And here we continue", "This is a test <a href='xxx'>T…</a>")]
    public void Truncate_Html(int length, string value, string result)
    {
        var dict = new Dictionary<string, string>
        {
            { "test", value }
        };

        var t = new PropertyParser(new[] { new StringTokenTypeHandler() });

        var outcome = t.Parse($"{{string|test|{length}|html}}", dict);

        outcome.Should().Be(result);
    }
}