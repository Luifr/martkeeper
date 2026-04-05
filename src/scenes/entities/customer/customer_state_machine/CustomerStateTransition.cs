namespace Martkeeper.Entities;

public abstract partial class CustomerStateTransition(CustomerStateName to, CustomerStateName from)
{
  public readonly CustomerStateName To = to;
  public readonly CustomerStateName From = from;
}
