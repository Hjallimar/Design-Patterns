using UnityEngine;

public class ProjectileCommand : Command
{
    public GameObject projectile { get; set; }
    public Transform Target { get; set; }
    public Transform Origin { get; set; }

    public float SpeedModifier { get; set; } = 1f;
    public float Damage { get; set; }
    private bool active = false;
    private GameObject myProjectile;
    private Vector3 startPos;
    private float travelTime = 0f;

    public override void ExecuteCommand()
    {
        if (!active)
        {
            active = true;
            myProjectile = ProjectileObjectPool.GetProjectile(projectile);
            myProjectile.SetActive(true);
            myProjectile.transform.position = Origin.position;
            myProjectile.transform.rotation = Origin.rotation;
            startPos = myProjectile.transform.position;
        }
        else
        {
            travelTime += Time.deltaTime * SpeedModifier;
            myProjectile.transform.position = Vector3.Lerp(startPos, Target.position, travelTime);
            if(travelTime > 1)
            {
                CommandCompleted();
            }
        }
    }

    protected override void CommandCompleted()
    {
        ProjectileObjectPool.ReturnProjectile(myProjectile);
        Target.GetComponent<IDamageable>().TakeDamage(Damage);
        PlayerCommand.CommandComplete();
    }
}
