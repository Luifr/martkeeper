using Godot;
using Godot.Collections;
using Martkeeper.Resources; // Ensure this matches your AllProducts namespace

namespace Martkeeper.Tools;

[Tool]
public partial class ProductScanner : Node
{
  public string SavePath = "res://data/all_products.tres";

  public string ResourceFolderPath = "res://data/products";

  private bool _refreshList = false;

  [Export]
  public bool RefreshList
  {
    get => _refreshList;
    set
    {
      if (value)
        UpdateAndSaveResource();
      _refreshList = false;
    }
  }

  public void Execute()
  {
    UpdateAndSaveResource();
  }

  private void UpdateAndSaveResource()
  {
    if (string.IsNullOrEmpty(ResourceFolderPath) || string.IsNullOrEmpty(SavePath))
    {
      GD.PrintErr("ProductScanner: Set both folder path and save path first");
      return;
    }

    // 1. Create the container resource
    var container = new AllProducts
    {
      Products = new()
    };

    using var dir = DirAccess.Open(ResourceFolderPath);
    if (dir == null)
    {
      GD.PrintErr($"ProductScanner: Could not open path: {ResourceFolderPath}");
      return;
    }

    dir.ListDirBegin();
    string fileName = dir.GetNext();

    while (fileName != "")
    {
      if (!dir.CurrentIsDir() && fileName.EndsWith(".tres"))
      {
        string fullPath = ResourceFolderPath.PathJoin(fileName);
        var res = GD.Load<Product>(fullPath);

        if (res != null)
        {
          container.Products.Add(res);
        }
      }
      fileName = dir.GetNext();
    }

    // 2. Save the container to a .tres file
    Error err = ResourceSaver.Save(container, SavePath);

    if (err == Error.Ok)
    {
      GD.Print($"ProductScanner: Successfully saved {container.Products.Count} products to {SavePath}");
      // Refresh the editor filesystem so the new file appears immediately
      // EditorInterface is not available at run time
      if (Engine.IsEditorHint())
        EditorInterface.Singleton.GetResourceFilesystem().Scan();
    }
    else
    {
      GD.PrintErr($"ProductScanner: Failed to save resource! Error: {err}");
    }
  }
}
