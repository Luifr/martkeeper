using Martkeeper.Resources;

namespace Martkeeper.Entities;

public class FromBaseToHeadingForProductNeedTransition(Product need)
  : CustomerStateTransition(CustomerStateName.BASE, CustomerStateName.HEADING_FOR_PRODUCT)
{
  public Product Need = need;
}

public class FromNullToBaseTransition() : CustomerStateTransition(null, CustomerStateName.BASE) { }
