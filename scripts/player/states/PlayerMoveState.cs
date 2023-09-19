using Godot;
using TheHollowestKnight.scripts.player.states.super;

namespace TheHollowestKnight.scripts.player.states;

public partial class PlayerMoveState : GroundedState
{
    [Export] private float speed = 10f;

    protected override void OnEnable()
    {
    }

    protected override void OnDisable()
    {
    }

    protected override void PhysicsProcess(float delta)
    {
        base.PhysicsProcess(delta);

        if (Player.Input.Direction is { X: 0, Y: 0 })
        {
            StateMachine.ChangeState<PlayerIdleState>();
            return;
        }

        var velocity = Player.Velocity;

        var direction = (Player.Transform.Basis * new Vector3(Player.Input.Direction.X, 0, Player.Input.Direction.Y)).Normalized();
        
        velocity.X = direction.X * speed;
        velocity.Z = direction.Z * speed;

        Player.Velocity = velocity;
    }

    protected override string GetAnimationName() =>
        "Walk";
}