using Godot;
using TheHollowestKnight.scripts.player.states.super;

namespace TheHollowestKnight.scripts.player.states;

public partial class PlayerDashState : PlayerAbilityState
{
    [Export] private GpuParticles3D particles;
    
    [Export] private float dashCooldown = 600f;
    [Export] private ulong dashTime = 150;
    [Export] private float dashSpeed = 30f;

    private Vector3 dashDirection;
    private ulong startTime;

    private MeshInstance3D characterMesh;
    [Export] private Material headBlackMaterial;
    
    public override void _Ready()
    {
        characterMesh = Player.Knight.GetNode<MeshInstance3D>("Armature/Skeleton3D/Head_2/Head_2");
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        
        Player.Gravity.ApplyGravity = false;
        Player.Slowdown.ApplySlowdown = false;
        
        dashDirection = (Player.Transform.Basis * new Vector3(Player.Input.LastNonNullDirection.X, 0, Player.Input.LastNonNullDirection.Y)).Normalized();
        
        startTime = Time.GetTicksMsec();

        particles.Emitting = true;
        
        characterMesh.SetSurfaceOverrideMaterial(0, headBlackMaterial);
    }

    protected override void OnDisable()
    {
        Player.Gravity.ApplyGravity = true;
        Player.Slowdown.ApplySlowdown = true;
        
        particles.Emitting = false;
        
        characterMesh.SetSurfaceOverrideMaterial(0, null);
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