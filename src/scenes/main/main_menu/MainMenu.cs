using System.Diagnostics;
using Godot;

namespace MartKeeper;

public partial class MainMenu : CanvasLayer
{
  [Export]
  public PackedScene GameScene;

  [Export]
  public PackedScene SettingsScene;

  private SceneManager _sceneManager;
  private Button _play;
  private Button _settings;
  private Button _quit;

  private SettingsMenu _settings_menu;

  public override void _Ready()
  {
    // TODO: generate type safe group
    _sceneManager = GetTree().GetFirstNodeInGroup("SceneManager") as SceneManager;

    _play = GetNode<Button>("%PlayButton");
    _settings = GetNode<Button>("%SettingsButton");
    _settings_menu = GetNode<SettingsMenu>("%SettingsMenu");
    _quit = GetNode<Button>("%QuitButton");

    _play.Pressed += HandlePlayPressed;
    _quit.Pressed += HandleQuitPressed;

    _settings.Pressed += () =>
    {
      _settings_menu.Visible = true;
    };
    _settings_menu.Apply += () =>
    {
      _settings_menu.Visible = false;
    };
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
