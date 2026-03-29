using Godot;

namespace MartKeeper;

public partial class Main : Node
{

	private string language = "automatic";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SetupLanguage();

		GD.Print(TR.HELLO_WORLD);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

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
}
