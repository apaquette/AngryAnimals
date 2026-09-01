using Godot;
using System;

public partial class LevelBase : Node
{
	[Export] private Marker2D _animalStart;
	[Export] private PackedScene _animal;
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
        var animal = _animal.Instantiate<Animal>();
		animal.Position = _animalStart.Position;
        AddChild(animal);
	}
}
