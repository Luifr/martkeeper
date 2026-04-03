using Godot;
using Martkeeper.Resources;

namespace Martkeeper.Entities;

public partial class Shelf : StaticBody2D
{
  [Export]
  public Product product;
  private int _productCount = 1;

  public int ProductCount
  {
    get => _productCount;
    set
    {
      _productCount = value;
      UpdateProductCountLabel();
    }
  }

  private Sprite2D _backgroundSprite;
  private Sprite2D _productSprite;
  private Label _productCountLabel;

  public override void _Ready()
  {
    _backgroundSprite = GetNode<Sprite2D>("BackgroundSprite");
    _productSprite = GetNode<Sprite2D>("%ProductSprite");
    _productCountLabel = GetNode<Label>("%ProductCount");

    if (_backgroundSprite.Texture is GradientTexture1D gradientTexture)
    {
      gradientTexture.Gradient.SetColor(0, new Color(GD.Randf(), GD.Randf(), GD.Randf()));
    }

    if (product != null)
    {
      _productSprite.Texture = product.Texture;
      UpdateProductCountLabel();
    }
  }

  public override void _Process(double delta) { }

  private void UpdateProductCountLabel()
  {
    _productCountLabel.Text = _productCount.ToString();
  }
}
