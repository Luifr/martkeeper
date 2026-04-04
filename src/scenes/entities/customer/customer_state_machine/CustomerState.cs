namespace Martkeeper.Entities;

public readonly record struct CustomerState(string value)
{
  // Setup customer
  public static CustomerState BASE = "BASE";
  public static CustomerState HEADING_FOR_PRODUCT = "HEADING_FOR_PRODUCT";
  public static CustomerState WAITING_FOR_PRODUCT = "WAITING_FOR_PRODUCT";
  public static CustomerState HEADING_TO_CHECKOUT = "HEADING_TO_CHECKOUT";
  public static CustomerState WAITING_ON_CHEKCOUT = "WAITING_ON_CHEKCOUT";
  public static CustomerState LEAVING = "LEAVING";

  public static implicit operator string(CustomerState key) => key.value;

  public static implicit operator CustomerState(string value) => new(value);
}
