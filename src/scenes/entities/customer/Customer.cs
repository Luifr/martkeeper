using Godot;
using Martkeeper.Resources;

namespace Martkeeper.Entities;

public partial class Customer : Person
{
  public Vector2 TargetPosition
  {
    get => _navigationAgent2D.TargetPosition;
    set
    {
      _isAtDestination = false;
      _navigationAgent2D.TargetPosition = value;
      _nextPosition = _navigationAgent2D.GetNextPathPosition();
    }
  }

  public bool IsAtDestination
  {
    get => _isAtDestination;
  }

  public Product need;

  private NavigationAgent2D _navigationAgent2D;
  private bool _isAtDestination;
  private Vector2 _nextPosition;

  public override void _Ready()
  {
    base._Ready();

    _navigationAgent2D = GetNode<NavigationAgent2D>("NavigationAgent2D");

    _navigationAgent2D.NavigationFinished += () => _isAtDestination = true;

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

    if (_isAtDestination || _nextPosition.Length() == 0)
    {
      LinearVelocity = LinearVelocity.Lerp(Vector2.Zero, 0.1f);
      return;
    }

    var direction = _nextPosition - GlobalPosition;
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
      return;
    }

    _nextPosition = _navigationAgent2D.GetNextPathPosition();
  }
}
