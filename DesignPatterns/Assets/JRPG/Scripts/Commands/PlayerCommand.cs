using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCommand
{
    private static PlayerCommand instance = null;

    private static PlayerCommand Instance { 
        get {
            if (instance == null)
            {
                instance = new PlayerCommand();
            }
            return instance;
        } 
    }

    private List<Command> playerCommands = new List<Command>();
    private bool comandIsDone = false;

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
        if(Instance.playerCommands.Count > 0)
        {
            foreach (Command command in Instance.playerCommands)
            {
                command.ExecuteCommand();
            }
            Instance.playerCommands.Clear();
        }
    }

    public static void CommandComplete()
    {
        Instance.comandIsDone = true;
    }

    private static IEnumerator Execute()
    {
        int i = 0;
        foreach(Command command in Instance.playerCommands)
        {
            while (!Instance.comandIsDone)
            {
                command.ExecuteCommand();
                yield return new WaitForSeconds(Time.deltaTime);
            }
            Instance.comandIsDone = false;
            i++;
        }

        Instance.playerCommands.Clear();
    }
}
