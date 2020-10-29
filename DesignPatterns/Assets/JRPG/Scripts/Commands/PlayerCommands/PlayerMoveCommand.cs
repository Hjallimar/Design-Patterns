using UnityEngine;

public class PlayerMoveCommand : Command
{
    protected Transform playerTrans = default;
    protected Vector3 startPos = Vector3.zero;
    protected Vector3 endPos = Vector3.zero;
    protected float timer = 0;
    public virtual void AssignMove(Transform performer, Vector3 direction, float distance)
    {
        playerTrans = performer;
        startPos = performer.position;
        endPos = startPos + direction.normalized * distance;
    }

    public virtual void AssignMove(Transform performer, Vector3 origin, Vector3 target)
    {
        playerTrans = performer;
        startPos = origin;
        endPos = target;
    }

    public override void ExecuteCommand()
    {
        timer += Time.deltaTime;
        playerTrans.position = Vector3.Lerp(startPos, endPos, timer);
        if(timer > 1)
        {
            CommandCompleted();
        }
    }

    public override void CommandCompleted()
    {
        timer = 0f;
        PlayerCommand.CommandComplete();
    }
}
