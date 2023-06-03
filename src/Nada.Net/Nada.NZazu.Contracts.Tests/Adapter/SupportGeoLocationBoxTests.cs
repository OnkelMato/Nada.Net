using FluentAssertions;
using Nada.NZazu.Contracts.Adapter;
using NUnit.Framework;

namespace Nada.NZazu.Contracts.Tests.Adapter
{
    [TestFixture]
    // ReSharper disable once InconsistentNaming
    public class SupportGeoLocationBoxTests
    {
        [Test]
        public void Be_Creatble()
        {
            var sut = new SupportGeoLocationBox();
            sut.Should().NotBeNull();

            var data = new NZazuCoordinate {Lat = 23.4, Lon = 56.7};
            data.Should().BeEquivalentTo(sut.Parse(sut.ToString(data)));
        }
    }
}