using Godot;

namespace MartKeeper.Entities;

public partial class Person : RigidBody2D
{
  [Export]
  public float Speed = 600f;

  protected Hand LeftHand;
  protected Hand RightHand;

  private CollisionShape2D HeadShape;
  private CollisionShape2D LeftShoulderShape;
  private CollisionShape2D RightShoulderShape;

  [Export]
  public float HeadRadius = 40;

  [Export]
  public float ShoulderRadiusRatio = 0.35f;

  [Export]
  public float TurnSpeedMultiplier = 0.1f;

  [Export]
  public Color HeadColor = Color.Color8(0, 0, 0);

  [Export]
  public Color ShirtColor = Color.Color8(0, 255, 0);

  public override void _Ready()
  {
    LeftHand = GetNode<Hand>("%LeftHand");
    RightHand = GetNode<Hand>("%RightHand");

    HeadShape = GetNode<CollisionShape2D>("HeadShape");
    LeftShoulderShape = GetNode<CollisionShape2D>("LeftShoulderShape");
    RightShoulderShape = GetNode<CollisionShape2D>("RightShoulderShape");

    if (HeadShape.Shape is CircleShape2D circleHeadShape)
    {
      circleHeadShape.Radius = HeadRadius;
    }

    if (LeftShoulderShape.Shape is CircleShape2D circleLeftShoulderShape)
    {
      circleLeftShoulderShape.Radius = HeadRadius * ShoulderRadiusRatio;
    }

    if (RightShoulderShape.Shape is CircleShape2D circleRightShoulderShape)
    {
      circleRightShoulderShape.Radius = HeadRadius * ShoulderRadiusRatio;
    }
  }

  protected void LookAtWalkingDirection(Vector2 direction)
  {
    Rotate(GetAngleTo(GlobalPosition + direction) * TurnSpeedMultiplier);
  }

  public override void _Draw()
  {
    // Shoulder
    DrawCircle(new Vector2(0, 35), HeadRadius * ShoulderRadiusRatio, ShirtColor);
    DrawCircle(new Vector2(0, -35), HeadRadius * ShoulderRadiusRatio, ShirtColor);

    // Nose
    DrawCircle(
      new Vector2(HeadRadius, 0),
      HeadRadius * ShoulderRadiusRatio * ShoulderRadiusRatio,
      HeadColor
    );

    // Head
    DrawCircle(Vector2.Zero, HeadRadius, HeadColor);
  }
}
