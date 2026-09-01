using Godot;
using System;

public partial class Water : Area2D
{
	[Export] private AudioStreamPlayer2D _splashSound;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
	}

    private void OnBodyEntered(Node2D body)
    {
		_splashSound.GlobalPosition = body.GlobalPosition;
		_splashSound.Play();

		if(body is Animal animal)
		{
			animal.Die();
		}
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}
}
