using FluentAssertions;
using Nada.NZazu.Fields;
using NUnit.Framework;

namespace Nada.NZazu.Tests.Fields
{
    [TestFixture]
    // ReSharper disable once InconsistentNaming
    internal class FieldFactoryExtensionsTests
    {
        [Test]
        public void Be_Static()
        {
            var type = typeof(FieldFactoryExtensions);

            type.IsAbstract.Should().BeTrue();
            type.IsSealed.Should().BeTrue();
        }
    }
}