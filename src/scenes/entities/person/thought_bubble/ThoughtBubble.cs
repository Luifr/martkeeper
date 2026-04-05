using Godot;

namespace Martkeeper.Entities;

[Tool]
public partial class ThoughtBubble : Node2D
{
	[Export]
	public float Width = 10;

	private Sprite2D _sprite;

	public override void _Ready()
	{
		_sprite = GetNode<Sprite2D>("Sprite2D");
	}

	public void UpdateTexture(Texture2D texture)
	{
		_sprite.Texture = texture;
	}

	public void ClearTexture()
	{
		_sprite.Texture = null;
	}

	public override void _Draw()
	{
		Color white = new Color(1, 1, 1);
		DrawEllipse(Position, Width, Width / 2f, white, true);
		DrawPolygon(
			[
				Position + new Vector2(-Width * 1.1f, Width / 2 * 1.1f),
				Position + new Vector2(-Width / 3, 0),
				Position + new Vector2(0, Width / 3),
			],
			[white]
		);
	}
}
