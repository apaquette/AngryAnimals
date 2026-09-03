using Godot;
using System;

public partial class GameUi : Control
{
	[Export] private Label _attemptsLabel, _levellabel;
	[Export] private VBoxContainer _vbGameOver;
	[Export] private AudioStreamPlayer2D _music;

	private int _attempts = -1;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		OnAttemptMade();
		SignalHub.Instance.Connect(SignalHub.SignalName.OnAttemptMade, Callable.From(OnAttemptMade));
		SignalHub.Instance.Connect(SignalHub.SignalName.OnCupDestroyed, Callable.From<int>(OnCupDestroyed));
	}

    private void OnCupDestroyed(int numRemaining)
    {
        if(numRemaining > 0) return;

		_vbGameOver.Show();
		_music.Play();
    }

    private void OnAttemptMade()
	{
		_attemptsLabel.Text = $"Attempts: {++_attempts}";
	}
}
