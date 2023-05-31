using FluentAssertions;
using Nada.Core.Extensions;
using NUnit.Framework;

namespace Nada.Core.Tests.Extensions;

[TestFixture]
internal class LinqExtensionsTests
{
    [Test]
    public void IsNullOrEmpty_With_Null()
    {
        var lst = (IEnumerable<string>)null!;

        lst.IsNullOrEmpty().Should().BeTrue();
    }

    [Test]
    public void IsNullOrEmpty_With_Empty()
    {
        var lst = Enumerable.Empty<string>();

        lst.IsNullOrEmpty().Should().BeTrue();
    }

    [Test]
    public void IsNullOrEmpty_With_Array_With_Elements()
    {
        var lst = new[] { "Avengers" };

        lst.IsNullOrEmpty().Should().BeFalse();
    }

    [Test]
    public void ForEach_With_Null()
    {
        var cnt = 0;
        var lst = (IEnumerable<string>)null!;

        lst.ForEach(x => cnt++);
        cnt.Should().Be(0);
    }

    [Test]
    public void ForEach_With_Empty()
    {
        var cnt = 0;
        var lst = Enumerable.Empty<string>();

        lst.ForEach(x => cnt++);
        cnt.Should().Be(0);
    }

    [Test]
    public void ForEach_With_Array_With_Elements()
    {
        var cnt = 0;
        var lst = new[] { "Avengers", "Team Marvel" };

        lst.ForEach(x => cnt++);
        cnt.Should().Be(2);
    }


    [Test]
    public void ForEach_With_Array_With_Null_Elements()
    {
        var cnt = 0;
        var lst = new[] { "Avengers", null, "Hulk" };

        lst.ForEach(x => cnt++);
        cnt.Should().Be(3);
    }
}