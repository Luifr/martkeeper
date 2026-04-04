using Godot;
using Godot.Collections;

namespace Martkeeper.Resources;

[GlobalClass, Tool]
public partial class AllProducts : Resource
{
  [Export]
  public Array<Product> Products;
}
