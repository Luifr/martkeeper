using System.Diagnostics;
using Godot;
using Martkeeper.Resources;

namespace Martkeeper.Entities;

public class CustomerStateStart(Customer customer)
  : CustomerState(CustomerStateName.BASE, customer)
{
  public override CustomerStateTransition Update()
  {
    // TODO: store this path in a shared constant
    var allProducts = GD.Load<AllProducts>("res://data/all_products.tres");
    Debug.Assert(allProducts != null, "All products is null");
    GD.Print("all products ", allProducts.Products, " ", allProducts.Products.Count);
    GD.Print(_customer);
    _customer.need = allProducts.Products.PickRandom();
    return new FromBaseToHeadingForProductNeedTransition(_customer.need);
  }
}
