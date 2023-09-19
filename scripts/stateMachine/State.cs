﻿using Godot;
using TheHollowestKnight.scripts.player;

namespace TheHollowestKnight.scripts.stateMachine;

public abstract partial class State : Node
{
    protected StateMachine StateMachine => GetParent() as StateMachine;
    protected PlayerReferences Player => StateMachine.Player;

    public void Enable()
    {
        Player.AnimationPlayer.Play(GetAnimationName(), 0.2f);
        
        ProcessMode = ProcessModeEnum.Inherit;
        OnEnable();
    }

    public void Disable()
    {
        ProcessMode = ProcessModeEnum.Disabled;
        OnDisable();
    }

    public override void _PhysicsProcess(double delta)
    {
        PhysicsProcess((float)delta);

        Player.MoveAndSlide();
    }

    protected abstract void OnEnable();
    protected abstract void OnDisable();
    protected abstract void PhysicsProcess(float delta);
    protected abstract string GetAnimationName();
}