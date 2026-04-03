using Godot;

namespace MartKeeper.Entities;

public partial class Player : Person
{
  private Vector2 _moveInput;

  public override void _Ready()
  {
    base._Ready();
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
    if (!RayCast.IsColliding())
    {
      GD.Print("TryInteract: No hit");
      // Nothing to interact with
      return;
    }

    GD.Print("TryInteract: Hit");

    var gameObject = RayCast.GetCollider();

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
    if (leftHand.CurrentItem?.NameKey == shelf.product.NameKey)
    {
      PutItemFromHandToShelf(shelf, leftHand);
    }
    if (rightHand.CurrentItem?.NameKey == shelf.product.NameKey)
    {
      PutItemFromHandToShelf(shelf, rightHand);
    }
  }

  private void TryInteractStock(Stock stock)
  {
    // For now only right hand gets the item, to be decided later how to handle it
    if (stock.product == null)
      return;

    rightHand.CurrentItem = stock.product;
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
