using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/ProjectileFire")]
public class ProjectileFire : CharacterAction
{
    [SerializeField] GameObject projectile = null;
    [SerializeField] float damage = 10f;
    [SerializeField] float speedMod = 2;
    
    public override void ActivateAction()
    {
        ProjectileCommand command = new ProjectileCommand();
        command.Target = Target;
        command.Damage = damage;
        command.projectile = projectile;
        command.Origin = Performer.transform;
        command.SpeedModifier = speedMod;
        PlayerCommand.AddCommand(command);
    }

    public override void UndoAction()
    {
        PlayerCommand.UndoLastCommand();
    }
}
