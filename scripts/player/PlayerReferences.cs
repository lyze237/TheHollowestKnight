using Godot;

namespace TheHollowestKnight.scripts.player;

public partial class PlayerReferences : CharacterBody3D
{
    [Export] public PlayerInput Input { get; private set; }
    [Export] public Node3D Knight { get; private set; }

    private AnimationPlayer player;
    public AnimationPlayer AnimationPlayer => player ??= Knight.GetNode<AnimationPlayer>("AnimationPlayer");
}