using UnityEngine;

public class PlayerMoveCommand : Command
{
    protected Transform playerTrans = default;
    protected Vector3 playerMove = Vector3.zero;
    protected float timer = 0;
    public virtual void AssignMove(Transform performer, Vector3 direction, float distance)
    {
        playerTrans = performer;
        playerMove = direction.normalized * distance;
    }

    public virtual void AssignMove(Transform performer, Vector3 target)
    {
        playerTrans = performer;
        float dist = Vector3.Magnitude(performer.position - target);
        Vector3 dirr =  target - performer.position;
        playerMove = dirr.normalized * dist;
    }

    public override void ExecuteCommand()
    {
        Debug.Log("My player trans is: " + playerTrans.name);
        playerTrans.transform.position += playerMove;
    }

    public override void CommandCompleted()
    {
        PlayerCommand.CommandComplete();
    }
}
