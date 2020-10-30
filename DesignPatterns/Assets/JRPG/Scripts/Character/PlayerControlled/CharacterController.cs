using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour, IDamageable
{
    private float speed = 1f;
    [SerializeField] private GameObject target = null;
    [SerializeField] private string heroName = "Gunnar";
    [SerializeField] private float health = 50;
    [SerializeField] private Sprite profile = null;
    [SerializeField] private CharacterAction[] actions = new CharacterAction[3];
    CharacterAction usedAction = null;

    public void Start()
    {
        PlaningPhase.AssignHero(heroName, health);
    }

    public void UseAction(int index)
    {
        index = index % (actions.Length - 1);
        usedAction = actions[index];
        usedAction.Performer = transform;
        usedAction.Target = target.transform;
        usedAction.ActivateAction();
        PlaningPhase.ActionUsed(usedAction.ActionName);
    }

    public Sprite GetProfile()
    {
        return profile;
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

    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health < 0)
        {
            //die you bihh
            health = 0;
        }
        PlaningPhase.UpdatePlayerHealth(heroName, health);
    }
}
