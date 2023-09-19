using Godot;
using TheHollowestKnight.scripts.stateMachine;

namespace TheHollowestKnight.scripts.player.states.super;

public abstract partial class GravityState : State
{
    private readonly float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();
    [Export] private int gravityScale = 10;

    [Export] private float slowDownSpeed = 10f;

    protected override void PhysicsProcess(float delta)
    {
        DoGravity(delta);
        DoSlowdown(delta);
    }

    private void DoGravity(float delta)
    {
        var velocity = Player.Velocity;

        if (!StateMachine.Player.IsOnFloor())
            velocity.Y -= gravity * gravityScale * delta;

        Player.Velocity = velocity;
    }

    private void DoSlowdown(float delta)
    {
        if (Player.Input.Direction is not { X: 0, Y: 0 })
            return;
        
        var velocity = Player.Velocity;
        velocity.X = Mathf.MoveToward(Player.Velocity.X, 0, slowDownSpeed);
        velocity.Z = Mathf.MoveToward(Player.Velocity.Z, 0, slowDownSpeed);
        Player.Velocity = velocity;
    }
}