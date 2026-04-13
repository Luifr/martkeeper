using Godot;
using Martkeeper.Resources;

namespace Martkeeper.Entities;

public partial class ShelfLocation : CollisionShape2D
{
  [Export]
  public Product product;

  public int _productCount = 1;
  public int ProductCount
  {
    get => _productCount;
    set
    {
      _productCount = value;
      UpdateProductCountLabel();
    }
  }

  private Sprite2D _productSprite;
  private Label _productCountLabel;

  // Called when the node enters the scene tree for the first time.
  public override void _Ready()
  {
    _productSprite = GetNode<Sprite2D>("%ProductSprite");
    _productCountLabel = GetNode<Label>("%ProductCount");

    if (product != null)
    {
      _productSprite.Texture = product.Texture;
      UpdateProductCountLabel();
    }
  }

  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(double delta) { }

  private void UpdateProductCountLabel()
  {
    _productCountLabel.Text = _productCount.ToString();
  }
}
