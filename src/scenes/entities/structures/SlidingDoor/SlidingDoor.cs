using Godot;

namespace Martkeeper.Entities;

public partial class SlidingDoor : Node2D
{
  string POSITION_PROPERTY = Node2D.PropertyName.Position.ToString();
  const float ANIMATION_DURATION = 1; // in seconds

  public override void _Ready()
  {
    var leftDoor = GetNode<StaticBody2D>("%LeftDoor");
    var rightDoor = GetNode<StaticBody2D>("%RightDoor");
    var openArea = GetNode<Area2D>("%OpenArea");

    var (openLeftDoor, closeLeftDoor) = MakeDoorFunctions(leftDoor);
    var (openRightDoor, closeRightDoor) = MakeDoorFunctions(rightDoor);

    openArea.BodyEntered += _ =>
    {
      openLeftDoor();
      openRightDoor();
    };
    openArea.BodyExited += _ =>
    {
      if (!openArea.HasOverlappingBodies())
      {
        closeLeftDoor();
        closeRightDoor();
      }
    };
  }

  private (System.Action, System.Action) MakeDoorFunctions(Node2D body)
  {
    var tweenTarget = body.GetNode<CollisionShape2D>("CollisionShape2D").Position * 2;
    Tween tween = null;

    var totalTravelDistance = tweenTarget.Length();

    // The duration is a value between 0 and ANIMATION_DURATION so the anmation speed is always the same.
    float getDuration(Vector2 target) =>
      ANIMATION_DURATION * ((body.Position - target).Length() / totalTravelDistance);

    return (
      () =>
      {
        // Open function
        tween?.Kill();
        tween = CreateTween();
        tween.TweenProperty(body, POSITION_PROPERTY, tweenTarget, getDuration(tweenTarget));
      },
      () =>
      {
        // Close function
        tween?.Kill();
        tween = CreateTween();
        tween.TweenProperty(body, POSITION_PROPERTY, Vector2.Zero, getDuration(Vector2.Zero));
      }
    );
  }
}
