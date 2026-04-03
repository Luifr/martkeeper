using System.Diagnostics;
using Godot;

namespace MartKeeper;

public partial class MainMenu : CanvasLayer
{
  [Export]
  public PackedScene GameScene;

  private SceneManager _sceneManager;
  private Button _play;
  private Button _quit;

  public override void _Ready()
  {
    // TODO: generate type safe group
    _sceneManager = GetTree().GetFirstNodeInGroup("SceneManager") as SceneManager;

    _play = GetNode<Button>("%PlayButton");
    _quit = GetNode<Button>("%QuitButton");

    _play.Pressed += HandlePlayPressed;
    _quit.Pressed += HandleQuitPressed;
  }

  private void HandlePlayPressed()
  {
    Debug.Assert(GameScene != null, "Set a game scene in main menu");

    _sceneManager.ChangeSceneToPackagedScene(GameScene, TransitionMode.Fade);
  }

  private void HandleQuitPressed()
  {
    // TODO: add confirmation modal. Implement this modal as a generic scene
    GetTree().Quit();
  }
}
