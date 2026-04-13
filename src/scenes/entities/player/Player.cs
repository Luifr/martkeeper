using Godot;
using Martkeeper.Types;

namespace Martkeeper.Entities;

public partial class Player : Person
{
  private Vector2 _moveInput;
  private RayCast2D _rayCast;

  [Signal]
  public delegate void InteractCashRegisterEventHandler(CashRegister cashRegister);

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

    if (gameObject is CashRegister cashRegister)
    {
      GD.Print("TryInteract: Hit Cash Register");
      EmitSignalInteractCashRegister(cashRegister);
      return;
    }
    if (gameObject is Shelf shelf)
    {
      var shapeId = _rayCast.GetColliderShape();
      var ownerId = shelf.ShapeFindOwner(shapeId);
      var shape = shelf.ShapeOwnerGetOwner(ownerId);
      GD.Print("TryInteract: Hit Shelf " + shape.ToString());
      if (shape is ShelfLocation shelfLocation)
      {
        TryInteractShelfLocation(shelfLocation);
      }
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

  private void TryInteractShelfLocation(ShelfLocation shelfLocation)
  {
    if (LeftHand.CurrentItem?.NameKey == shelfLocation.product.NameKey)
    {
      PutItemFromHandToShelf(shelfLocation, LeftHand);
    }
    if (RightHand.CurrentItem?.NameKey == shelfLocation.product.NameKey)
    {
      PutItemFromHandToShelf(shelfLocation, RightHand);
    }
  }

  private void TryInteractStock(Stock stock)
  {
    // For now only right hand gets the item, to be decided later how to handle it
    if (stock.product == null)
      return;

    RightHand.CurrentItem = stock.product;
  }

  private void PutItemFromHandToShelf(ShelfLocation shelfLocation, Hand hand)
  {
    shelfLocation.ProductCount += 1;
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
