using System.Collections.Generic;
using Godot;
using Martkeeper.Constants;

namespace Martkeeper.Entities;

public partial class Shelf : StaticBody2D
{
  [Export]
  private bool selfInitializeProducts;

  public Dictionary<string, List<Vector2>> productToGlobalPositions = new();

  public override void _Ready()
  {
    if (selfInitializeProducts)
      InitializeShelfLocations();
    InitializeProductPositions();
  }

  private void InitializeShelfLocations()
  {
    foreach (var child in GetChildren())
    {
      if (child is ShelfLocation shelfLocation)
      {
        shelfLocation.Product = ResourceConstants.AllProductsResource.Products.PickRandom();
      }
    }
  }

  private void InitializeProductPositions()
  {
    var children = FindChildren("Marker2D", "Marker2D");

    foreach (var child in children)
    {
      if (child is Marker2D marker)
      {
        var parent = marker.GetParent();
        if (parent is ShelfLocation shelfLocation)
        {
          var productKey = shelfLocation.Product.NameKey;
          if (!productToGlobalPositions.ContainsKey(productKey))
            productToGlobalPositions.Add(productKey, new List<Vector2>());

          productToGlobalPositions[productKey].Add(marker.GlobalPosition);
        }
      }
    }
  }
}
