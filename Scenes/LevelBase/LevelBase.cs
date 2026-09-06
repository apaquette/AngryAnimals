using Godot;
using System;

public partial class LevelBase : Node
{
	[Export] private Marker2D _startPosition;
	[Export] private PackedScene _animalScene;
	[Export] private PackedScene _mainScene;
    // Called when the node enters the scene tree for the first time.
    public override void _UnhandledInput(InputEvent @event)
    {
		if (@event.IsActionPressed("ui_cancel"))
		{
			GetTree().ChangeSceneToPacked(_mainScene);
		}
    }
	public override void _Ready()
	{
		SpawnAnimal();
		SignalHub.Instance.Connect(SignalHub.SignalName.OnAnimalDie, Callable.From(SpawnAnimal));
	}

    public override void _EnterTree()
    {
        Cup.NumCups = 0;
    }

	private void SpawnAnimal()
	{
        Animal animal = _animalScene.Instantiate<Animal>();
		animal.GlobalPosition = _startPosition.GlobalPosition;
		CallDeferred(MethodName.AddChild, animal);
	}
}
