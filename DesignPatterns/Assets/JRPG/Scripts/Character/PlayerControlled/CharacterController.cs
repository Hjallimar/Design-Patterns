using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private float speed = 1f;
    [SerializeField] private GameObject target = null;
    [SerializeField] private CharacterAction[] actions = new CharacterAction[3];
    CharacterAction usedAction = null;

    public void UseAction(int index)
    {
        index = index % (actions.Length - 1);
        usedAction = actions[index];
        usedAction.Performer = transform;
        usedAction.Target = target.transform;
        usedAction.ActivateAction();
        PlaningPhase.ActionUsed(usedAction.ActionName);
    }

    public void UndoAction()
    {
        if(usedAction != null)
        {
            PlaningPhase.ActionUndo();
            usedAction.UndoAction();
            usedAction = null;
        }
    }

    public string[] GetActionNames()
    {
        List<string> names = new List<string>();
        foreach(CharacterAction action in actions)
        {
            names.Add(action.ActionName);
        }
        return names.ToArray();
    }
}
