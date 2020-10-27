using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaningPhase : MonoBehaviour
{
    private static PlaningPhase instance = null;

    [SerializeField] private GameObject execute = null;
    [SerializeField] private GameObject undo = null;
    [SerializeField] private Text actions = null;

    [SerializeField] private List<Text> buttonNames = new List<Text>();


    private List<string> usedActions = new List<string>();

    private CharacterController currentCharacter;
    private void Awake()
    {
        if(instance == null){
            instance = this;
        }

        instance.execute.SetActive(false);
    }

    public static void ActionUndo()
    {
        if (instance.usedActions.Count > 0)
        {
            int index = instance.usedActions.Count - 1;
            instance.usedActions.RemoveAt(index);
        }

        UpdateActionNames();
    }

    public static void ChangeToNextCharacter(CharacterController character)
    {
        instance.execute.SetActive(false);
        instance.currentCharacter = character;
        UpdateActionNames();
    }

    public static void ChangeToExecute()
    {
        instance.execute.SetActive(true);
        UpdateActionNames();
    }

    public static void ActionUsed(string name)
    {
        instance.usedActions.Add(name);
    }

    private static void UpdateActions()
    {
        string actionsUsed = string.Empty;

        foreach(string action in instance.usedActions)
        {
            actionsUsed += action + "\n";
        }

        instance.actions.text = actionsUsed;
    }

    private static void UpdateActionNames()
    {
        string[] names = instance.currentCharacter.GetActionNames();
        for (int i = 0; i < names.Length; i++)
        {
            instance.buttonNames[i].text = names[i];
        }
        UpdateActions();
    }

    public static void ClearList()
    {
        instance.usedActions.Clear();
        instance.actions.text = "";
    }
}
