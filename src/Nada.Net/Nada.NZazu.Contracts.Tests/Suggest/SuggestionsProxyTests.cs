using FluentAssertions;
using Nada.NZazu.Contracts.Suggest;
using Nada.NZazu.Contracts.Tests.Helper;
using NUnit.Framework;

namespace Nada.NZazu.Contracts.Tests.Suggest
{
    [TestFixture]
    // ReSharper disable once InconsistentNaming
    internal class SuggestionsProxyTests
    {
        [Test]
        public void Be_Creatable()
        {
            var ctx = new ContextFor<SuggestionsProxy>();
            var sut = ctx.BuildSut();

            sut.Should().NotBeNull();
            sut.Should().BeAssignableTo<IProvideSuggestions>();

            sut.BlackListSize = 69;
            sut.BlackListSize.Should().Be(69);

            sut.CacheSize = 169;
            sut.CacheSize.Should().Be(169);
        }

        [Test]
        public void Cache()
        {
            var ctx = new ContextFor<SuggestionsProxy>();
            var sut = ctx.BuildSut();

            var result = sut.For("xyz", null);
            result.Should().BeNull();
        }
    }
}