using Godot;
using TheHollowestKnight.scripts.player.states.super;

namespace TheHollowestKnight.scripts.player.states;

public partial class PlayerInAirState : GravityState
{
    [Export] private float speed = 10f;
    [Export] private Node3D pivot;
    
    protected override void OnEnable()
    {
    }

    protected override void OnDisable()
    {
    }

    protected override void PhysicsProcess(float delta)
    {
        base.PhysicsProcess(delta);
        
        if (Player.IsOnFloor())
            StateMachine.ChangeState<PlayerLandState>();
        
        var velocity = Player.Velocity;

        var direction = (Player.Transform.Basis * new Vector3(Player.Input.Direction.X, 0, Player.Input.Direction.Y)).Normalized();
        velocity.X = direction.X * speed;
        velocity.Z = direction.Z * speed;

        if (Player.Input.Direction is not {X: 0, Y: 0})
        {
            pivot.LookAt(Player.Position + direction, Vector3.Up);
        }

        Player.Velocity = velocity;
    }

    protected override string GetAnimationName() =>
        "Jump";
}