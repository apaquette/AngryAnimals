using Godot;

public partial class NextLevelButton : TextureButton
{
	// Called when the node enters the scene tree for the first time.
	[Export] private Label _label;
	public override void _Ready()
	{
		bool isNextLevel = ScoreManager.LevelSelected < ScoreManager.MaxLevel;
		_label.Text = isNextLevel ? "Next Level" : "Main Menu";
		Pressed += isNextLevel ? NextLevel : MainMenu;
		MouseEntered += OnMouseEntered;
		MouseExited += OnMouseExited;
	}

	private void NextLevel()
	{
		GetTree().ChangeSceneToFile($"res://Scenes/LevelBase/Level{++ScoreManager.LevelSelected}.tscn");
	}

	private void MainMenu()
	{
		GetTree().ChangeSceneToFile("res://Scenes/Main/Main.tscn");
	}

	private void OnMouseEntered()
	{
		Scale = new Vector2(1.1f, 1.1f); // scale the button
	}
	private void OnMouseExited()
	{
		Scale = new Vector2(1, 1); // scale the button back to normal
	}
}
