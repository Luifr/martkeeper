using Godot;
using Martkeeper.Constants;
using Martkeeper.Types;

namespace Martkeeper.UI;

public partial class QuitContainer : Control
{
  private Button _quit;

  public override void _Ready()
  {
    _quit = GetNode<Button>("%QuitButton");
    _quit.Pressed += TriggerQuitConfirmation;
  }

  public void TriggerQuitConfirmation()
  {
    ConfirmationModal confirmationModal =
      SceneConstants.ConfirmationModal.Instantiate<ConfirmationModal>();

    confirmationModal.SetTitleText(TR.QUIT_CONFIRMATION_MESSAGE);
    confirmationModal.SetConfirmText(TR.QUIT);
    confirmationModal.ConfirmPressed += () => GetTree().Quit();

    UiStack.Instance.AddChild(confirmationModal);
  }
}
