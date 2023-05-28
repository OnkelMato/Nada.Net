using FluentAssertions;
using Nada.Core.Extensions;
using Nada.Core.Replacer;
using Nada.Core.Replacer.Handlers;
using NUnit.Framework;

namespace Nada.Core.Tests.Replacer;

public class PropertyParserTests
{
    // Humanizer allows to truncate in different way that's why we have to truncate in tests on one place
    private static string Trim(string? input, int length)
    {
        return input.Truncate(length);
    }

    [Test]
    public void Pass_Null_Template()
    {
        var t = new PropertyParser(null);

        var outcome = t.Parse(null!, new Dictionary<string, string>
        {
            { "title", "Abc" }
        });

        outcome.Should().BeNull();
    }

    [Test]
    public void Pass_Null_Replacement_Dictionary()
    {
        var t = new PropertyParser(null);

        var outcome = t.Parse("XXX", null);

        outcome.Should().Be("XXX");
    }

    [Test]
    public void Pass_Null_Template_And_Null_Replacement_Dictionary()
    {
        var t = new PropertyParser(null);

        var outcome = t.Parse(null!, null);

        outcome.Should().BeNull();
    }

    [Test]
    public void No_replacement()
    {
        var dict = new Dictionary<string, string>
        {
            { "title", "Abc" }
        };

        // Sample template
        var template = "The task XYZ has been completed";

        var t = new PropertyParser(null);

        var outcome = t.Parse(template, dict);

        outcome.Should().Be("The task XYZ has been completed");
    }

    [Test]
    public void Empty_Dictionary()
    {
        var dict = new Dictionary<string, string>();

        // Sample template
        var template = "The task {string|test} {string|test2} has been completed";

        var t = new PropertyParser(null);

        var outcome = t.Parse(template, dict);

        outcome.Should().Be("The task {string|test} {string|test2} has been completed");
    }

    [Test]
    public void Missing_Tokens()
    {
        var dict = new Dictionary<string, string>
        {
            { "key", "value" }
        };

        // Sample template
        var template = "The task {string|test} {string|test2} has been completed";

        var t = new PropertyParser(new[] { new StringTokenTypeHandler() });

        var outcome = t.Parse(template, dict);

        outcome.Should().Be("The task {string|test} {string|test2} has been completed");
    }


    [Test]
    public void Replace_Token_Inside()
    {
        var dict = new Dictionary<string, string>
        {
            { "test", "green" }
        };
        var t = new PropertyParser(new[] { new StringTokenTypeHandler() });

        var outcome = t.Parse("red {test {string|test}} blue", dict);

        outcome.Should().Be("red {test green} blue");
    }

    [Test]
    public void Replace_Token_Inside2()
    {
        var dict = new Dictionary<string, string>
        {
            { "test", "green" }
        };
        var t = new PropertyParser(new[] { new StringTokenTypeHandler() });

        var outcome = t.Parse("red {test {string|test|3}} blue", dict);

        outcome.Should().Be($"red {{test {Trim("green", 3)}}} blue");
    }

    [Test]
    public void Missing_Right_Token_Closing()
    {
        var dict = new Dictionary<string, string>
        {
            { "test", "green" }
        };
        var t = new PropertyParser(new[] { new StringTokenTypeHandler() });

        var outcome = t.Parse("red {test {string|test} blue", dict);

        outcome.Should().Be("red {test green blue");
    }

    [Test]
    public void Missing_Left_Token_Closing()
    {
        var dict = new Dictionary<string, string>
        {
            { "test", "green" }
        };
        var t = new PropertyParser(new[] { new StringTokenTypeHandler() });

        var outcome = t.Parse("red test {string|test} blue}", dict);

        outcome.Should().Be("red test green blue}");
    }

    [Test]
    public void Replace_Token_With_Trim__Space_Before_Format_Specifier()
    {
        var dict = new Dictionary<string, string>
        {
            { "test", "green" }
        };
        var t = new PropertyParser(new[] { new StringTokenTypeHandler() });

        var outcome = t.Parse("red {string|test |3} blue", dict);

        outcome.Should().Be($"red {Trim("green", 3)} blue");
    }

    [Test]
    public void Replace_Token_With_Trim__Space_After_Format_Specifier()
    {
        var dict = new Dictionary<string, string>
        {
            { "test", "green" }
        };
        var t = new PropertyParser(new[] { new StringTokenTypeHandler() });

        var outcome = t.Parse("red {string|test| 3} blue", dict);

        outcome.Should().Be($"red {Trim("green", 3)} blue");
    }

    [Test]
    public void Replace_Token_With_Trim__Space_Before_And_After_Format_Specifier()
    {
        var dict = new Dictionary<string, string>
        {
            { "test", "green" }
        };
        var t = new PropertyParser(new[] { new StringTokenTypeHandler() });

        var outcome = t.Parse("red {string|test | 3} blue", dict);

        outcome.Should().Be($"red {Trim("green", 3)} blue");
    }

