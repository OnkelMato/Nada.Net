using FluentAssertions;
using Nada.NZazu.Contracts.FormChecks;
using Nada.NZazu.Contracts.Tests.Helper;
using NUnit.Framework;

namespace Nada.NZazu.Contracts.Tests.FormChecks
{
    [TestFixture]
    // ReSharper disable InconsistentNaming
    internal class GreaterThanFormCheckTests
    {
        [Test]
        public void Be_Creatable()
        {
            var settings = new Dictionary<string, string>
            {
                {"Hint", "this is the hint"},
                {"LeftFieldName", "leftField"},
                {"RightFieldName", "rightField"}
            } as IDictionary<string, string>;

            var ctx = new ContextFor<GreaterThanFormCheck>();
            ctx.Use(settings);
            var sut = ctx.BuildSut();

            sut.Should().NotBeNull();
        }

        [Test]
        public void Deal_With_Not_Exiting_Fields()
        {
            var settings = new Dictionary<string, string>
            {
                {"Hint", "this is the hint"},
                {"LeftFieldName", "leftField"},
                {"RightFieldName", "rightField"}
            } as IDictionary<string, string>;

            var sut = new GreaterThanFormCheck(settings);
            FormData foo = new Dictionary<string, string>
            {
                {"leftField", ""},
                {"rightField", ""}
            };

            sut.Validate(foo);
        }
    }
}