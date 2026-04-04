using System.Collections.Generic;
using System.Diagnostics;
using Godot;

namespace Martkeeper.Entities;

public partial class CustomerStateMachine : Node
{
  [Export]
  public Customer TargetCustomer;

  private CustomerState _currentState = CustomerState.BASE;
  private Dictionary<CustomerState, ICustomerState> _customerStateHandlerHash = new();

  public override void _Ready()
  {
    Debug.Assert(TargetCustomer != null, "Customer not assigned to CustomerStateMachine");

    var children = GetChildren();

    foreach (Node child in children)
    {
      if (child is ICustomerState customerState)
      {
        _customerStateHandlerHash[customerState.State] = customerState;
        GD.Print("TargetCustomer in state machine ", TargetCustomer);
        customerState.Init(ChangeState, TargetCustomer);
      }
      else
      {
        GD.PushError("Child of CustomerStateMachine is not a ICustomerState ", child);
      }
    }

    _customerStateHandlerHash[_currentState].Enter();
  }

  private void ChangeState(CustomerState newState)
  {
    _customerStateHandlerHash[_currentState].Exit();
    _currentState = newState;
    _customerStateHandlerHash[_currentState].Enter();
  }

  public override void _Process(double delta)
  {
    _customerStateHandlerHash[_currentState].Update(delta);
  }
}
