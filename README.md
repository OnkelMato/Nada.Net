# nada.net

Nada.net is "not another dotnet API" with the purpose of a function library with fully-tested functions.

## Stats

### SonarQube Code Analysis

[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=ThomasLey_Nada.Net&metric=coverage)](https://sonarcloud.io/summary/new_code?id=ThomasLey_Nada.Net) [![Code Smells](https://sonarcloud.io/api/project_badges/measure?project=ThomasLey_Nada.Net&metric=code_smells)](https://sonarcloud.io/summary/new_code?id=ThomasLey_Nada.Net)

### Nuget Package Statistics

[![NuGet Badge](https://buildstats.info/nuget/Nada)](https://www.nuget.org/packages/Nada/)

## Examples

### List-Tree-Converter

Converting a list view into a tree is not a big deal but requires code. `Nada.net` has an extension method that created a hierarchical structure based on a list with id and parent id.

#### Convert a list to a tree

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
