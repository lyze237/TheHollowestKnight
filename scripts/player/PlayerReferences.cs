using Godot;
using TheHollowestKnight.scripts.player.components;

namespace TheHollowestKnight.scripts.player;

public partial class PlayerReferences : CharacterBody3D
{
    [Export] public components.PlayerInputComponent Input { get; private set; }
    [Export] public Node3D Pivot { get; private set; }
    [Export] public Node3D Knight { get; private set; }
    [Export] public PlayerGravityComponent Gravity { get; private set; }
    [Export] public PlayerSlowdownComponent Slowdown { get; private set; }

    private AnimationPlayer player;
    public AnimationPlayer AnimationPlayer => player ??= Knight.GetNode<AnimationPlayer>("AnimationPlayer");
}