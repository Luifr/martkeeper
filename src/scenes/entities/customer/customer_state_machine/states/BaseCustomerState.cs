using System;
using Godot;
using Martkeeper.Resources;

namespace Martkeeper.Entities;

public partial class BaseCustomerState : Node, ICustomerState
{
  public CustomerState State { get; } = CustomerState.BASE;
  private Customer _customer;
  private Action<CustomerState> _changeState;

  public void Init(Action<CustomerState> ChangeState, Customer customer)
  {
    _changeState = ChangeState;
    _customer = customer;
    GD.Print("Init has run ", _customer, " ", customer);
  }

  public void Enter()
  {
    GD.Print("Customer base state");
    var allProducts = GD.Load<AllProducts>("res://data/all_products.tres");
    if (allProducts != null)
    {
      GD.Print("all products ", allProducts.Products, " ", allProducts.Products.Count);
      GD.Print(_customer);
      _customer.need = allProducts.Products.PickRandom();
    }
    else
    {
      GD.Print("all products is null");
    }
    _changeState(CustomerState.HEADING_FOR_PRODUCT);
  }

  public void Update(double delta) { }

  public void Exit() { }
}
