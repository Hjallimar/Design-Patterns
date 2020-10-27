using UnityEngine;

public class AttackCommand : Command
{
    protected float damage = 0f;
    protected GameObject target = default;

    public void AssignCommand(float dmg, GameObject enemy)
    {
        damage = dmg;
        target = enemy;
    }

    public override void ExecuteCommand()
    {
        IDamageable dmgTarget = target.GetComponent<IDamageable>();
        if(dmgTarget != null)
        {
            dmgTarget.TakeDamage(damage);
        }

        CommandCompleted();

    }
    public override void CommandCompleted()
    {
        PlayerCommand.CommandComplete();
    }
}
