using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [SerializeField] private List<BaseState> states = new List<BaseState>();

    private BaseState currentState = null;
    // Start is called before the first frame update
    void Awake()
    {
        foreach(BaseState state in states)
        {
            state.Initialize(this);
        }

        if(states.Count > 0)
        {
            currentState = states[0];
            currentState.Enter();
        }
    }

    public void Update()
    {
        //currentState.Run();
    }

    public void SwitchState(BaseState newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }
}
