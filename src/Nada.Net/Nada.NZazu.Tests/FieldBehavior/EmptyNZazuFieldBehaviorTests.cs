using FluentAssertions;
using Nada.NZazu.FieldBehavior;
using NSubstitute;
using NUnit.Framework;

namespace Nada.NZazu.Tests.FieldBehavior;

[TestFixture]
// ReSharper disable once InconsistentNaming
public class EmptyNZazuFieldBehaviorTests
{
    [Test]
    public void Be_Creatable()
    {
        var sut = new EmptyNZazuFieldBehavior();
        sut.Should().NotBeNull();
        sut.Should().BeAssignableTo<INZazuWpfFieldBehavior>();
        sut.GetType().Name.Should().StartWith("Empty");
    }

    [Test]
    public void Have_guard_clauses_on_ctor()
    {
        var sut = new EmptyNZazuFieldBehavior();

        sut.Invoking(x => x.AttachTo(null, null)).Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void Do_Nothing()
    {
        var field = Substitute.For<INZazuWpfField>();
        var sut = new EmptyNZazuFieldBehavior();

        sut.AttachTo(field, null!);
        sut.Detach();

        field.ReceivedCalls().Should().BeEmpty();
    }
}