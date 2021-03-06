﻿using UnityEngine;

public abstract class CharacterAction : ScriptableObject
{
    [SerializeField] private string actionName = "Action";
    public string ActionName { get { return actionName; } }
    public Transform Target { get; set; }
    public Transform Performer { get; set; }
    public abstract void ActivateAction();
    public abstract void UndoAction();
}
