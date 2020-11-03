using UnityEngine;

public abstract class BaseState : ScriptableObject
{
    protected StateMachine owner = null;
    public abstract void Initialize(StateMachine owner);
    public abstract void Run();
    public abstract void Enter();
    public abstract void Exit();
}
