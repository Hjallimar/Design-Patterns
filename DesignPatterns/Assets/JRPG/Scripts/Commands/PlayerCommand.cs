using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCommand : MonoBehaviour
{
    private static PlayerCommand instance = null;

    private static PlayerCommand Instance {
        get
        {
            if (instance == null)
            {
                instance = new PlayerCommand();
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public static Action ExecutionComplete = delegate{};
    private List<Command> playerCommands = new List<Command>();
    private bool executeCommands = false;
    private int executeIndex = 0;
    private Coroutine executeCorutine = default;

    public static void AddCommand(Command newCommand)
    {
        Instance.playerCommands.Add(newCommand);
    }

    public static void UndoLastCommand()
    {
        if(Instance.playerCommands.Count > 0)
        {
            Instance.playerCommands.RemoveAt(Instance.playerCommands.Count - 1);
        }
    }

    public static void ExecuteCommands()
    {
        if (!Instance.executeCommands)
        {
            Instance.executeCommands = true;
            Instance.executeIndex = 0;
            Instance.executeCorutine = Instance.StartCoroutine(ExecuteMiStuffs());
        }
    }

    private static IEnumerator ExecuteMiStuffs()
    {
        Debug.Log("we got'em");
        while (Instance.executeCommands)
        {
            Execute();
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    public static void CommandComplete()
    {
        Instance.executeIndex++;
    }

    private static void Execute()
    {
        if (Instance.executeIndex >= Instance.playerCommands.Count)
        {
            Instance.executeCommands = false;
            Instance.executeIndex = 0;
            Instance.playerCommands.Clear();
            ExecutionComplete();
            CharacterObserver.ResetTurn();
        }
        else
        {
            Instance.playerCommands[Instance.executeIndex].ExecuteCommand();
        }
    }
}
