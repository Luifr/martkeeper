using Godot;
using Martkeeper;
using MartKeeper.Core;


namespace MartKeeper;

public partial class Player : Node2D
{
	private Person _person;

	public override void _Ready()
	{
		_person = GetNode<Person>("Person");

		_person.CollisionLayer = 2;
		_person.CollisionMask = 7;
	}

	public override void _Process(double delta)
	{
		_person.MoveInput = Input.GetVector(InputAction.LEFT, InputAction.RIGHT, InputAction.UP, InputAction.DOWN);
	}

	private void TryInteract()
	{
		GD.Print("TryInteract: Lets try to interact");
		if (!_person.RayCast.IsColliding())
		{
			GD.Print("TryInteract: No hit");
			// Nothing to interact with
			return;
		}

		GD.Print("TryInteract: Hit");

		var gameObject = _person.RayCast.GetCollider();

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
		if (_person.leftHand.CurrentItem?.NameKey == shelf.product.NameKey)
		{
			PutItemFromHandToShelf(shelf, _person.leftHand);
		}
		if (_person.rightHand.CurrentItem?.NameKey == shelf.product.NameKey)
		{
			PutItemFromHandToShelf(shelf, _person.rightHand);
		}
	}

	private void TryInteractStock(Stock stock)
	{
		// For now only right hand gets the item, to be decided later how to handle it
		if (stock.product == null) return;

		_person.rightHand.CurrentItem = stock.product;
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
