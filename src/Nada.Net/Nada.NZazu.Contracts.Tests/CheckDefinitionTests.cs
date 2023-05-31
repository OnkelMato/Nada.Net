using FluentAssertions;
using NUnit.Framework;

namespace Nada.NZazu.Contracts.Tests
{
    [TestFixture]
    // ReSharper disable InconsistentNaming
    public class CheckDefinitionTests
    {
        [Test]
        public void Be_Creatable()
        {
            var settings = new Dictionary<string, string> {{"key1", "value1"}, {"key2", "value2"}};
            var sut = new CheckDefinition {Type = "type", Settings = settings};
            sut.Type.Should().Be("type");
            sut.Settings.Should().BeEquivalentTo(settings);
        }
    }
}