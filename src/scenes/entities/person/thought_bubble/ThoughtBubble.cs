using Godot;

namespace Martkeeper.Entities;

public partial class ThoughtBubble : Node2D
{
  [Export]
  public float Width = 35;

  private Sprite2D _sprite;
  private Node2D _parent;
  private Vector2 _initialOffset;

  public override void _Ready()
  {
    _initialOffset = Position + new Vector2(Width, -Width / 2);
    TopLevel = true;

    _sprite = GetNode<Sprite2D>("Sprite2D");
    _parent = GetParent<Node2D>();
  }

  public override void _Process(double _)
  {
    Position = _parent.Position + _initialOffset;
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
    Vector2 center = Vector2.Zero;

    DrawEllipse(center, Width, Width / 2f, white, true);
    DrawPolygon(
      [
        center + new Vector2(-Width * 1.1f, Width / 2 * 1.1f),
        center + new Vector2(-Width / 3, 0),
        center + new Vector2(0, Width / 3),
      ],
      [white]
    );
  }
}
