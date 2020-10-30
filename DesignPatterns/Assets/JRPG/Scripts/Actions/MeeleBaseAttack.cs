using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/MeeleBase")]
public class MeeleBaseAttack : MeeleAction
{
    public override void ActivateAction()
    {
        originPosition = Performer.transform.position;
        PlayerMoveCommand moveCommand = new PlayerMoveCommand();
        moveCommand.AssignMove(Performer, Performer.transform.position,Target.position);
        PlayerCommand.AddCommand(moveCommand);

        AttackCommand attackCommand = new AttackCommand();
        attackCommand.AssignCommand(Damage ,Target.gameObject);

        Debug.Log("trigger: " + animationTrigger);
        attackCommand.AssignAnimation(Performer.GetAnimator(), animationTrigger, actionDuration );
        PlayerCommand.AddCommand(attackCommand);

        PlayerMoveCommand returnCommand = new PlayerMoveCommand();
        returnCommand.AssignMove(Performer, Target.position, originPosition);
        PlayerCommand.AddCommand(returnCommand);
    }

    public override void UndoAction()
    {
        PlayerCommand.UndoLastCommand();
        PlayerCommand.UndoLastCommand();
        PlayerCommand.UndoLastCommand();
    }
}
