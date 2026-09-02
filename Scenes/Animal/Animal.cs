using Godot;

public partial class Animal : RigidBody2D
{
	private readonly Vector2 DRAG_LIM_MIN = new(-60,0), DRAG_LIM_MAX = new(0,60);
	private const float IMPULSE_MULT = 20.0f;
	[Export] private Label _label;
	[Export] private AudioStreamPlayer2D _stretchSound, _launchSound, _kickSound;

	private bool _isDragging = false, _isDead = false;
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
		_start = Position;
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
		}
    }

	private void HandleRelease()
	{
		_launchSound.Play();
		_isDragging = false;
		Freeze = false;
		ApplyCentralImpulse(CalculateImpulse());
		SignalHub.EmitOnAttemptMade();
	}

	private void HandleDragging()
	{
		if (_isDragging)
		{
			_dragVector = GetGlobalMousePosition() - _dragStart;
			_dragVector = _dragVector.Clamp(DRAG_LIM_MIN, DRAG_LIM_MAX);
			Position = _start + _dragVector;
		}
	}

	private void Debug()
	{
		string ds = $"SL:{Sleeping} FR: {Freeze}\n";
		ds += $"Drag: {_isDragging} Drag Start: {_dragStart} Start: {_start}\n";
		ds += $"DragVec: {_dragVector}";
		_label.Text = ds;
	}

	private Vector2 CalculateImpulse() => _dragVector * -IMPULSE_MULT;

	public void Die()
	{
		if(_isDead) return;
		_isDead = true;
		SignalHub.EmitOnAnimalDied();
		QueueFree();
	}
	
}
