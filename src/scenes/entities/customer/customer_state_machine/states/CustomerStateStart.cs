using System.Diagnostics;
using Godot;
using Martkeeper.Constants;

namespace Martkeeper.Entities;

public class CustomerStateStart(Customer customer) : CustomerState(CustomerStateName.BASE, customer)
{
  public override CustomerStateTransition Update()
  {
    var allProducts = ResourceConstants.AllProductsResource;
    Debug.Assert(allProducts != null, "All products is null");
    GD.Print("all products ", allProducts.Products, " ", allProducts.Products.Count);
    GD.Print(_customer);
    _customer.need = allProducts.Products.PickRandom();
    return new FromBaseToHeadingForProductNeedTransition(_customer.need);
  }
}
