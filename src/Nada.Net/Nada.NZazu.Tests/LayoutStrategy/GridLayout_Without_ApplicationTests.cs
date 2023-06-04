using FluentAssertions;
using Nada.NZazu.LayoutStrategy;
using NUnit.Framework;

namespace Nada.NZazu.Tests.LayoutStrategy;

[TestFixture]
// ReSharper disable InconsistentNaming
public class GridLayout_Without_ApplicationTests
{
    [Test]
    public void Be_Creatable()
    {
        var sut = new GridLayout();
        sut.Should().NotBeNull();
        sut.Should().BeAssignableTo<INZazuWpfLayoutStrategy>();
    }
}