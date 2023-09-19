using Godot;
using TheHollowestKnight.scripts.stateMachine;

namespace TheHollowestKnight.scripts.player.states.super;

public abstract partial class GroundedState : GravityState
{
	private readonly float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();
    
	[Export] private int gravityScale = 10;

	protected override void PhysicsProcess(float delta)
	{
		base.PhysicsProcess(delta);
		
		if (Player.Input.JumpJustPressed && Player.IsOnFloor())
			StateMachine.ChangeState<PlayerJumpState>();
		else if (!Player.IsOnFloor())
			StateMachine.ChangeState<PlayerInAirState>();
		else if (Player.Input.DashJustPressed && StateMachine.Get<PlayerDashState>().CheckIfCanDash())
			StateMachine.ChangeState<PlayerDashState>();
	}
}