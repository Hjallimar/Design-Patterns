using UnityEngine;

public abstract class MeeleAction : CharacterAction
{
    [SerializeField] private float damage = 10f;
    [SerializeField] protected string animationTrigger = "";
    [SerializeField] protected float actionDuration = 0.5f;
    protected float Damage { get { return damage; } }
    protected Vector3 originPosition { get; set; }
    protected Quaternion originRotation { get; set; }

    public override void ActivateAction()
    {
        Debug.Log("The action is incomplete");
    }

    public override void UndoAction()
    {
        Debug.Log("The action is incomplete");
    }
}
