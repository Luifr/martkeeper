using System;
using Godot;

namespace Martkeeper.Entities;

public partial class HeadingForProductCustomerState : Node, ICustomerState
{
  public CustomerState State { get; } = CustomerState.HEADING_FOR_PRODUCT;
  private Customer _customer;
  private Action<CustomerState> _changeState;

  public void Init(Action<CustomerState> ChangeState, Customer customer)
  {
    _changeState = ChangeState;
    _customer = customer;
  }

  public void Enter()
  {
    GD.Print("Customer heading for product state");
    // TODO: set target position
  }

  public void Update(double delta)
  {
    // If reached target position, try to take an item, if there is a matching item there, move to next state
  }

  public void Exit() { }
}
