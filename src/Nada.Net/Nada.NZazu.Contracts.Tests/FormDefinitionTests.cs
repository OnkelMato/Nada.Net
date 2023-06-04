using FluentAssertions;
using NUnit.Framework;

namespace Nada.NZazu.Contracts.Tests;

[TestFixture]
// ReSharper disable once InconsistentNaming
public class FormDefinitionTests
{
    [Test]
    public void Be_Creatable()
    {
        var sut = new FormDefinition();

        sut.Should().NotBeNull();
        sut.Fields.Should().BeNull();
    }
}