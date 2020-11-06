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
    private bool EnemyTurn = false;

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
            Instance.EnemyTurn = true;
            Instance.executeCorutine = Instance.StartCoroutine(ExecuteAllCommands());
        }
    }

    private static IEnumerator ExecuteAllCommands()
    {
        while (Instance.executeCommands)
        {
            Execute();
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    private static IEnumerator ExecuteEnemiesCommands()
    {
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

    public static void Complete()
    {
        if (Instance.EnemyTurn)
        {
            Instance.executeCommands = true;
            Instance.executeIndex = 0;
            EnemyObserver.EnemyAttack();
            Instance.StartCoroutine(ExecuteEnemiesCommands());
            Instance.EnemyTurn = false;
        }
        else 
        {
            ExecutionComplete();
            CharacterObserver.ResetTurn();
        }
    }

    private static void Execute()
    {
        if (Instance.executeIndex >= Instance.playerCommands.Count)
        {
            Instance.executeCommands = false;
            Instance.executeIndex = 0;
            Instance.playerCommands.Clear();
            Complete();
        }
        else
        {
            Instance.playerCommands[Instance.executeIndex].ExecuteCommand();
        }
    }
}
