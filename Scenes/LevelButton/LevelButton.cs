
using Godot;

public partial class LevelButton : TextureButton
{
	[Export] private int _levelNumber = 1;
	[Export] private Label _levelLabel;
	[Export] private Label _scoreLabel;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		MouseEntered += OnMouseEntered;
		MouseExited += OnMouseExited;
		Pressed += OnPressed;
		_levelLabel.Text = $"{_levelNumber}";
		_scoreLabel.Text = ScoreManager.GetBestScoreForLevel(_levelNumber).ToString("D4");
	}

    private void OnPressed()
    {
		ScoreManager.LevelSelected = _levelNumber;
        GetTree().ChangeSceneToFile($"res://Scenes/LevelBase/Level{_levelNumber}.tscn");
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
