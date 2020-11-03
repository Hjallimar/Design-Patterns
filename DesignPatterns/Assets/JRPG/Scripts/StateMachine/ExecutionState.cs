using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Executionstate")]
public class ExecutionState : BaseState
{
    [field: SerializeField] private BaseState nextState = null;
    public override void Run()
    {
    }

    public override void Initialize(StateMachine newOwner)
    {
        owner = newOwner;
    }
    public override void Enter()
    {
        PlayerCommand.ExecutionComplete += ChangeState;
        Debug.Log("Execution phase entered");
        //PlayerCommand.ExecuteCommands();
    }

    private void ChangeState()
    {
        Debug.Log("Execution Switch state called");
        owner.SwitchState(nextState);
    }

    public override void Exit()
    {
        PlayerCommand.ExecutionComplete -= ChangeState;
        Debug.Log("Exit the Execution state");
    }
}
