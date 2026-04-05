namespace Martkeeper.Entities;

public class CustomerStateHeadingForProduct(Customer customer)
  : CustomerState(CustomerStateName.HEADING_FOR_PRODUCT, customer)
{
  public override void Enter(CustomerStateTransition transitionData)
  {
    if (transitionData is FromBaseToHeadingForProductNeedTransition needTransition)
    {
      _customer.EnableThought(needTransition.Need.Texture);
    }
  }

  public override CustomerStateTransition Update()
  {
    // If reached target position, try to take an item, if there is a matching item there, move to next state
    return null;
  }
}
