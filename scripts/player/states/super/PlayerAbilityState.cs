namespace TheHollowestKnight.scripts.player.states.super;

public abstract partial class PlayerAbilityState : GravityState
{
    public bool IsAbilityDone { get; protected set; }

    protected override void PhysicsProcess(float delta)
    {
        base.PhysicsProcess(delta);

        if (!IsAbilityDone)
            return;

        if (Player.IsOnFloor())
            StateMachine.ChangeState<PlayerIdleState>();
        else
            StateMachine.ChangeState<PlayerInAirState>();
    }
}