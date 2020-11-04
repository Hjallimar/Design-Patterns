using System.Threading;
using UnityEngine;

public class AttackCommand : Command
{
    protected float damage = 0f;
    protected GameObject target = default;
    protected GameObject performer = null;

    protected string trigger = "";
    protected bool animationstarted = false;
    protected float animationTimer = 0f;
    protected float halftime = 0f;

    public void AssignCommand(float dmg, GameObject enemy)
    {
        damage = dmg;
        target = enemy;
    }

    public void AssignAnimation(GameObject performer, string trigger, float timer)
    {
        this.performer = performer;
        this.trigger = trigger;
        animationTimer = timer;
    }
    public override void ExecuteCommand()
    {
        animationTimer -= Time.deltaTime;
        if (!animationstarted)
        {
            EventInfo ei = new EventInfo(performer, trigger);
            AnimationEventController.SetAnimTrigger(ei);
            animationstarted = true;
        }
        else if(animationTimer < 0)
        {
            IDamageable dmgTarget = target.GetComponent<IDamageable>();
            if (dmgTarget != null)
            {
                dmgTarget.TakeDamage(damage);
            }
            CommandCompleted();
        }
    }

    protected override void CommandCompleted()
    {
        PlayerCommand.CommandComplete();
    }
}
