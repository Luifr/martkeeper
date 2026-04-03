using Godot;

namespace MartKeeper;

public partial class VisualTimer : Node2D
{
  [Export]
  public float TimerCircleRadius = 10f;

  [Export]
  public Color BackgroundColor = new Color(0.5f, 0.5f, 0.5f);

  [Export]
  public Color ForegroundColor = new Color(0.8f, 0.8f, 0.8f);

  [Export]
  public float WaitTime = 10f;

  // Time until the timer shows in game
  [Export]
  public float StartTimeOffset = 2.0f;

  public float TotalTime
  {
    get => WaitTime - StartTimeOffset;
  }

  public float ElapsedTime
  {
    get => TotalTime - (float)_timer.TimeLeft;
  }

  private Timer _timer;

  [Signal]
  public delegate void TimeoutEventHandler();

  public override void _Ready()
  {
    _timer = new Timer()
    {
      WaitTime = WaitTime,
      Autostart = false,
      OneShot = true,
    };

    _timer.Timeout += EmitSignalTimeout;

    AddChild(_timer);
  }

  public override void _Process(double delta)
  {
    GlobalRotation = 0;

    if (!_timer.IsStopped())
    {
      QueueRedraw();
    }
  }

  public override void _Draw()
  {
    if (_timer.IsStopped())
      return;

    if (ElapsedTime <= 0)
      return;

    DrawCircle(Vector2.Zero, TimerCircleRadius, BackgroundColor);

    float percent = ElapsedTime / TotalTime;

    float startAngle = -Mathf.Pi / 2;
    float endAngle = startAngle - (Mathf.Pi * 2f * (1.0f - percent));

    DrawArc(
      Vector2.Zero,
      TimerCircleRadius / 2,
      startAngle,
      endAngle,
      100,
      ForegroundColor,
      TimerCircleRadius
    );
  }

  public void StartTimer()
  {
    _timer.Start();
  }

  public void StopTimer()
  {
    _timer.Stop();
  }
}
