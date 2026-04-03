using Godot;

namespace MartKeeper.Entities;

public partial class Customer : Person
{
	private Vector2 _moveTo;
	private NavigationAgent2D _navigationAgent2D;

	public override void _Ready()
	{
		base._Ready();

		_navigationAgent2D = GetNode<NavigationAgent2D>("NavigationAgent2D");

		// TODO: targetPostion will later be set depending on customer state
		_navigationAgent2D.TargetPosition = new Vector2(150, 150);

		var timer = new Timer
		{
			Autostart = true,
			OneShot = false,
			WaitTime = 0.5f,
		};

		timer.Timeout += UpdateMovingDirection;

		AddChild(timer);
	}

	public override void _IntegrateForces(PhysicsDirectBodyState2D state)
	{
		Vector2 targetVelocity = Vector2.Zero;

		var direction = _moveTo - GlobalPosition;
		if (direction.Length() > 2f)
		{
			targetVelocity = direction.Normalized() * Speed;
		}

		// Setting velocity will handle movement
		LinearVelocity = LinearVelocity.Lerp(targetVelocity, 0.1f);

		LookAtWalkingDirection(direction.Normalized());
	}

	private void UpdateMovingDirection()
	{
		var distanceToGoal = GlobalPosition - _navigationAgent2D.TargetPosition;

		if (distanceToGoal.Length() < 2f)
		{
			_moveTo = Vector2.Zero;
			return;
		}

		_moveTo = _navigationAgent2D.GetNextPathPosition();
	}
}
