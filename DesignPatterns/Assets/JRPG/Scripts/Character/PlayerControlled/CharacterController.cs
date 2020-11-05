using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour, IDamageable
{
    [SerializeField] private string heroName = "Gunnar";
    [SerializeField] private float health = 50;
    [SerializeField] private Sprite profile = null;
    [SerializeField] private CharacterAction[] actions = new CharacterAction[3];
    private Transform target = null;
    //private List<CharacterAction> actions = new List<CharacterAction>();
    CharacterAction usedAction = null;
    protected bool aliveStatus = true;

    public void Start()
    {
        PlaningUI.AssignHero(heroName, health);
    }

    public string GetName()
    {
        return heroName;
    }

    public void GetNewTarget(Transform trans)
    {
        target = trans;
    }

    public void UseAction(int index)
    {
        index = index % (actions.Length);
        usedAction = actions[index];
        usedAction.Performer = this;
        usedAction.Target = target.transform;
        usedAction.ActivateAction();
        string actionUpdate = usedAction.Performer.GetComponent<IDamageable>().GetName() + 
            " used " + usedAction.ActionName + " on " + 
            usedAction.Target.GetComponent<IDamageable>().GetName();
        PlaningUI.ActionUsed(actionUpdate);
    }

    public Sprite GetProfile()
    {
        return profile;
    }

    public void UndoAction()
    {
        if(usedAction != null)
        {
            PlaningUI.ActionUndo();
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
            health = 0;
            Die();
        }
        PlaningUI.UpdatePlayerHealth(heroName, health);
    }

    public void Die()
    {
        aliveStatus = false;
        EventInfo ei = new EventInfo(gameObject, "Die");
        AnimationEventController.SetAnimTrigger(ei);
    }

    public bool Alive()
    {
        return aliveStatus;
    }
}
