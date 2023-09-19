using Godot;

namespace TheHollowestKnight.scripts.player.components;

public partial class PlayerGravityComponent : Component
{
    private readonly float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();
    [Export] private int gravityScale = 10;

    public bool ApplyGravity { get; set; } = true;

    protected override void PhysicsProcess(float delta)
    {
        var velocity = Player.Velocity;

        if (!ApplyGravity)
            velocity.Y = 0;
        else if (!Player.IsOnFloor())
            velocity.Y -= gravity * gravityScale * delta;

        Player.Velocity = velocity;
    }
}