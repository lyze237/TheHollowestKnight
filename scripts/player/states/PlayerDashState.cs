using Godot;
using TheHollowestKnight.scripts.player.states.super;

namespace TheHollowestKnight.scripts.player.states;

public partial class PlayerDashState : PlayerAbilityState
{
    [Export] private float dashCooldown = 0.5f;
    [Export] private ulong dashTime = 800;
    [Export] private float dashSpeed = 20f;

    public PlayerDashState()
    {
        ApplyGravity = false;
    }

    private ulong startTime;
    
    protected override void OnEnable()
    {
        base.OnEnable();
        
        GD.Print("Start dash");
        startTime = Time.GetTicksMsec();
    }

    protected override void OnDisable()
    {
    }

    protected override void PhysicsProcess(float delta)
    {
        base.PhysicsProcess(delta);

        if (Time.GetTicksMsec() - startTime > dashTime)
        {
            GD.Print("End dash");
            IsAbilityDone = true;
        }
        
        var velocity = Player.Velocity;

        var direction = (Player.Transform.Basis * new Vector3(Player.Input.Direction.X, 0, Player.Input.Direction.Y)).Normalized();
        
        velocity.X = direction.X * dashSpeed;
        velocity.Z = direction.Z * dashSpeed;
        
        Player.Velocity = velocity;
    }

    protected override string GetAnimationName() => 
        "Dash";

    public bool CheckIfCanDash() => 
        Time.GetTicksMsec() - startTime > dashCooldown + dashTime;
}