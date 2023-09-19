using Godot;

namespace TheHollowestKnight.scripts.player.components;

public partial class PlayerRotateToDirection : Node
{
	[Export] private PlayerReferences player;
	[Export] private float speed = 10f;

	private Vector3 lookDir = Vector3.Zero;
	private Vector2 lastNonNullDirection = Vector2.Up;
	private RandomNumberGenerator rng = new RandomNumberGenerator();

	public override void _PhysicsProcess(double delta)
	{
		var lookAtRotationRad = player.Pivot.Rotation.Y;

		var targetLookAt = calculateTargetLookAt();
		var targetLookAtRad = targetLookAt.SignedAngleTo(Vector3.Forward, Vector3.Down);

		var rotatedAngle = Mathf.LerpAngle(lookAtRotationRad, targetLookAtRad, delta * speed);
		float deltaAngle = (float)(rotatedAngle - lookAtRotationRad);
		player.Pivot.Rotate(Vector3.Up, deltaAngle);
	}

	public Vector3 calculateTargetLookAt()
	{
		lazyUpdateLastInputDir();
		var dir = lastNonNullDirection;

		return (player.Transform.Basis * new Vector3(dir.X, 0, dir.Y)).Normalized();
	}

	public void lazyUpdateLastInputDir()
	{
		var dir = player.Input.Direction;
		if (hasDirectionInput(dir))
		{
			lastNonNullDirection = dir;
		}
	}

	private bool hasDirectionInput(Vector2 dir)
	{
		if (dir is { X: 0, Y: 0 })
		{
			return false;
		}

		return true;
	}
}
