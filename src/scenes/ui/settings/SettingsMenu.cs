using Godot;

namespace Martkeeper.UI;

public partial class SettingsMenu : Control
{
  public Button _accept;

  [Signal]
  public delegate void ApplyEventHandler();

  public override void _Ready()
  {
    _accept = GetNode<Button>("%AcceptButton");

    _accept.Pressed += () => EmitSignal(SignalName.Apply);
  }
}
