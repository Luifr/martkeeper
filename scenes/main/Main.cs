using Godot;

namespace MartKeeper;

public partial class Main : Node
{

	private string language = "automatic";

	public override void _Ready()
	{
		SetupLanguage();

		GD.Print(TR.HELLO_WORLD);
	}

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
