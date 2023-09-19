using Godot;

namespace TheHollowestKnight.scripts.player;

public partial class PlayerInput : Node
{
    public Vector2 Direction { get; private set; }
    public Vector2I IntDirection { get; private set; }

    public bool DashJustPressed { get; private set; }
    public bool DashPressed { get; private set; }
    
    public bool JumpJustPressed { get; private set; }
    public bool JumpPressed { get; private set; }
    
    public override void _Process(double delta)
    {
		Direction = Input.GetVector("move_left", "move_right", "move_forward", "move_backward");
        IntDirection = new Vector2I(Mathf.RoundToInt(IntDirection.X), Mathf.RoundToInt(IntDirection.Y));

        DashJustPressed = Input.IsActionJustPressed("dash");
        DashJustPressed = Input.IsActionPressed("dash");
        
        JumpJustPressed = Input.IsActionJustPressed("jump");
        JumpJustPressed = Input.IsActionPressed("jump");
    }
}