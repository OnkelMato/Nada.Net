using FluentAssertions;
using Nada.Replacer;
using Nada.Replacer.Handlers;
using NUnit.Framework;

namespace Nada.Tests.Replacer.Handlers;

public class LinkTests
{
    [Test]
    public void Replace_When_Title_Is_Not_Specified()
    {
        var dict = new Dictionary<string, string>
        {
            { "taskLink", "https://my-url-that-should-not-exist.not.exist" }
        };

        // Sample template
        var template = "Open {link|taskLink|Task} here";

        var t = new PropertyParser(new[] { new LinkTokenTypeHandler() });

        var outcome = t.Parse(template, dict);

        outcome.Should().Be($"Open <a href='{dict["taskLink"]}'>Task</a> here");
    }

    [Test]
    public void Replace_When_Title_Is_Specified()
    {
        var dict = new Dictionary<string, string>
        {
            { "taskLink", "https://my-url-that-should-not-exist.not.exist" },
            { "Task", "My task #35" }
        };

        // Sample template
        var template = "Open {link|taskLink|@Task} here";

        var t = new PropertyParser(new[] { new LinkTokenTypeHandler() });

        var outcome = t.Parse(template, dict);

        outcome.Should().Be($"Open <a href='{dict["taskLink"]}'>My task #35</a> here");
    }

    [Test]
    public void Replace_When_Title_Is_Specified_But_Not_Provided()
    {
        var dict = new Dictionary<string, string>
        {
            { "taskLink", "https://my-url-that-should-not-exist.not.exist" }
        };

        // Sample template
        var template = "Open {link|taskLink|@Task} here";

        var t = new PropertyParser(new[] { new LinkTokenTypeHandler() });

        var outcome = t.Parse(template, dict);

        outcome.Should().Be(template);
    }

    [Test]
    public void Replace_When_Missing_Link_Value()
    {
        var dict = new Dictionary<string, string>();

        // Sample template
        var template = "Open {link|taskLink|Task} here";

        var t = new PropertyParser(new[] { new LinkTokenTypeHandler() });

        var outcome = t.Parse(template, dict);

        outcome.Should().Be(template);
    }

    [Test]
    public void Replace_When_Missing_Title_Value()
    {
        var dict = new Dictionary<string, string>
        {
            { "taskLink", "https://my-url-that-should-not-exist.not.exist" }
        };

        // Sample template
        var template = "Open {link|taskLink|@Task} here";

        var t = new PropertyParser(new[] { new LinkTokenTypeHandler() });

        var outcome = t.Parse(template, dict);

        outcome.Should().Be(template);
    }

    [Test]
    public void Replace_When_Missing_Format_Specifier()
    {
        var dict = new Dictionary<string, string>
        {
            { "taskLink", "https://my-url-that-should-not-exist.not.exist" }
        };

        // Sample template
        var template = "Open {link|taskLink} here";

        var t = new PropertyParser(new[] { new LinkTokenTypeHandler() });

        var outcome = t.Parse(template, dict);

        outcome.Should().Be(template);
    }
}