using System;
using Godot;

public partial class Animal : RigidBody2D
{
	private readonly Vector2 DRAG_LIM_MIN = new(-60,0), DRAG_LIM_MAX = new(0,60);
	private const float IMPULSE_MULT = 20.0f, IMUPLSE_MAX = 2000.0f;
	[Export] private Label _label;
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
		_start = Position;
		_arrowScaleX = _arrowSprite.Scale.X;
		_arrowSprite.Hide();
	}

    

    public override void _PhysicsProcess(double delta)
	{
		HandleDragging();
		Debug();
	}

	private void OnInputEvent(Node viewport, InputEvent @event, long shapeIdx)
    {
		if (@event.IsActionPressed("drag"))
		{
			InputEvent -= OnInputEvent;
			_isDragging = true;
			_dragStart = GetGlobalMousePosition();
			_arrowSprite.Show();
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

	private void HandleDragging()
	{
		if (_isDragging)
		{
			_dragVector = GetGlobalMousePosition() - _dragStart;
			_dragVector = _dragVector.Clamp(DRAG_LIM_MIN, DRAG_LIM_MAX);
			Position = _start + _dragVector;
			ScaleArrow();
		}
	}

	private void Debug()
	{
		string ds = $"SL:{Sleeping} FR: {Freeze}\n";
		ds += $"Drag: {_isDragging} Drag Start: {_dragStart} Start: {_start}\n";
		ds += $"DragVec: {_dragVector}";
		_label.Text = ds;
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
		if(_isDead) return;
		_isDead = true;
		SignalHub.EmitOnAnimalDied();
		QueueFree();
	}

	private void OnSleepingStateChanged()
    {
        if(!Sleeping) return;

		foreach (var body in GetCollidingBodies())
		{		
			if(body is Cup cup)
			{
				cup.Die();
			}
		}
		Die();
    }
	
}
