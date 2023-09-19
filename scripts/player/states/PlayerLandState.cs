using TheHollowestKnight.scripts.player.states.super;

namespace TheHollowestKnight.scripts.player.states;

public partial class PlayerLandState : GroundedState
{
    protected override void OnEnable()
    {
    }

    protected override void OnDisable()
    {
    }

    protected override void PhysicsProcess(float delta)
    {
        base.PhysicsProcess(delta);

        if (Player.Velocity is { X: 0, Z: 0 })
            StateMachine.ChangeState<PlayerIdleState>();
        else
            StateMachine.ChangeState<PlayerMoveState>();
    }

    protected override string GetAnimationName() => 
        "Jump";
}