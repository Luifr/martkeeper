using Godot;

namespace Martkeeper;

public partial class ConfirmationModal : Control
{
  [Signal]
  public delegate void ConfirmPressedEventHandler();

  [Signal]
  public delegate void CancelPressedEventHandler();

  public bool PauseGame = false;

  private Label _titleLabel;
  private Button _confirmButton;
  private Button _cancelButton;

  private string _titleLabelText;
  private string _confirmButtonText;
  private string _cancelButtonText;

  public override void _Ready()
  {
    _titleLabel = GetNode<Label>("%Title");
    _confirmButton = GetNode<Button>("%ConfirmButton");
    _cancelButton = GetNode<Button>("%CancelButton");

    _confirmButton.Pressed += ConfirmAction;
    _cancelButton.Pressed += CancelAction;

    GuiInput += OnGuiInput;

    UiStack.Instance.Push(CloseConfirmationModal);

    if (PauseGame)
      GetTree().Paused = PauseGame;

    if (!string.IsNullOrEmpty(_titleLabelText))
      _titleLabel.Text = _titleLabelText;
    if (!string.IsNullOrEmpty(_confirmButtonText))
      _confirmButton.Text = _confirmButtonText;
    if (!string.IsNullOrEmpty(_cancelButtonText))
      _cancelButton.Text = _cancelButtonText;
  }

  public void SetTitleText(string titleText)
  {
    _titleLabelText = titleText;
  }

  public void SetConfirmText(string confirmText)
  {
    _confirmButtonText = confirmText;
  }

  public void SetCancelText(string cancelText)
  {
    _cancelButtonText = cancelText;
  }

  private void CloseConfirmationModal()
  {
    SceneTree sceneTree = GetTree();
    if (PauseGame && sceneTree.Paused)
    {
      sceneTree.Paused = false;
    }
    QueueFree();
  }

  private void OnGuiInput(InputEvent inputEvent)
  {
    if (inputEvent is InputEventMouseButton mouseEvent)
    {
      if (mouseEvent.ButtonIndex == MouseButton.Left)
      {
        CancelAction();
      }
    }
  }

  private void ConfirmAction()
  {
    EmitSignalConfirmPressed();
    UiStack.Instance.Pop();
  }

  private void CancelAction()
  {
    EmitSignalCancelPressed();
    UiStack.Instance.Pop();
  }
}
