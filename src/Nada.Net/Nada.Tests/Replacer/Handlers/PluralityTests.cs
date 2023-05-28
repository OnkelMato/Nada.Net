using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Nada.Core.Replacer;
using Nada.Core.Replacer.Handlers;
using NUnit.Framework;

namespace Nada.Core.Tests.Replacer.Handlers;

public class PluralityTests
{
    [ExcludeFromCodeCoverage]
    private static IEnumerable<dynamic> GetNumber1()
    {
        return new ValueType[]
        {
            (sbyte)1,
            (byte)1,
            (short)1,
            (ushort)1,
            1,
            (uint)1,
            (long)1,
            (ulong)1
        };
    }

    [ExcludeFromCodeCoverage]
    private static IEnumerable<dynamic> GetNumber5()
    {
        return new ValueType[]
        {
            (sbyte)5,
            (byte)5,
            (short)5,
            (ushort)5,
            5,
            (uint)5,
            (long)5,
            (ulong)5
        };
    }

    [ExcludeFromCodeCoverage]
    private static IEnumerable<dynamic> GetFloatingNumber1()
    {
        return new ValueType[]
        {
            (float)1,
            (double)1,
            (decimal)1
        };
    }

    [ExcludeFromCodeCoverage]
    private static IEnumerable<dynamic> GetFloatingNumber10000001()
    {
        return new ValueType[]
        {
            (float)1.0000001,
            1.0000001,
            (decimal)1.0000001
        };
    }

    [ExcludeFromCodeCoverage]
    private static IEnumerable<dynamic> GetFloatingNumber5()
    {
        return new ValueType[]
        {
            (float)5.5,
            5.5,
            (decimal)5.5
        };
    }

    [Test]
    [TestCaseSource(nameof(GetNumber1))]
    [TestCaseSource(nameof(GetFloatingNumber1))]
    public void As_Singular(dynamic numberValue)
    {
        var dict = new Dictionary<string, string>
        {
            { "tct", numberValue.ToString() }
        };

        // Sample template
        var template =
            "{plurality|tct|A|@tct} new {plurality|tct|task|tasks} {plurality|tct|has|have} been assigned to you";

        var t = new PropertyParser(new[] { new PluralityTokenTypeHandler() });

        var outcome = t.Parse(template, dict);

        outcome.Should().Be("A new task has been assigned to you");
    }

    [Test]
    [TestCaseSource(nameof(GetNumber5))]
    [TestCaseSource(nameof(GetFloatingNumber5))]
    [TestCaseSource(nameof(GetFloatingNumber10000001))]
    public void As_Plural(dynamic numberValue)
    {
        var dict = new Dictionary<string, string>
        {
            { "tct", numberValue.ToString() }
        };

        // Sample template
        var template =
            "{plurality|tct|A|@tct} new {plurality|tct|task|tasks} {plurality|tct|has|have} been assigned to you";

        var t = new PropertyParser(new[] { new PluralityTokenTypeHandler() });

        var outcome = t.Parse(template, dict);

        outcome.Should().Be($"{numberValue} new tasks have been assigned to you");
    }

    [Test]
    public void Replace_When_Missing_Value()
    {
        var dict = new Dictionary<string, string>
        {
            { "key", "value" }
        };

        // Sample template
        var template =
            "{plurality|tct|A|@tct} new {plurality|tct|task|tasks} {plurality|tct|has|have} been assigned to you";

        var t = new PropertyParser(new[] { new PluralityTokenTypeHandler() });

        var outcome = t.Parse(template, dict);

        outcome.Should().Be(template);
    }

    [Test]
    public void Replace_When_Missing_Value_For_Reference_Singular()
    {
        var dict = new Dictionary<string, string>
        {
            { "key", "value" },
            { "tct", "1" }
        };

        // Sample template
        var template = "{plurality|tct|@Task|XXX}";

        var t = new PropertyParser(new[] { new PluralityTokenTypeHandler() });

        var outcome = t.Parse(template, dict);

        outcome.Should().Be("@Task");
    }

    [Test]
    public void Replace_When_Missing_Value_For_Reference_Plural()
    {
        var dict = new Dictionary<string, string>
        {
            { "key", "value" },
            { "tct", "2" }
        };

        // Sample template
        var template = "{plurality|tct|XXX|@Task}";

        var t = new PropertyParser(new[] { new PluralityTokenTypeHandler() });

        var outcome = t.Parse(template, dict);

        outcome.Should().Be("@Task");
    }

    [Test]
    public void Replace_With_Null_Value()
    {
        var dict = new Dictionary<string, string>
        {
            { "tct", null! }
        };

        // Sample template
        var template =
            "{plurality|tct|A|@tct} new {plurality|tct|task|tasks} {plurality|tct|has|have} been assigned to you";

        var t = new PropertyParser(new[] { new PluralityTokenTypeHandler() });

        var outcome = t.Parse(template, dict);

        outcome.Should().Be(template);
    }

    [Test]
    public void Replace_With_Invalid_Value()
    {
        var dict = new Dictionary<string, string>
        {
            { "tct", "zzz" }
        };

        // Sample template
        var template =
            "{plurality|tct|A|@tct} new {plurality|tct|task|tasks} {plurality|tct|has|have} been assigned to you";

        var t = new PropertyParser(new[] { new PluralityTokenTypeHandler() });

        var outcome = t.Parse(template, dict);

        outcome.Should().Be(template);
    }
}