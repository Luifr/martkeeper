using Godot;
using Martkeeper.Resources;

namespace Martkeeper.Entities;

public partial class ShelfLocation : CollisionShape2D
{
  private Product _product;

  [Export]
  public Product Product
  {
    get => _product;
    set
    {
      _product = value;
      _productSprite.Texture = value.Texture;
      UpdateProductCountLabel();
    }
  }

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

  public override void _Ready()
  {
    _productSprite = GetNode<Sprite2D>("%ProductSprite");
    _productCountLabel = GetNode<Label>("%ProductCount");

    if (Product != null)
    {
      _productSprite.Texture = Product.Texture;
      UpdateProductCountLabel();
    }
  }

  private void UpdateProductCountLabel()
  {
    _productCountLabel.Text = _productCount.ToString();
  }
}