    [Test]
    public void Replace_Token_With_Trim__Multiple_Spaces_Before_Format_Specifier()
    {
        var dict = new Dictionary<string, string>
        {
            { "test", "green" }
        };
        var t = new PropertyParser(new[] { new StringTokenTypeHandler() });

        var outcome = t.Parse("red {string|  test   |3} blue", dict);

        outcome.Should().Be($"red {Trim("green", 3)} blue");
    }

    [Test]
    public void Replace_Token_With_Trim__Multiple_Spaces_After_Format_Specifier()
    {
        var dict = new Dictionary<string, string>
        {
            { "test", "green" }
        };
        var t = new PropertyParser(new[] { new StringTokenTypeHandler() });

        var outcome = t.Parse("red {string|test|   3 } blue", dict);

        outcome.Should().Be($"red {Trim("green", 3)} blue");
    }

    [Test]
    public void Replace_Token_With_Trim__Multiple_Spaces_Before_And_After_Format_Specifier()
    {
        var dict = new Dictionary<string, string>
        {
            { "test", "green" }
        };
        var t = new PropertyParser(new[] { new StringTokenTypeHandler() });

        var outcome = t.Parse("red { string   |  test    |   3  } blue", dict);

        outcome.Should().Be($"red {Trim("green", 3)} blue");
    }

    [Test]
    public void Replace_Token__Space_After_Token()
    {
        var dict = new Dictionary<string, string>
        {
            { "test", "green" }
        };
        var t = new PropertyParser(new[] { new StringTokenTypeHandler() });

        var outcome = t.Parse("red {string|test } blue", dict);

        outcome.Should().Be("red green blue");
    }

    [Test]
    public void Replace_Token__Space_Before_Token()
    {
        var dict = new Dictionary<string, string>
        {
            { "test", "green" }
        };
        var t = new PropertyParser(new[] { new StringTokenTypeHandler() });

        var outcome = t.Parse("red {string| test} blue", dict);

        outcome.Should().Be("red green blue");
    }

    [Test]
    public void Replace_Token__Space_Before_And_After_Token()
    {
        var dict = new Dictionary<string, string>
        {
            { "test", "green" }
        };
        var t = new PropertyParser(new[] { new StringTokenTypeHandler() });

        var outcome = t.Parse("red {string| test } blue", dict);

        outcome.Should().Be("red green blue");
    }

    [Test]
    public void Replace_Token__Multiple_Spaces_Before_Token()
    {
        var dict = new Dictionary<string, string>
        {
            { "test", "green" }
        };
        var t = new PropertyParser(new[] { new StringTokenTypeHandler() });

        var outcome = t.Parse("red {string    |  test} blue", dict);

        outcome.Should().Be("red green blue");
    }

    [Test]
    public void Replace_Token__Multiple_Spaces_After_Token()
    {
        var dict = new Dictionary<string, string>
        {
            { "test", "green" }
        };
        var t = new PropertyParser(new[] { new StringTokenTypeHandler() });

        var outcome = t.Parse("red {string    |    test   } blue", dict);

        outcome.Should().Be("red green blue");
    }

    [Test]
    public void Replace_Token__Multiple_Spaces_Before_And_After_Token()
    {
        var dict = new Dictionary<string, string>
        {
            { "test", "green" }
        };
        var t = new PropertyParser(new[] { new StringTokenTypeHandler() });

        var outcome = t.Parse("red {string     |      test       } blue", dict);

        outcome.Should().Be("red green blue");
    }

    [Test]
    public void Replace_Unknown_Token_Type()
    {
        var dict = new Dictionary<string, string>
        {
            { "key", "value" }
        };
        var t = new PropertyParser(null);

        var template = "{spaceDataTypeRunning|tokenName}";
        var outcome = t.Parse(template, dict);

        outcome.Should().Be(template);
    }

    [Test]
    public void Invalid_Token_Because_Of_Too_Many_Parts()
    {
        var dict = new Dictionary<string, string>();
        var t = new PropertyParser(null);

        var template = "{spaceDataTypeRunning|tokenName|fsd|fds|fsd}";
        var outcome = t.Parse(template, dict);

        outcome.Should().Be(template);
    }

    [Test]
    public void Replace_Token_With_Invalid_Token_Name()
    {
        var dict = new Dictionary<string, string>();
        var t = new PropertyParser(new[] { new StringTokenTypeHandler() });

        var template = "{string|!@#$%^&*()}";
        var outcome = t.Parse(template, dict);

        outcome.Should().Be(template);
    }

    [Test]
    public void Replace_Token_With_Invalid_Token_Name2()
    {
        var dict = new Dictionary<string, string>();
        var t = new PropertyParser(new[] { new StringTokenTypeHandler() });

        var template = "{string|this#is#invalid}";
        var outcome = t.Parse(template, dict);

        outcome.Should().Be(template);
    }

    [Test]
    public void Replace_Token_With_Hyphen_And_Underscore()
    {
        var dict = new Dictionary<string, string>
        {
            { "this_is-invalid", "green" }
        };
        var t = new PropertyParser(new[] { new StringTokenTypeHandler() });

        var template = "{string|this_is-invalid}";
        var outcome = t.Parse(template, dict);

        outcome.Should().Be("green");
    }
}