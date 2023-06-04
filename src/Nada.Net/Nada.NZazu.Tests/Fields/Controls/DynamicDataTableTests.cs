using FluentAssertions;
using Nada.NZazu.Fields.Controls;
using NUnit.Framework;

namespace Nada.NZazu.Tests.Fields.Controls;

[TestFixture]
#pragma warning disable 618
// ReSharper disable once InconsistentNaming
public class DynamicDataTableTests
{
    [Test]
    [Apartment(ApartmentState.STA)]
    public void Be_Creatable()
    {
        var sut = new DynamicDataTable();

        sut.Should().NotBeNull();
    }
}
#pragma warning restore 618