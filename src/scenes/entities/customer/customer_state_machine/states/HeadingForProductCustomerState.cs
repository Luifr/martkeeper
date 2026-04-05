namespace Martkeeper.Entities;

public partial class CustomerStateHeadingForProduct(Customer customer) : CustomerState
{
  public override CustomerStateName StateName { get => CustomerStateName.BASE; }
  private new Customer _customer = customer;

  public override void Enter(CustomerState prevState, CustomerStateTransition transitionData)
  {
    if (transitionData is HeadingForProductNeedStateTransition needTransition)
    {
    }
  }

  public override CustomerStateTransition Update()
  {
    // If reached target position, try to take an item, if there is a matching item there, move to next state
    return null;
  }
}
