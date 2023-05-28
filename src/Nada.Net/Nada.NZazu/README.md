# NADA.Net

## List-Tree-Converter

Converting a list view into a tree is not a big deal but requires code. `Nada.net` has an extension method that created a hierarchical structure based on a list with id and parent id.

### Convert a list to a tree

```cs
// data class with an id and a parent id.
private class Category
{
    public int Id { get; }
    public int ParentId { get; }
}

[Test]
public void GenerateTree_Converts_List_To_Tree()
{
    var categories = new List<Category>() {};

    IEnumerable<TreeItem<Category>> roots = categories.GenerateTree(c => c.Id, c => c.ParentId);
```
