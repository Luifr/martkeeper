using System;
using Godot;

public partial class SettingsMenu : Control
{
  public Button _accept;

  [Signal]
  public delegate void ApplyEventHandler();

  // Called when the node enters the scene tree for the first time.
  public override void _Ready()
  {
    _accept = GetNode<Button>("%AcceptButton");

    _accept.Pressed += () => EmitSignal(SignalName.Apply);
  }

  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(double delta) { }
}
