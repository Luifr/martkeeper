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
    Debug.Assert(TargetCustomer != null, "Customer not assigned to CustomerStateMachine");

    _statesDictionary = new()
    {
      { CustomerStateName.BASE, new CustomerStateStart(TargetCustomer) },
      { CustomerStateName.HEADING_FOR_PRODUCT, new CustomerStateHeadingForProduct(TargetCustomer) },
    };

    _currentState = _statesDictionary[CustomerStateName.BASE];
    _currentState.Enter(new FromNullToBaseTransition());
  }

  private void ChangeState(CustomerStateTransition newStateTransition)
  {
    _currentState.Exit();
    _currentState = _statesDictionary[newStateTransition.To];
    _currentState.Enter(newStateTransition);
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
