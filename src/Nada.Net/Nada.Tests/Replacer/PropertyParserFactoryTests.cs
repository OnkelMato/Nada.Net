using System.Globalization;
using FluentAssertions;
using Nada.Core.Replacer;
using Nada.Core.Replacer.Handlers;
using NUnit.Framework;

// ReSharper disable InconsistentNaming

namespace Nada.Core.Tests.Replacer;

public class PropertyParserFactoryTests
{
    [Test]
    public void Pass_Null_CultureInfo()
    {
        var factory = new PropertyParserFactory();

        var action = () => factory.Create(null!);

        action.Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void Can_Find_Default_TokenType_Handlers()
    {
        var factory = new PropertyParserFactory();

        var parser = factory.Create(CultureInfo.InvariantCulture) as PropertyParser;

        parser.Should().NotBeNull();
        parser!.TokenTypeHandlers.Count().Should().BeGreaterThan(0);
    }

    [Test]
    public void Create_Using_Handler_With_CultureInfo_Constructor_As_Object()
    {
        var factory = new PropertyParserFactory();
        var replacements = new Dictionary<string, string>
        {
            { "key", "test" }
        };

        var parser = factory.Create(CultureInfo.InvariantCulture,
            new[] { new MockTokenTypeHandler_CultureInfoConstructor(CultureInfo.InvariantCulture) });
        var property = parser.Parse("Here some replaced string: {string|key}", replacements);

        property.Should().Be("Here some replaced string: cultureInfo_key_test");
    }

    [Test]
    public void Create_Using_Handler_Without_Parameter()
    {
        var factory = new PropertyParserFactory();
        var replacements = new Dictionary<string, string>
        {
            { "key", "test" }
        };

        var parser = factory.Create();
        var property = parser.Parse("Here some replaced string: {string|key}", replacements);

        property.Should().Be("Here some replaced string: test");
    }

    [Test]
    public void Create_Using_Handler_With_Empty_Constructor_As_Object()
    {
        var factory = new PropertyParserFactory();
        var replacements = new Dictionary<string, string>
        {
            { "key", "test" }
        };

        var parser = factory.Create(CultureInfo.InvariantCulture,
            new[] { new MockTokenTypeHandler_EmptyConstructor() });
        var property = parser.Parse("Here some replaced string: {string|key}", replacements);

        property.Should().Be("Here some replaced string: empty_key_test");
    }

    [Test]
    public void Create_Using_Handler_With_Unusable_Constructor_As_Object()
    {
        var factory = new PropertyParserFactory();
        var replacements = new Dictionary<string, string>
        {
            { "key", "test" }
        };

        var parser = factory.Create(CultureInfo.InvariantCulture,
            new[] { new MockTokenTypeHandler_UnusableConstructor_For_Reflection("mock") });
        var property = parser.Parse("Here some replaced string: {string|key}", replacements);

        property.Should().Be("Here some replaced string: unusable_for_reflection_key_test");
    }


    private class MockTokenTypeHandler_CultureInfoConstructor : ITokenTypeHandler
    {
        // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
        public MockTokenTypeHandler_CultureInfoConstructor(CultureInfo cultureInfo)
        {
            if (cultureInfo == null) throw new ArgumentNullException(nameof(cultureInfo));
        }

        public bool CanHandle(string dataType)
        {
            // Override dataTypeHandler "string"
            return string.Equals(dataType, "string", StringComparison.OrdinalIgnoreCase);
        }

        public TokenHandlerResult Handle(string key, string value, string additionalInformation,
            IDictionary<string, string>? @params)
        {
            return new TokenHandlerResult($"cultureInfo_{key}_{value}");
        }
    }

    private class MockTokenTypeHandler_EmptyConstructor : ITokenTypeHandler
    {
        public bool CanHandle(string dataType)
        {
            // Override dataTypeHandler "string"
            return string.Equals(dataType, "string", StringComparison.OrdinalIgnoreCase);
        }

        public TokenHandlerResult Handle(string key, string value, string additionalInformation,
            IDictionary<string, string>? @params)
        {
            return new TokenHandlerResult($"empty_{key}_{value}");
        }
    }

    private class MockTokenTypeHandler_UnusableConstructor_For_Reflection : ITokenTypeHandler
    {
        // ReSharper disable once UnusedParameter.Local
        public MockTokenTypeHandler_UnusableConstructor_For_Reflection(string mockParameter)
        {
        }

        public bool CanHandle(string dataType)
        {
            // Override dataTypeHandler "string"
            return string.Equals(dataType, "string", StringComparison.OrdinalIgnoreCase);
        }

        public TokenHandlerResult Handle(string key, string value, string additionalInformation,
            IDictionary<string, string>? @params)
        {
            return new TokenHandlerResult($"unusable_for_reflection_{key}_{value}");
        }
    }
}