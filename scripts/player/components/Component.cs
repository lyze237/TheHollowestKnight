using Godot;

namespace TheHollowestKnight.scripts.player.components;

public abstract partial class Component : Node
{
    [Export] protected PlayerReferences Player;

    public override void _PhysicsProcess(double delta)
    {
        PhysicsProcess((float)delta);
    }

    protected abstract void PhysicsProcess(float delta);
}