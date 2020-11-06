using UnityEngine;

public class PlayerMoveCommand : Command
{
    protected Transform playerTrans = default;
    protected Vector3 startPos = Vector3.zero;
    protected Vector3 endPos = Vector3.zero;
    protected float timer = 0;

    public virtual void AssignMove(Transform performer, Vector3 origin, Vector3 target)
    {
        playerTrans = performer.transform;
        startPos = origin;
        endPos = target;
    }

    public override void ExecuteCommand()
    {
        timer += Time.deltaTime * 0.7f;
        ChangeRotation(endPos, playerTrans.position);
        AnimationBoolEvent abe = new AnimationBoolEvent(playerTrans.gameObject, "Run", true);
        AnimationEventController.SetAnimBool(abe);
        playerTrans.position = Vector3.Lerp(startPos, endPos, timer);
        if(timer > 1)
        {
            CommandCompleted();
        }
    }

    protected override void CommandCompleted()
    {
        timer = 0f;
        PlayerCommand.CommandComplete();
        AnimationBoolEvent abe = new AnimationBoolEvent(playerTrans.gameObject, "Run", false);
        AnimationEventController.SetAnimBool(abe);
        ChangeRotation(startPos, endPos);
    }

    protected void ChangeRotation(Vector3 pos1, Vector3 pos2)
    {
        Vector3 dirr = pos1 - pos2;
        float angle = Vector3.Angle(playerTrans.forward, dirr.normalized);
        playerTrans.RotateAround(playerTrans.position, Vector3.up, angle);
    }
}
