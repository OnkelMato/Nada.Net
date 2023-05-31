using FluentAssertions;
using NUnit.Framework;

namespace Nada.NZazu.Contracts.Tests
{
    [TestFixture]
    // ReSharper disable InconsistentNaming
    public class BehaviorDefinitionTests
    {
        [Test]
        public void Be_Creatable()
        {
            var sut = new BehaviorDefinition
            {
                Name = "name",
                Settings = new Dictionary<string, string> {{"key1", "value1"}, {"key2", "value2"}}
            };

            sut.Name.Should().Be("name");
            sut.Settings["key1"].Should().Be("value1");
            sut.Settings["key2"].Should().Be("value2");
        }
    }
}