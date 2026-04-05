using Martkeeper.Resources;

namespace Martkeeper.Entities;

public partial class CustomerStateHeadingForProduct(Customer customer, Product product) : CustomerState
{
  private new Customer _customer = customer;

  private readonly Product _product = product;

  public override void Enter(CustomerState prevState)
  {
    // TODO: set target position
  }

  public override CustomerState Update()
  {
    // If reached target position, try to take an item, if there is a matching item there, move to next state
    return null;
  }
}
