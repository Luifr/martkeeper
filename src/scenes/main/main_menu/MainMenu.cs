using System.Diagnostics;
using Godot;
using Martkeeper.Constants;
using Martkeeper.Types;

namespace Martkeeper;

public partial class MainMenu : CanvasLayer
{
  [Export]
  public PackedScene GameScene;

  [Export]
  public PackedScene SettingsScene;

  private Button _play;
  private Button _settings;
  private Button _quit;

  private SettingsMenu _settings_menu;

  public override void _Ready()
  {
    _play = GetNode<Button>("%PlayButton");
    _settings = GetNode<Button>("%SettingsButton");
    _settings_menu = GetNode<SettingsMenu>("%SettingsMenu");
    _quit = GetNode<Button>("%QuitButton");

    _play.Pressed += HandlePlayPressed;
    _quit.Pressed += HandleQuit;

    _settings.Pressed += () =>
    {
      _settings_menu.Visible = true;
      UiStack.Instance.Push(() => _settings_menu.Visible = false);
    };
    _settings_menu.Apply += () => UiStack.Instance.Pop();

    UiStack.Instance.OnEmptyCancelPress += HandleQuit;
  }

  public override void _ExitTree()
  {
    UiStack.Instance.OnEmptyCancelPress -= HandleQuit;
  }

  private void HandlePlayPressed()
  {
    Debug.Assert(GameScene != null, "Set a game scene in main menu");

    SceneManager.Instance.ChangeSceneToPackagedScene(GameScene, TransitionMode.Fade);
  }

  private void HandleQuit()
  {
    ConfirmationModal confirmationModal =
      SceneConstants.ConfirmationModal.Instantiate<ConfirmationModal>();

    confirmationModal.SetTitleText(TR.QUIT_CONFIRMATION_MESSAGE);
    confirmationModal.SetConfirmText(TR.QUIT);
    confirmationModal.ConfirmPressed += () => GetTree().Quit();

    AddChild(confirmationModal);
  }
}
