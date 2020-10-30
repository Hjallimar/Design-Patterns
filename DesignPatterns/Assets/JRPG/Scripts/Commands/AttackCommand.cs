using System.Threading;
using UnityEngine;

public class AttackCommand : Command
{
    protected float damage = 0f;
    protected GameObject target = default;

    protected string trigger = "";
    protected bool animationstarted = false;
    protected float animationTimer = 0f;
    protected float halftime = 0f;
    protected Animator animator = null;

    public void AssignCommand(float dmg, GameObject enemy)
    {
        damage = dmg;
        target = enemy;
    }

    public void AssignAnimation(Animator anim, string trigger, float timer)
    {
        animator = anim;
        this.trigger = trigger;
        animationTimer = timer;
    }
    public override void ExecuteCommand()
    {
        animationTimer -= Time.deltaTime;
        if (!animationstarted)
        {
            animator.SetTrigger(trigger);
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
