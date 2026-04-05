using Godot;

namespace Martkeeper.Entities;

public abstract partial class CustomerState : Node
{
  protected Customer _customer;

  /// <returns>The next state. Return null if should not change states.</returns>
  public abstract CustomerState Update();

  public virtual void Enter(CustomerState prevState) { }

  public virtual void Exit(CustomerState nextState) { }
}
