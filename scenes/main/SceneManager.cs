using Godot;

namespace Martkeeper;

public partial class SceneManager : Node
{
  private Node _sceneContainer;

  public override void _Ready()
  {
    _sceneContainer = GetNode<Node>("SceneContainer");
  }

  public void ChangeSceneToPackagedScene(PackedScene packedScene)
  {
    // TODO: apply transitions
    Callable
      .From(() =>
      {
        var children = _sceneContainer.GetChildren();
        foreach (var child in children)
        {
          child.QueueFree();
        }

        var scene = packedScene.Instantiate();
        _sceneContainer.AddChild(scene);
      })
      .CallDeferred();
  }
}
