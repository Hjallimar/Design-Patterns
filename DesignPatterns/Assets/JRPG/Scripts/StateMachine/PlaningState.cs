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
        Debug.Log("Planing Switch state called");
        owner.SwitchState(nextState);
    }

    public override void Enter()
    {
        CharacterObserver.Execute += ChangeState;
        Debug.Log("Planing phase entered");
    }
    public override void Exit()
    {
        CharacterObserver.Execute -= ChangeState;
        Debug.Log("Exit the planing state");
    }
}
