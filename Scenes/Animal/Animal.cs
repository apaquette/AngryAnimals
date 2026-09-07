using System;
using System.Linq;
using Godot;

public partial class Animal : RigidBody2D
{
	private readonly Vector2 DRAG_LIM_MIN = new(-60,0), DRAG_LIM_MAX = new(0,60);
	private const float IMPULSE_MULT = 15.0f, IMUPLSE_MAX = 2000.0f;
	[Export] private Sprite2D _arrowSprite;
	[Export] private AudioStreamPlayer2D _stretchSound, _launchSound, _kickSound;

	private bool _isDragging = false, _isDead = false;
	private float _arrowScaleX = 0.0f;
	private Vector2 _dragStart = Vector2.Zero, _start = Vector2.Zero, _dragVector = Vector2.Zero;

	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event.IsActionReleased("drag") && _isDragging)
		{
			CallDeferred(nameof(HandleRelease));
		}
	}

	public override void _Ready()
	{
		InputEvent += OnInputEvent;
		SleepingStateChanged += OnSleepingStateChanged;
		BodyEntered += OnBodyEntered;
		_start = Position;
		_arrowScaleX = _arrowSprite.Scale.X;
		_arrowSprite.Hide();
	}

    

    public override void _PhysicsProcess(double delta) 
	{
		if (_isDragging)
		{
			_dragVector = GetGlobalMousePosition() - _dragStart;
			_dragVector = _dragVector.Clamp(DRAG_LIM_MIN, DRAG_LIM_MAX);
			Position = _start + _dragVector;
			ScaleArrow();
		}
	}

	private void OnInputEvent(Node viewport, InputEvent @event, long shapeIdx)
    {
		if (@event.IsActionPressed("drag"))
		{
			InputEvent -= OnInputEvent;
			_isDragging = true;
			_dragStart = GetGlobalMousePosition();
			_arrowSprite.Show();
			_stretchSound.Play();
		}
    }

	private void HandleRelease()
	{
		_launchSound.Play();
		_isDragging = false;
		Freeze = false;
		ApplyCentralImpulse(CalculateImpulse());
		_arrowSprite.Hide();
		SignalHub.EmitOnAttemptMade();
	}


	private void ScaleArrow()
	{
		float fraction = CalculateImpulse().Length() / IMUPLSE_MAX;
		fraction = Mathf.Clamp(fraction, 0.0f, 1.0f);
		_arrowSprite.Scale = new Vector2(
			Mathf.Lerp(_arrowScaleX, _arrowScaleX * 2.0f, fraction),
			_arrowSprite.Scale.Y
		);
		_arrowSprite.Rotation = (_start - Position).Angle();
	}

	private Vector2 CalculateImpulse() => _dragVector * -IMPULSE_MULT;

	public void Die()
	{
		if(_isDead) return; // prevent multiple calls to Die
		_isDead = true;
		SignalHub.EmitOnAnimalDied();
		QueueFree();
	}

	private void OnSleepingStateChanged()
    {
        if(!Sleeping) return;	// wait until animal isn't moving anymore

		foreach (var body in GetCollidingBodies().Where(b => b is Cup))
		{		
			(body as Cup).Die(); // invoke Die on cup
		}
		Die(); // invoke Die on animal
    }
	
	private void OnBodyEntered(Node body)
    {
        if(body is Cup && !_kickSound.Playing)
		{
			_kickSound.Play();
		}
    }
}
