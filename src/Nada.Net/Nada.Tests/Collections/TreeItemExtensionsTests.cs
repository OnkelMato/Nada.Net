using FluentAssertions;
using Nada.Core.Collections;
using NUnit.Framework;

namespace Nada.Core.Tests.Collections;

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
        var categories = new List<Category>
        {
            new(1, "Sport", 0),
            new(2, "Balls", 1),
            new(3, "Shoes", 1),
            new(4, "Electronics", 0),
            new(5, "Cameras", 4),
            new(6, "Lenses", 5),
            new(7, "Tripod", 5),
            new(8, "Computers", 4),
            new(9, "Laptops", 8),
            new(10, "Empty", 0),
            new(-1, "Broken", 999)
        };

        var root = categories.GenerateTree(c => c.Id, c => c.ParentId);

        var all = root.ToArray();

        root.First().Item.Title.Should().Be("Sport");
        root.Skip(1).First().Item.Title.Should().Be("Electronics");
        root.Skip(2).First().Item.Title.Should().Be("Empty");
    }
}