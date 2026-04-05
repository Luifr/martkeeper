using System.Diagnostics;
using Godot;
using Martkeeper.Entities;

namespace Martkeeper;

public partial class Level : Node
{
  private Player _player;
  private CashRegister _cashRegister;

  // Called when the node enters the scene tree for the first time.
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
  }

  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(double delta) { }
}
