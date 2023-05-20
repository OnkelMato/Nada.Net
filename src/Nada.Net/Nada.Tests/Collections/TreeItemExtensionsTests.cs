using FluentAssertions;
using Nada.Collections;
using NUnit.Framework;

namespace Nada.Tests.Collections
{
    [TestFixture]
    public class TreeItemExtensionsTests
    {
        private class Category
        {
            public Category(int id, string title, int parentId)
            {
                Id = id;
                Title = title;
                ParentId = parentId;
            }

            public int Id { get; }
            public string Title { get; }
            public int ParentId { get; }
        }

        [Test]
        public void GenerateTree_Converts_List_To_Tree()
        {
            var categories = new List<Category>() {
                new Category(1, "Sport", 0),
                new Category(2, "Balls", 1),
                new Category(3, "Shoes", 1),
                new Category(4, "Electronics", 0),
                new Category(5, "Cameras", 4),
                new Category(6, "Lenses", 5),
                new Category(7, "Tripod", 5),
                new Category(8, "Computers", 4),
                new Category(9, "Laptops", 8),
                new Category(10, "Empty", 0),
                new Category(-1, "Broken", 999),
            };

            var root = categories.GenerateTree(c => c.Id, c => c.ParentId);

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            root.First().Item.Title.Should().Be("Sport");
            root.Skip(1).First().Item.Title.Should().Be("Electronics");
            root.Skip(2).First().Item.Title.Should().Be("Empty");
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }
    }
}