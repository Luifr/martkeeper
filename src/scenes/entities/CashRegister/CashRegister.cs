using System;
using System.Collections.Generic;
using System.Diagnostics;
using Godot;
using Martkeeper.Entities;

public partial class CashRegister : StaticBody2D
{
  private readonly List<ulong> WaitingCustomersIds = [];

  public override void _Ready() { }

  public override void _Process(double delta) { }

  public void PlayerInteraction()
  {
    if (WaitingCustomersIds.Count == 0)
    {
      Debug.Print("No customers right now");
      return;
    }

    throw new Exception("Not implemented");
  }

  public void AddWaitingCustomer(Customer customer)
  {
    WaitingCustomersIds.Add(customer.GetInstanceId());
  }
}
