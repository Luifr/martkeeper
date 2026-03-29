using Godot;

namespace MartKeeper.Core;

[GlobalClass]
// Base Class for Products, Tools and so on
public partial class Item : Resource
{
  // Used like an Id, to display use GetName
  [Export] public string NameKey;
  [Export] public Texture2D Texture;

  public string GetDisplayName() => Tr(NameKey);
}
