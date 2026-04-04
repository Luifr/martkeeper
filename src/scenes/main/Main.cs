using Godot;

namespace Martkeeper;

public partial class Main : Node
{
  private string language = "automatic";

  public override void _Ready()
  {
    SetupLanguage();

    RenderingServer.SetDefaultClearColor(new Color(230 / 255f, 227 / 255f, 197 / 255f));

    CleanUpEditorTools();
  }

  public override void _Process(double delta) { }

  private void SetupLanguage()
  {
    if (language == "automatic")
    {
      string preferedLanguage = OS.GetLocaleLanguage();
      TranslationServer.SetLocale(preferedLanguage);
    }
    else
    {
      TranslationServer.SetLocale(language);
    }
  }

  private void CleanUpEditorTools()
  {
    var toolsNode = GetNode<Node>("%Tools");
    var tools = toolsNode.GetChildren();

    // Run each tool that has an execute method
    foreach (var tool in tools)
    {
      if (tool.HasMethod("Execute") && OS.IsDebugBuild())
      {
        tool.Call("Execute");
      }
    }

    // Remove from from scene as not needed in run time
    toolsNode.QueueFree();
  }
}
