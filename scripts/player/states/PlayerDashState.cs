using Godot;
using TheHollowestKnight.scripts.player.states.super;

namespace TheHollowestKnight.scripts.player.states;

public partial class PlayerDashState : PlayerAbilityState
{
    [Export] private float dashCooldown = 600f;
    [Export] private ulong dashTime = 150;
    [Export] private float dashSpeed = 20f;

    private Vector3 dashDirection;
    private ulong startTime;

    protected override void OnEnable()
    {
        base.OnEnable();
        
        Player.Gravity.ApplyGravity = false;
        Player.Slowdown.ApplySlowdown = false;
        
        dashDirection = (Player.Transform.Basis * new Vector3(Player.Input.Direction.X, 0, Player.Input.Direction.Y)).Normalized();
        
        startTime = Time.GetTicksMsec();
    }

    protected override void OnDisable()
    {
        Player.Gravity.ApplyGravity = true;
        Player.Slowdown.ApplySlowdown = true;
    }

    protected override void PhysicsProcess(float delta)
    {
        base.PhysicsProcess(delta);

        if (Time.GetTicksMsec() - startTime > dashTime)
        {
            IsAbilityDone = true;
        }
        
        var velocity = Player.Velocity;

        velocity.X = dashDirection.X * dashSpeed;
        velocity.Z = dashDirection.Z * dashSpeed;
        
        Player.Velocity = velocity;
    }

    protected override string GetAnimationName() => 
        "Dash";

    public bool CheckIfCanDash() => 
        Time.GetTicksMsec() - startTime > dashCooldown + dashTime;
}