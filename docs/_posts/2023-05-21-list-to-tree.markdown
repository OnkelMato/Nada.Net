---
layout: post
title:  "List to Tree converter"
date:   2023-05-21 12:26:22 +0200
categories: nada.net
---

# List to Tree Converter

Converting a list view into a tree is not a big deal but requires code. `Nada.net` has an extension method that created a hierarchical structure based on a list with id and parent id.

```cs
// data class with an id and a parent id.
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

    IEnumerable<TreeItem<Category>> roots = categories.GenerateTree(c => c.Id, c => c.ParentId);

    roots.First().Item.Title.Should().Be("Sport");
    roots.Skip(1).First().Item.Title.Should().Be("Electronics");
    roots.Skip(2).First().Item.Title.Should().Be("Empty");

```
