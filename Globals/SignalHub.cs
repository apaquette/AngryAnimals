using Godot;

public partial class SignalHub : Node
{
    public static SignalHub Instance { get; private set; }
    [Signal] private delegate void OnAnimalDieEventHandler();

    public override void _Ready()
    {
        Instance = this;
    }
    public static void EmitOnAnimalDied() => Instance.EmitSignal(SignalName.OnAnimalDie);
}