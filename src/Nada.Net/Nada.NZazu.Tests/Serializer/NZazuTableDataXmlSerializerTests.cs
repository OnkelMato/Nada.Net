using FluentAssertions;
using Nada.NZazu.Contracts;
using Nada.NZazu.Serializer;
using NUnit.Framework;

namespace Nada.NZazu.Tests.Serializer
{
    [TestFixture]
    // ReSharper disable once InconsistentNaming
    public class NZazuTableDataXmlSerializerTests
    {
        [Test]
        public void Be_Creatable()
        {
            var sut = new NZazuTableDataXmlSerializer();

            sut.Should().NotBeNull();
            sut.Should().BeAssignableTo<INZazuTableDataSerializer>();
        }

        [Test]
        public void Be_Symetric()
        {
            var data = new Dictionary<string, string>
            {
                {"Jane", "Doe"},
                {"John", "Smith"}
            };

            var sut = new NZazuTableDataXmlSerializer();

            var actual = sut.Serialize(data);
            var expected = sut.Deserialize(actual);

            foreach (var item in data)
                expected.Should().Contain(item);
        }

        [Test]
        public void Handle_Empty_Symetric()
        {
            var data = new Dictionary<string, string>();
            var sut = new NZazuTableDataXmlSerializer();

            var actual = sut.Serialize(data);
            var expected = sut.Deserialize(actual);

            expected.Should().NotBeNull();
            expected.Count.Should().Be(0);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("         ")]
        public void Handle_Empty_Stuff(string data)
        {
            var sut = new NZazuTableDataXmlSerializer();

            var expected = sut.Deserialize(data);

            expected.Should().NotBeNull();
            expected.Count.Should().Be(0);
        }
    }
}