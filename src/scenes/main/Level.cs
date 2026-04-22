using System.Diagnostics;
using Godot;
using Martkeeper.Entities;
using Martkeeper.UI;

namespace Martkeeper;

public partial class Level : Node
{
  private Player _player;
  private CashRegister _cashRegister;

  public override void _Ready()
  {
    _player = GetNode<Player>("%Player");
    _cashRegister = GetNode<CashRegister>("%CashRegister");

    Debug.Assert(_player != null, "Player not found in level");
    Debug.Assert(_cashRegister != null, "Cash Register not found in level");

    _player.InteractCashRegister += _ =>
    {
      _cashRegister.PlayerInteraction();
    };

    UiStack.Instance.OnEmptyCancelPress += PauseMenu.Instance.PauseGame;
  }

  public override void _ExitTree()
  {
    UiStack.Instance.OnEmptyCancelPress -= PauseMenu.Instance.PauseGame;
  }
}
