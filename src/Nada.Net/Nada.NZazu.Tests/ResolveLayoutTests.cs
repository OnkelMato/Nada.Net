using FluentAssertions;
using Nada.NZazu.LayoutStrategy;
using NUnit.Framework;

namespace Nada.NZazu.Tests
{
    [TestFixture]
    // ReSharper disable once InconsistentNaming
    public class ResolveLayoutTests
    {
        [Test]
        public void Be_Creatable()
        {
            var sut = new ResolveLayout();

            sut.Should().NotBeNull();
            sut.Should().BeAssignableTo<IResolveLayout>();
            sut.Should().BeAssignableTo<IResolveFromName<INZazuWpfLayoutStrategy>>();
        }

        [Test]
        public void Have_GridLayout_As_Default()
        {
            var sut = new ResolveLayout();

            sut.Resolve(null).Should().BeAssignableTo<GridLayout>();
        }

        [Test]
        public void Resolve_StackedLayout()
        {
            var sut = new ResolveLayout();

            sut.Resolve("stack").Should().BeAssignableTo<StackedLayout>();
        }
    }
}