using Godot;
using TheHollowestKnight.scripts.player.states.super;

namespace TheHollowestKnight.scripts.player.states;

public partial class PlayerJumpState : PlayerAbilityState
{
	[Export] private float jumpVelocity = 20f;
    
    protected override void OnEnable()
    {
        IsAbilityDone = true;
    }

    protected override void OnDisable()
    {
    }

    protected override void PhysicsProcess(float delta)
    {
        base.PhysicsProcess(delta);

        var velocity = Player.Velocity;
        velocity.Y = jumpVelocity;
        Player.Velocity = velocity;
    }

    protected override string GetAnimationName() =>
        "Jump";
}