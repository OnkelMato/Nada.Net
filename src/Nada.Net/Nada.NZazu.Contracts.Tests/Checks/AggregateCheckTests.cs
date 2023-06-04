using FluentAssertions;
using Nada.NZazu.Contracts.Checks;
using NSubstitute;
using NUnit.Framework;

namespace Nada.NZazu.Contracts.Tests.Checks;

[TestFixture]
// ReSharper disable InconsistentNaming
public class AggregateCheckTests
{
    [Test]
    public void Be_Creatable()
    {
        var sut = new AggregateCheck(Enumerable.Empty<IValueCheck>());

        sut.Should().NotBeNull();
        sut.Should().BeAssignableTo<IValueCheck>();
    }

    [Test]
    public void ValidateTests_Accept_Null_Results_As_Success()
    {
        var check1 = Substitute.For<IValueCheck>();
        var check2 = Substitute.For<IValueCheck>();
        check1.Validate(null, null).Returns((ValueCheckResult)null!);
        check2.Validate(null, null).Returns((ValueCheckResult)null!);

        var sut = new AggregateCheck(new[] { check1, check2 });
        var result = sut.Validate(null, null, null!);

        result.Should().NotBeNull();
        result.IsValid.Should().BeTrue();
        result.Exception.Should().BeNull();
    }

    [Test]
    public void Validate_should_run_all_checks()
    {
        var check1 = Substitute.For<IValueCheck>();
        var check2 = Substitute.For<IValueCheck>();
        check1.Validate(null, null).Returns((ValueCheckResult)null!);
        check2.Validate(null, null).Returns((ValueCheckResult)null!);

        var sut = new AggregateCheck(new[] { check1, check2 });
        var result = sut.Validate(null, null, null!);

        result.Should().NotBeNull();
        result.IsValid.Should().BeTrue();
        result.Exception.Should().BeNull();

        check1.Received(1).Validate(null, null!);
        check2.Received(1).Validate(null, null!);
    }

    [Test]
    public void Aggregate_Single_Error_Results()
    {
        var exception = new Exception();
        var checkResult1 = new ValueCheckResult(exception);
        var checkResult2 = new ValueCheckResult(true);
        var check1 = Substitute.For<IValueCheck>();
        var check2 = Substitute.For<IValueCheck>();
        check1.Validate(null, null).Returns(checkResult1);
        check2.Validate(null, null).Returns(checkResult2);

        var sut = new AggregateCheck(new[] { check1, check2 });
        var result = sut.Validate(null, null, null!);

        result.Should().NotBeNull();
        result.Should().Be(checkResult1, "we return the one and only error");
        result.IsValid.Should().BeFalse();
        result.Exception.Should().Be(exception);
    }

    [Test]
    public void Aggregate_Multiple_Error_Results()
    {
        var exception1 = new Exception();
        var exception2 = new Exception();
        var check1 = Substitute.For<IValueCheck>();
        var check2 = Substitute.For<IValueCheck>();
        check1.Validate(null, null).Returns(new ValueCheckResult(exception1));
        check2.Validate(null, null).Returns(new ValueCheckResult(exception2));

        var sut = new AggregateCheck(new[] { check1, check2 });
        var result = sut.Validate(null, null, null!);

        result.Should().NotBeNull();
        result.Should().BeOfType<AggregateValueCheckResult>("we return an aggregation of all errors");
        result.IsValid.Should().BeFalse();
        result.Exception.Should().BeOfType<AggregateException>();
    }
}