using Godot;

namespace TheHollowestKnight.scripts.player;

public partial class Player : CharacterBody3D
{
	private readonly float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();
	
	[Export] private float speed = 10f;
	[Export] private float jumpVelocity = 20f;
	[Export] private int gravityScale = 10;

	private Node3D pivot;
	//private AnimationPlayer animation;
	
	public override void _Ready()
	{
		pivot = GetNode<Node3D>("Pivot");
		//animation = GetNode<AnimationPlayer>("Pivot/Character/AnimationPlayer");
	}

	public override void _PhysicsProcess(double deltaD)
	{
		var delta = (float) deltaD;
		
		var velocity = Velocity;

		velocity = DoGravity(velocity, delta);
		velocity = DoJump(velocity);
		velocity = DoInput(velocity);

		Velocity = velocity;
		MoveAndSlide();
	}
	
	private Vector3 DoGravity(Vector3 velocity, float delta)
	{
		if (!IsOnFloor())
			velocity.Y -= gravity * gravityScale * delta;
		return velocity;
	}

	private ulong jumpLastPressed;
	private Vector3 DoJump(Vector3 velocity)
	{
		// coyote timer
		if (Input.IsActionJustPressed("jump"))
			jumpLastPressed = Time.GetTicksMsec();

		if (Time.GetTicksMsec() - jumpLastPressed < 100 && IsOnFloor())
		{
			velocity.Y = jumpVelocity;
			jumpLastPressed = 0;
		}

		return velocity;
	}

	private Vector3 DoInput(Vector3 velocity)
	{
		var inputDir = Input.GetVector("move_left", "move_right", "move_forward", "move_backward");
		var direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
		if (direction != Vector3.Zero)
		{
			velocity.X = direction.X * speed;
			velocity.Z = direction.Z * speed;

			pivot.LookAt(Position + direction, Vector3.Up);
			
			//animation.Play("Walk");
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, speed);
			velocity.Z = Mathf.MoveToward(Velocity.Z, 0, speed);
			
			//animation.Play("Idle");
		}

		return velocity;
	}
}