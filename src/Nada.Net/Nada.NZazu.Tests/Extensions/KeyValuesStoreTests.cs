using FluentAssertions;
using Nada.NZazu.Extensions;
using NUnit.Framework;

namespace Nada.NZazu.Tests.Extensions
{
    [TestFixture]
    // ReSharper disable once InconsistentNaming
    internal class KeyValuesStoreTests
    {
        [Test]
        public void Be_Creatable_And_Empty()
        {
            var sut = new KeyValuesStore();

            sut.Should().NotBeNull();
            sut.GetValues("foo").Should().BeEmpty();
            sut.GetValues("").Should().BeEmpty();

            Action tryGetNull = () => { sut.GetValues(null); };

            tryGetNull.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Add_And_Return_Values()
        {
            var sut = new KeyValuesStore();

            sut.Add("name", "jane");
            sut.Add("name", "john");

            var values = sut.GetValues("name").ToArray();

            values.Count().Should().Be(2);
            values.Should().Contain("jane");
            values.Should().Contain("john");
        }

        [Test]
        public void Return_All_On_Empty_Key()
        {
            var sut = new KeyValuesStore();

            sut.Add("name", "jane");
            sut.Add("name", "john");
            sut.Add("lastname", "smith");

            var values = sut.GetValues(string.Empty).ToArray();

            values.Count().Should().Be(3);
            values.Should().Contain("jane");
            values.Should().Contain("john");
            values.Should().Contain("smith");
        }
    }
}