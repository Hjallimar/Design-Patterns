﻿using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject target = null;
    [SerializeField] private string heroName = "Gunnar";
    [SerializeField] private float health = 50;
    [SerializeField] private Sprite profile = null;
    [SerializeField] private CharacterAction[] actions = new CharacterAction[3];
    [SerializeField] private Animator animator = null;
    CharacterAction usedAction = null;
    protected bool aliveStatus = true;

    public void Start()
    {
        PlaningPhase.AssignHero(heroName, health);
        animator.SetBool("Idel", true);
    }

    public void UseAction(int index)
    {
        index = index % (actions.Length);
        usedAction = actions[index];
        usedAction.Performer = this;
        usedAction.Target = target.transform;
        usedAction.ActivateAction();
        PlaningPhase.ActionUsed(usedAction.ActionName);
    }

    public Animator GetAnimator()
    {
        return animator;
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
            health = 0;
        }
        PlaningPhase.UpdatePlayerHealth(heroName, health);
    }

    public void Die()
    {
        aliveStatus = false;
        animator.SetTrigger("Die");
    }

    public bool Alive()
    {
        return aliveStatus;
    }
}
