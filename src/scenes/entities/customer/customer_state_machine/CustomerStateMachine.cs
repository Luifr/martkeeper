using System.Collections.Generic;
using System.Diagnostics;
using Godot;

namespace Martkeeper.Entities;

public partial class CustomerStateMachine : Node
{
  [Export]
  public Customer TargetCustomer;
  private CustomerState _currentState;

  private Dictionary<CustomerStateName, CustomerState> _statesDictionary;

  public override void _Ready()
  {

    _statesDictionary = new()
    {
      {CustomerStateName.BASE, new CustomerStateBase(TargetCustomer)},
      {CustomerStateName.HEADING_FOR_PRODUCT, new CustomerStateHeadingForProduct(TargetCustomer)},
    };

    Debug.Assert(TargetCustomer != null, "Customer not assigned to CustomerStateMachine");
    _currentState = new CustomerStateBase(TargetCustomer);
    _currentState.Enter(null);
  }

  private void ChangeState(CustomerStateTransition newStateTransition)
  {
    _currentState.Exit();

    _currentState = _statesDictionary[newStateTransition.TargetStateName];

    _currentState.Enter(_currentState, newStateTransition);
  }

  public override void _Process(double delta)
  {
    var newStateTransition = _currentState.Update();
    if (newStateTransition != null)
    {
      ChangeState(newStateTransition);
    }
  }
}
