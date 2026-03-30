using Godot;
using MartKeeper.Core;

public partial class Stock : StaticBody2D
{

	[Export] public Product product;

	private Sprite2D _backgroundSprite;
	private Sprite2D _productSprite;

	public override void _Ready()
	{
		_backgroundSprite = GetNode<Sprite2D>("BackgroundSprite");
		_productSprite = GetNode<Sprite2D>("%ProductSprite");

		if (_backgroundSprite.Texture is GradientTexture1D gradientTexture)
		{
			gradientTexture.Gradient.SetColor(0, new Color(GD.Randf(), GD.Randf(), GD.Randf()));
		}

		if (product != null)
		{
			_productSprite.Texture = product.Texture;
		}
	}
}
