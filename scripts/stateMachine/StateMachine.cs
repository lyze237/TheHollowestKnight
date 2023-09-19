using System;
using System.Linq;
using Godot;
using TheHollowestKnight.scripts.player;

namespace TheHollowestKnight.scripts.stateMachine;

public partial class StateMachine : Node
{
    [Export] private CharacterBody3D player;
    public PlayerReferences Player => player as PlayerReferences;

    [Export] private State initialState;
    public State CurrentState { get; private set; }

    public override void _Ready()
    {
        foreach (var child in GetChildren()) 
            child.ProcessMode = ProcessModeEnum.Disabled;

        CurrentState = initialState ?? throw new ArgumentException("Initial State Machine State not defined", nameof(initialState));
        CurrentState.Enable();
    }
    
    public void ChangeState<T>() where T : State 
    {
        GD.Print($"Changing state from {CurrentState.GetType()} to {Get<T>().GetType()}");
        
        CurrentState.Disable();
        CurrentState = Get<T>();
        CurrentState.Enable();
    }

    private T Get<T>() where T : State =>
        GetChildren().First(c => c.GetType() == typeof(T)) as T;
}