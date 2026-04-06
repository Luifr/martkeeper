using Godot;
using Martkeeper.Constants;
using Martkeeper.Types;

namespace Martkeeper.UI;

public partial class PauseMenu : Panel
{
  public static PauseMenu Instance;

  private Button _resumeButton;
  private Button _quitToMainMenuButton;

  public override void _EnterTree()
  {
    if (Instance != null)
    {
      GD.PushError("Duplicate instance of PauseMenu");
      QueueFree();
      return;
    }

    Instance = this;
  }

  public override void _Ready()
  {
    Hide();

    _resumeButton = GetNode<Button>("%ResumeButton");
    _quitToMainMenuButton = GetNode<Button>("%QuitToMainMenuButton");

    _resumeButton.Pressed += () => UiStack.Instance.Pop();
    _quitToMainMenuButton.Pressed += HandleClickQuitToMainMenu;
  }

  public void PauseGame()
  {
    UiStack.Instance.Push(ResumeGame);
    Show();
    GetTree().Paused = true;
  }

  private void ResumeGame()
  {
    Hide();
    GetTree().Paused = false;
  }

  private void HandleClickQuitToMainMenu()
  {
    ConfirmationModal confirmationModal =
        SceneConstants.ConfirmationModal.Instantiate<ConfirmationModal>();

    confirmationModal.SetTitleText(TR.QUIT_TO_MENU_CONFIRMATION_MESSAGE);

    confirmationModal.ConfirmPressed += () =>
    {
      UiStack.Instance.Pop();
      SceneManager.Instance.ChangeSceneToPackagedScene(
        GD.Load<PackedScene>("uid://ua5q1io8pn3c"),
        TransitionMode.Fade
      );
    };

    UiStack.Instance.AddChild(confirmationModal);
  }
}
