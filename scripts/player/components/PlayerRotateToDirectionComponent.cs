using Godot;

namespace TheHollowestKnight.scripts.player.components;

public partial class PlayerRotateToDirectionComponent : Component
{
	[Export] private float speed = 10f;

	private Vector3 lookDir = Vector3.Zero;
	private Vector2 lastNonNullDirection = Vector2.Up;

	protected override void PhysicsProcess(float delta)
	{
		var lookAtRotationRad = Player.Pivot.Rotation.Y;

		var targetLookAt = CalculateTargetLookAt();
		var targetLookAtRad = targetLookAt.SignedAngleTo(Vector3.Forward, Vector3.Down);

		var rotatedAngle = Mathf.LerpAngle(lookAtRotationRad, targetLookAtRad, delta * speed);
		var deltaAngle = rotatedAngle - lookAtRotationRad;
		
		Player.Pivot.Rotate(Vector3.Up, deltaAngle);
	}

	private Vector3 CalculateTargetLookAt()
	{
		LazyUpdateLastInputDir();
		
		var dir = lastNonNullDirection;
		return (Player.Transform.Basis * new Vector3(dir.X, 0, dir.Y)).Normalized();
	}

	private void LazyUpdateLastInputDir()
	{
		var dir = Player.Input.Direction;
		
		if (HasDirectionInput(dir)) 
			lastNonNullDirection = dir;
	}

	private static bool HasDirectionInput(Vector2 dir) => 
		dir is not { X: 0, Y: 0 };
}
