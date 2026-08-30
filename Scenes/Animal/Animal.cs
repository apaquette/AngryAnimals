using Godot;

public partial class Animal : RigidBody2D
{
	[Export] private Label _label;
	[Export] private AudioStreamPlayer2D _stretchSound, _launchSound, _kickSound;

	private bool _isDragging = false;
	private Vector2 _dragStart = Vector2.Zero, _start = Vector2.Zero;

	public override void _Ready()
	{
		InputEvent += OnInputEvent;
		_start = Position;
	}

    public override void _PhysicsProcess(double delta)
	{
		string ds = $"SL:{Sleeping} FR: {Freeze}\n Drag: {_isDragging} Drag Start: {_dragStart} Start: {_start}";
		_label.Text = ds;
	}

	private void StartDragging()
	{
		_isDragging = true;
		_dragStart = GetGlobalMousePosition();
	}

	private void OnInputEvent(Node viewport, InputEvent @event, long shapeIdx)
    {
		if (@event.IsActionPressed("drag"))
		{
			InputEvent -= OnInputEvent;
			StartDragging();
		}
    }
}
