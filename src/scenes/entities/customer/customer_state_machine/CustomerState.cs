namespace Martkeeper.Entities;

public readonly record struct CustomerStateName(string value)
{
  // Setup customer
  public static readonly CustomerStateName BASE = "BASE";
  public static readonly CustomerStateName HEADING_FOR_PRODUCT = "HEADING_FOR_PRODUCT";
  public static readonly CustomerStateName WAITING_FOR_PRODUCT = "WAITING_FOR_PRODUCT";
  public static readonly CustomerStateName HEADING_TO_CHECKOUT = "HEADING_TO_CHECKOUT";
  public static readonly CustomerStateName WAITING_ON_CHEKCOUT = "WAITING_ON_CHEKCOUT";
  public static readonly CustomerStateName LEAVING = "LEAVING";

  public static implicit operator string(CustomerStateName key) => key.value;

  public static implicit operator CustomerStateName(string value) => new(value);
}

public abstract partial class CustomerState(CustomerStateName name, Customer customer)
{
  public readonly CustomerStateName StateName = name;
  protected readonly Customer _customer = customer;

  /// <returns>The next state. Return null if should not change states.</returns>
  public abstract CustomerStateTransition Update();

  public virtual void Enter(CustomerStateTransition stateTransition) { }

  public virtual void Exit() { }
}
