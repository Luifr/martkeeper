

using Martkeeper.Resources;

namespace Martkeeper.Entities;

public class HeadingForProductNeedStateTransition : CustomerStateTransition
{
  public override CustomerStateName TargetStateName { get => CustomerStateName.HEADING_FOR_PRODUCT; }

  public Product Need;

  public HeadingForProductNeedStateTransition(Product need)
  {
    Need = need;
  }
}