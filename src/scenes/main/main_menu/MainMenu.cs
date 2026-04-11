using System.Diagnostics;
using Godot;
using Martkeeper.UI;

namespace Martkeeper;

public partial class MainMenu : CanvasLayer
{
  [Export]
  public PackedScene GameScene;

  [Export]
  public PackedScene SettingsScene;

  private Button _play;
  private Button _settings;

  private SettingsMenu _settings_menu;
  private QuitContainer _quitContainer;

  public override void _Ready()
  {
    _play = GetNode<Button>("%PlayButton");
    _settings = GetNode<Button>("%SettingsButton");
    _settings_menu = GetNode<SettingsMenu>("%SettingsMenu");
    _quitContainer = GetNode<QuitContainer>("%QuitContainer");

    _play.Pressed += HandlePlayPressed;

    _settings.Pressed += () =>
    {
      _settings_menu.Visible = true;
      UiStack.Instance.Push(() => _settings_menu.Visible = false);
    };
    _settings_menu.Apply += () => UiStack.Instance.Pop();

    UiStack.Instance.OnEmptyCancelPress += _quitContainer.TriggerQuitConfirmation;
  }

  public override void _ExitTree()
  {
    UiStack.Instance.OnEmptyCancelPress -= _quitContainer.TriggerQuitConfirmation;
  }

  private void HandlePlayPressed()
  {
    Debug.Assert(GameScene != null, "Set a game scene in main menu");

    SceneManager.Instance.ChangeSceneToPackagedScene(GameScene, TransitionMode.Fade);
  }
}
