using Godot;
using System;

public partial class GameUi : Control
{
	[Export] private Label _attemptsLabel, _levellabel;

	private int _attempts = -1;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		OnAttemptMade();
		SignalHub.Instance.Connect(SignalHub.SignalName.OnAttemptMade, Callable.From(OnAttemptMade));
	}

	private void OnAttemptMade()
	{
		_attempts++;
		_attemptsLabel.Text = $"Attempts: {_attempts}";
	}
}
