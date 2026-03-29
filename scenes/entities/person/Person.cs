using Godot;

namespace Martkeeper;

public partial class Person : RigidBody2D
{

	// Use to guide the person which acts like a vehicle
	public Vector2 MoveInput;
	[Export] public float Speed = 400f;

	public Hand leftHand;
	public Hand rightHand;

	public override void _Ready()
	{
		leftHand = GetNode<Hand>("%LeftHand");
		rightHand = GetNode<Hand>("%RightHand");
	}

	public override void _Process(double delta)
	{
	}

	public override void _IntegrateForces(PhysicsDirectBodyState2D state)
	{
		Vector2 targetVelocity = MoveInput * Speed;

		LinearVelocity = LinearVelocity.Lerp(targetVelocity, 0.1f);
	}
}
