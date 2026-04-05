namespace Martkeeper.Entities;

public abstract class CustomerStateTransition(CustomerStateName from, CustomerStateName to)
{
  public readonly CustomerStateName From = from;
  public readonly CustomerStateName To = to;
}
