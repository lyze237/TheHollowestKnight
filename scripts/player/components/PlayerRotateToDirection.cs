using Godot;

namespace TheHollowestKnight.scripts.player.components;

public partial class PlayerRotateToDirection : Node
{
    [Export] private PlayerReferences player;
    [Export] private float speed = 10f;

    private Vector3 lookDir = Vector3.Zero;
    private Vector2 lastNonNullDirection = Vector2.Up;
    
    public override void _PhysicsProcess(double delta)
    {
        var dir = player.Input.Direction;
        if (dir is { X: 0, Y: 0 })
            dir = lastNonNullDirection;
        else
            lastNonNullDirection = dir;
        
        var direction = (player.Transform.Basis * new Vector3(dir.X, 0, dir.Y)).Normalized();
        
        lookDir = lookDir.MoveToward(direction, (float) delta * speed);
        player.Pivot.LookAt(player.Position + lookDir, Vector3.Up);
    }
}