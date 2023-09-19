using Godot;

namespace TheHollowestKnight.scripts.player.components;

public partial class PlayerInputComponent : Component
{
	public Vector2 LastNonNullDirection { get; private set; }
    public Vector2 Direction { get; private set; }
    public Vector2I IntDirection { get; private set; }

    public bool DashJustPressed { get; private set; }
    public bool DashPressed { get; private set; }
    
    public bool JumpJustPressed { get; private set; }
    public bool JumpPressed { get; private set; }
    
    protected override void PhysicsProcess(float delta)
    {
		Direction = Input.GetVector("move_left", "move_right", "move_forward", "move_backward");
        IntDirection = new Vector2I(Mathf.RoundToInt(IntDirection.X), Mathf.RoundToInt(IntDirection.Y));

        if (Direction is not { X: 0, Y: 0 })
	        LastNonNullDirection = Direction;
        
        DashJustPressed = Input.IsActionJustPressed("dash");
        DashPressed = Input.IsActionPressed("dash");
        
		if (JumpJustPressed)
			GD.Print("Input Jump");
		
        JumpJustPressed = Input.IsActionJustPressed("jump");
        JumpPressed = Input.IsActionPressed("jump");
    }
}