using Godot;

namespace Martkeeper;

public partial class Person : RigidBody2D
{

	// Use to guide the person which acts like a vehicle
	public Vector2 MoveInput;
	[Export] public float Speed = 600f;

	public Hand leftHand;
	public Hand rightHand;
	public RayCast2D RayCast;

	private CollisionShape2D HeadShape;
	private CollisionShape2D LeftShoulderShape;
	private CollisionShape2D RightShoulderShape;

	[Export] public float headRadius = 40;
	[Export] public float shoulderRadiusRatio = 0.35f;
	[Export] public float turnSpeedMultiplier = 0.1f;

	public override void _Ready()
	{
		RayCast = GetNode<RayCast2D>("RayCast2D");
		leftHand = GetNode<Hand>("%LeftHand");
		rightHand = GetNode<Hand>("%RightHand");

		HeadShape = GetNode<CollisionShape2D>("HeadShape");
		LeftShoulderShape = GetNode<CollisionShape2D>("LeftShoulderShape");
		RightShoulderShape = GetNode<CollisionShape2D>("RightShoulderShape");

		if (HeadShape.Shape is CircleShape2D circleHeadShape)
		{
			circleHeadShape.Radius = headRadius;
		}

		if (LeftShoulderShape.Shape is CircleShape2D circleLeftShoulderShape)
		{
			circleLeftShoulderShape.Radius = headRadius * shoulderRadiusRatio;
		}

		if (RightShoulderShape.Shape is CircleShape2D circleRightShoulderShape)
		{
			circleRightShoulderShape.Radius = headRadius * shoulderRadiusRatio;
		}
	}

	public override void _Process(double delta)
	{
	}

	public override void _IntegrateForces(PhysicsDirectBodyState2D state)
	{
		Vector2 targetVelocity = MoveInput * Speed;


		// Setting velocity will handle movement
		LinearVelocity = LinearVelocity.Lerp(targetVelocity, 0.1f);

		LookAtWalkingDirection();
	}

	private void LookAtWalkingDirection()
	{
		if (MoveInput.Length() > 0)
		{
			Rotate(
				GetAngleTo(GlobalPosition + MoveInput) * turnSpeedMultiplier
			);
		}
	}

	public override void _Draw()
	{
		var headColor = Color.Color8(0, 0, 0);
		var shirtColor = Color.Color8(0, 255, 0);

		// Shoulder
		DrawCircle(new Vector2(0, 35), headRadius * shoulderRadiusRatio, shirtColor);
		DrawCircle(new Vector2(0, -35), headRadius * shoulderRadiusRatio, shirtColor);

		// Nose
		DrawCircle(new Vector2(headRadius, 0), headRadius * shoulderRadiusRatio * shoulderRadiusRatio, headColor);

		// Head
		DrawCircle(Vector2.Zero, headRadius, headColor);
	}
}
