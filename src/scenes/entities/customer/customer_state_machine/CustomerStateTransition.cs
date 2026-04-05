

namespace Martkeeper.Entities;

public abstract partial class CustomerStateTransition
{
  public abstract CustomerStateName TargetStateName { get; }
}