using Godot;

public partial class Cup : StaticBody2D
{
	[Export] private AnimationPlayer _animationPlayer;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_animationPlayer.AnimationFinished += OnAnimationFinished;
	}

    private void OnAnimationFinished(StringName animName)
    {
        QueueFree();
    }

    public void Die()
	{
		_animationPlayer.Play("vanish");
	}
}
