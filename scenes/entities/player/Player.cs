using Godot;

namespace MartKeeper.Entities;

public partial class Player : Person
{
  private Vector2 _moveInput;
  private RayCast2D _rayCast;

  public override void _Ready()
  {
    base._Ready();

    _rayCast = GetNode<RayCast2D>("RayCast2D");
  }

  public override void _Process(double delta)
  {
    base._Process(delta);

    _moveInput = Input.GetVector(
      InputAction.LEFT,
      InputAction.RIGHT,
      InputAction.UP,
      InputAction.DOWN
    );
  }

  public override void _IntegrateForces(PhysicsDirectBodyState2D state)
  {
    Vector2 targetVelocity = _moveInput * Speed;

    // Setting velocity will handle movement
    LinearVelocity = LinearVelocity.Lerp(targetVelocity, 0.1f);

    LookAtWalkingDirection(_moveInput);
  }

  private void TryInteract()
  {
    GD.Print("TryInteract: Lets try to interact");
    if (!_rayCast.IsColliding())
    {
      GD.Print("TryInteract: No hit");
      // Nothing to interact with
      return;
    }

    GD.Print("TryInteract: Hit");

    var gameObject = _rayCast.GetCollider();

    if (gameObject is Shelf shelf)
    {
      GD.Print("TryInteract: Hit Shelf");
      TryInteractShelf(shelf);
      return;
    }
    if (gameObject is Stock stock)
    {
      GD.Print("TryInteract: Hit Stock");
      TryInteractStock(stock);
      return;
    }

    GD.Print("TryInteract: Else case, hit:", gameObject.GetType());
  }

  private void TryInteractShelf(Shelf shelf)
  {
    if (LeftHand.CurrentItem?.NameKey == shelf.product.NameKey)
    {
      PutItemFromHandToShelf(shelf, LeftHand);
    }
    if (RightHand.CurrentItem?.NameKey == shelf.product.NameKey)
    {
      PutItemFromHandToShelf(shelf, RightHand);
    }
  }

  private void TryInteractStock(Stock stock)
  {
    // For now only right hand gets the item, to be decided later how to handle it
    if (stock.product == null)
      return;

    RightHand.CurrentItem = stock.product;
  }

  private void PutItemFromHandToShelf(Shelf shelf, Hand hand)
  {
    shelf.ProductCount += 1;
    hand.CurrentItem = null;
  }

  public override void _Input(InputEvent @event)
  {
    if (@event.IsActionPressed(InputAction.INTERACT))
    {
      TryInteract();
    }
  }
}
