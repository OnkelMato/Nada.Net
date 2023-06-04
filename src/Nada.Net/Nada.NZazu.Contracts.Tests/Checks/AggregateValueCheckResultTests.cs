using FluentAssertions;
using Nada.NZazu.Contracts.Checks;
using NUnit.Framework;

namespace Nada.NZazu.Contracts.Tests.Checks;

[TestFixture]
public class AggregateValueCheckResultTests
{
    [Test]
    public void Be_Creatable()
    {
        var sut = new AggregateValueCheckResult(Enumerable.Empty<ValueCheckResult>());

        sut.Should().NotBeNull();
    }
}