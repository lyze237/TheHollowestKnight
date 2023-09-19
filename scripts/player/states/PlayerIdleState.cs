using Godot;
using TheHollowestKnight.scripts.player.states.super;

namespace TheHollowestKnight.scripts.player.states;

public partial class PlayerIdleState : GroundedState
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
        
        if (Player.Input.Direction is not { X: 0, Y: 0 })
            StateMachine.ChangeState<PlayerMoveState>();
    }

    protected override string GetAnimationName() =>
        "Idle";
}