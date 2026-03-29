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
		_person.rightHand.CurrentItem = GD.Load<Item>("data/products/banana.tres");
	}

	public override void _Process(double delta)
	{
		_person.MoveInput = Input.GetVector("left", "right", "up", "down");
	}
}
