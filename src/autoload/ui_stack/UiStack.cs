using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

namespace Martkeeper;

public partial class UiStack : Node
{
  private int lastId = 0;

  private readonly struct UIStackEntry(int _sequentailId, Action _closeHandler)
  {
    public readonly int sequentailId = _sequentailId;
    public readonly Action CloseHandler = _closeHandler;
  }

  [Signal]
  public delegate void OnEmptyCancelPressEventHandler();

  public static UiStack Instance;

  private List<UIStackEntry> _stack = new();

  public override void _EnterTree()
  {
    if (Instance != null)
    {
      GD.PushError("Duplicate instance of UIStack");
      QueueFree();
      return;
    }

    Instance = this;
  }

  public override void _Ready()
  {
    var pauseMenu = GD.Load<PackedScene>("uid://cf2g65wxefua5").Instantiate();
    AddChild(pauseMenu);
  }

  public int Push(Action closeHandler)
  {
    int newId = lastId;
    _stack.Add(new UIStackEntry(newId, closeHandler));

    lastId++;

    return newId;
  }

  public int Pop()
  {
    if (_stack.Count == 0)
    {
      EmitSignalOnEmptyCancelPress();
      return -1;
    }

    var lastItem = _stack.Last();
    lastItem.CloseHandler();

    _stack.RemoveAt(_stack.Count - 1);

    return lastItem.sequentailId;
  }

  public override void _UnhandledInput(InputEvent @event)
  {
    if (@event.IsActionPressed("ui_cancel"))
    {
      Pop();
      GetViewport().SetInputAsHandled();
    }
  }
}
