using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    [SerializeField] private Animator animator = null;

    private void OnEnable()
    {
        AnimationEventController.SetAnimTrigger += PlayAnimatonTrigger;
        AnimationEventController.SetAnimBool += PlayAnimationBool;
        AnimationEventController.SetAnimFloat += PlayAnimationFloat;
        AnimationEventController.SetAnimInt += PlayAnimationInt;
    }

    public void PlayAnimatonTrigger(EventInfo aei)
    {
        if (gameObject == aei.GO)
        {
            animator.SetTrigger(aei.Text);
        }
    }

    public void PlayAnimationBool(EventInfo ei)
    {
        if (gameObject == ei.GO)
        {
            AnimationBoolEvent abe = (AnimationBoolEvent)ei;
            animator.SetBool(abe.Text, abe.Status);
        }
    }

    public void PlayAnimationFloat(EventInfo ei)
    {
        if (gameObject == ei.GO)
        {
            AnimationFloatEvent afe = (AnimationFloatEvent)ei;
            animator.SetFloat(afe.Text, afe.Value);
        }
    }

    public void PlayAnimationInt(EventInfo ei)
    {
        if (gameObject == ei.GO)
        {
            AnimationIntEvent aie = (AnimationIntEvent)ei;
            animator.SetInteger(aie.Text, aie.Value);
        }
    }

    private void OnDisable()
    {
        AnimationEventController.SetAnimTrigger -= PlayAnimatonTrigger;
        AnimationEventController.SetAnimBool -= PlayAnimationBool;
        AnimationEventController.SetAnimFloat -= PlayAnimationFloat;
        AnimationEventController.SetAnimInt -= PlayAnimationInt;
    }
}
