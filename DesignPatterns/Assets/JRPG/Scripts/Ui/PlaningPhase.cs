using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaningPhase : MonoBehaviour
{
    private static PlaningPhase instance = null;

    [SerializeField] private GameObject execute = null;
    [SerializeField] private GameObject undo = null;
    [SerializeField] private Text actions = null;
    [SerializeField] private Image heroPortrait = null;

    [SerializeField] private List<Text> buttonNames = new List<Text>();
    [SerializeField] private List<GameObject> actionButtons = new List<GameObject>();

    [SerializeField] private List<Slider> healthBars = new List<Slider>();
    [SerializeField] private List<Text> nameFrames = new List<Text>();

    [SerializeField] private List<string> activeHeroes = new List<string>();


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
        SetButtonsActive(true);
        instance.execute.SetActive(false);
        instance.currentCharacter = character;
        UpdateActionNames();
        instance.heroPortrait.sprite = instance.currentCharacter.GetProfile();
    }

    public static void ChangeToExecute()
    {
        instance.execute.SetActive(true);
        SetButtonsActive(false);
        UpdateActionNames();
    }

    public static void SetButtonsActive(bool status)
    {
        foreach (GameObject obj in instance.actionButtons)
        {
            obj.SetActive(status);
        }
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

    public static void AssignHero(string name, float health)
    {
        if (!instance.activeHeroes.Contains(name))
        {
            instance.activeHeroes.Add(name);
            int i = instance.activeHeroes.IndexOf(name);
            instance.nameFrames[i].text = name;
            instance.healthBars[i].maxValue = health;
            instance.healthBars[i].value = health;
        }
    }

    public static void UpdatePlayerHealth(string characterName, float health)
    {
        if (instance.activeHeroes.Contains(characterName))
        {
            int i = instance.activeHeroes.IndexOf(characterName);
            instance.healthBars[i].value = health;
        }
    }

}
