using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/PlaningState")]
public class PlaningState : BaseState
{
    [field:SerializeField] private BaseState nextState = null;
    public override void Run()
    {
    }
    public override void Initialize(StateMachine newOwner)
    {
        owner = newOwner;
    }

    private void ChangeState()
    {
        owner.SwitchState(nextState);
    }

    public override void Enter()
    {
        CharacterObserver.Execute += ChangeState;
    }
    public override void Exit()
    {
        CharacterObserver.Execute -= ChangeState;
    }
}
