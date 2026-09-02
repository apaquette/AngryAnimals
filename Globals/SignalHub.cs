using Godot;

public partial class SignalHub : Node
{
    public static SignalHub Instance { get; private set; }
    [Signal] private delegate void OnAnimalDieEventHandler();
    [Signal] private delegate void OnAttemptMadeEventHandler();

    public override void _Ready()
    {
        Instance = this;
    }
    public static void EmitOnAnimalDied() => Instance.EmitSignal(SignalName.OnAnimalDie);
    public static void EmitOnAttemptMade() => Instance.EmitSignal(SignalName.OnAttemptMade);
}