using Godot;
using System;

public partial class LevelBase : Node
{
	[Export] private Marker2D _startPosition;
	[Export] private PackedScene _animalScene;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SpawnAnimal();
		SignalHub.Instance.Connect(SignalHub.SignalName.OnAnimalDie, Callable.From(SpawnAnimal));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void SpawnAnimal()
	{
        Animal animal = _animalScene.Instantiate<Animal>();
		animal.GlobalPosition = _startPosition.GlobalPosition;
		CallDeferred(MethodName.AddChild, animal);
	}
}
