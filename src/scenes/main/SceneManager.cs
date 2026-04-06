using System;
using System.Threading.Tasks;
using Godot;

namespace Martkeeper;

public enum TransitionMode
{
  None,
  Fade,
}

public partial class SceneManager : Node
{
  public static SceneManager Instance;

  private Node _sceneContainer;
  private ColorRect _transitionColorRect;

  public override void _EnterTree()
  {
    if (Instance != null)
    {
      GD.PushError("Duplicate instance of SceneManager");
      QueueFree();
      return;
    }

    Instance = this;
  }

  public override void _Ready()
  {
    _sceneContainer = GetNode<Node>("SceneContainer");
    _transitionColorRect = GetNode<ColorRect>("%TransitionColorRect");
  }

  // TODO: make transition more flexible with config params
  public void ChangeSceneToPackagedScene(
    PackedScene packedScene,
    TransitionMode transitionMode = TransitionMode.None
  )
  {
    Callable
      .From((Action)(async () => await ChangeScene(packedScene, transitionMode)))
      .CallDeferred();
  }

  private async Task ChangeScene(PackedScene packedScene, TransitionMode transitionMode)
  {
    var children = _sceneContainer.GetChildren();
    var scene = packedScene.Instantiate();

    await TransitionIn(transitionMode);

    foreach (var child in children)
    {
      child.QueueFree();
    }

    _sceneContainer.AddChild(scene);

    if (!scene.IsNodeReady())
    {
      await ToSignal(scene, Node.SignalName.Ready);
    }

    await TransitionOut(transitionMode);
  }

  private async Task TransitionIn(TransitionMode transitionMode)
  {
    switch (transitionMode)
    {
      case TransitionMode.None:
        return;
      case TransitionMode.Fade:
        var transitionTween = GetTree().CreateTween();
        transitionTween.TweenProperty(_transitionColorRect, "color", new Color(0, 0, 0, 1), 0.5);
        await ToSignal(transitionTween, Tween.SignalName.Finished);
        break;
      default:
        throw new ArgumentOutOfRangeException(nameof(transitionMode));
    }
  }

  private async Task TransitionOut(TransitionMode transitionMode)
  {
    switch (transitionMode)
    {
      case TransitionMode.None:
        return;
      case TransitionMode.Fade:
        var transitionTween = GetTree().CreateTween();
        transitionTween.TweenProperty(_transitionColorRect, "color", new Color(0, 0, 0, 0), 1);
        await ToSignal(transitionTween, Tween.SignalName.Finished);
        break;
      default:
        throw new ArgumentOutOfRangeException(nameof(transitionMode));
    }
  }
}
