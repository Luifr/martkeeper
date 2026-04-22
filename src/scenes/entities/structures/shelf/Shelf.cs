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
      var shelfLocation = child as ShelfLocation;
      if (shelfLocation == null)
        return;

      shelfLocation.Product = ResourceConstants.AllProductsResource.Products.PickRandom();
    }
  }

  private void InitializeProductPositions()
  {
    var children = FindChildren("Marker2D", "Marker2D");

    foreach (Node2D marker2D in children)
    {
      var shelfLocation = marker2D.GetParent() as ShelfLocation;
      if (shelfLocation == null)
        return;

      var productKey = shelfLocation.Product.NameKey;
      if (!productToGlobalPositions.ContainsKey(productKey))
        productToGlobalPositions.Add(productKey, new List<Vector2>());

      productToGlobalPositions[productKey].Add(marker2D.GlobalPosition);
    }
  }
}
