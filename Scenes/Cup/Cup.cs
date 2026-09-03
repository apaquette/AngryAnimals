using Godot;

public partial class Cup : StaticBody2D
{
	public static int NumCups = 0;
	[Export] private AnimationPlayer _animationPlayer;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		NumCups++;
		_animationPlayer.AnimationFinished += OnAnimationFinished;
	}

    private void OnAnimationFinished(StringName animName)
    {
        QueueFree();
		SignalHub.EmitOnCupDestroyed(--NumCups);
    }

    public void Die()
	{
		_animationPlayer.Play("vanish");
	}
}
