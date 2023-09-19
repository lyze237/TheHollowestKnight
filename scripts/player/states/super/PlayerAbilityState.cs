using TheHollowestKnight.scripts.stateMachine;

namespace TheHollowestKnight.scripts.player.states.super;

public abstract partial class PlayerAbilityState : State
{
    public bool IsAbilityDone { get; protected set; }

    protected override void OnEnable()
    {
        IsAbilityDone = false;
    }

    protected override void PhysicsProcess(float delta)
    {
        if (!IsAbilityDone)
            return;

        if (Player.IsOnFloor())
            StateMachine.ChangeState<PlayerIdleState>();
        else
            StateMachine.ChangeState<PlayerInAirState>();
    }
}