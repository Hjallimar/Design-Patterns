using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CommandOverTime : MonoBehaviour
{
    private bool execute = false;
    private float executeTimer = 0f;
    List<Command> commands = new List<Command>();

    public static void ExecuteCommands(List<Command> commands)
    {
        
    }

    private static void Execute()
    {

    }
}
