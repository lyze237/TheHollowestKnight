using Godot;

namespace TheHollowestKnight.scripts.player.components;

public partial class PlayerSlowdownComponent : Component
{
    [Export] private float slowDownSpeed = 10f;

    public bool ApplySlowdown { get; set; } = true;

    protected override void PhysicsProcess(float delta)
    {
        if (Player.Input.Direction is not { X: 0, Y: 0 })
            return;

        if (!ApplySlowdown)
            return;

        var velocity = Player.Velocity;
        velocity.X = Mathf.MoveToward(Player.Velocity.X, 0, slowDownSpeed);
        velocity.Z = Mathf.MoveToward(Player.Velocity.Z, 0, slowDownSpeed);
        Player.Velocity = velocity;
    }
}