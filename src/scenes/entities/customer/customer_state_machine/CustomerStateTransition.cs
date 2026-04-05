namespace Martkeeper.Entities;

public abstract class CustomerStateTransition(CustomerStateName to, CustomerStateName from)
{
  public readonly CustomerStateName To = to;
  public readonly CustomerStateName From = from;
}
