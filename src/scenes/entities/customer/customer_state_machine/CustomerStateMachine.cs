using System.Diagnostics;
using Godot;

namespace Martkeeper.Entities;

public partial class CustomerStateMachine : Node
{
  [Export]
  public Customer TargetCustomer;
  private CustomerState _currentState;

  public override void _Ready()
  {
    Debug.Assert(TargetCustomer != null, "Customer not assigned to CustomerStateMachine");
    _currentState = new CustomerStateBase(TargetCustomer);
    _currentState.Enter(null);
  }

  private void ChangeState(CustomerState newState)
  {
    _currentState.Exit(newState);
    newState.Enter(_currentState);
    _currentState = newState;
  }

  public override void _Process(double delta)
  {
    var nextState = _currentState.Update();
    if (nextState != null)
    {
      ChangeState(nextState);
    }
  }
}
