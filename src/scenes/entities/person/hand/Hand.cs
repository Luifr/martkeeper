using Godot;
using Martkeeper.Resources;

namespace Martkeeper.Entities;

public partial class Hand : Node2D
{
  private Item _currentItem;
  private Sprite2D _sprite;

  [Export]
  public Item CurrentItem
  {
    get => _currentItem;
    set
    {
      _currentItem = value;
      UpdateSprite();
    }
  }

  public override void _Ready()
  {
    _sprite = GetNode<Sprite2D>("ItemSprite");
  }

  private void UpdateSprite()
  {
    if (_sprite == null)
      return;

    if (_currentItem != null)
    {
      _sprite.Texture = _currentItem.Texture;
      _sprite.Visible = true;
    }
    else
    {
      _sprite.Visible = false;
    }
  }
}
