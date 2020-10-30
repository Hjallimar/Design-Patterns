using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void PlayAnimatonTrigger(GameObject gO, string trigger)
    {
        if (gameObject == gO)
        {
            animator.SetTrigger(trigger);
        }
    }

    public void PlayAnimationBool(GameObject gO, string name, bool status)
    {
        if (gameObject == gO)
        {
            animator.SetBool(name, status);
        }
    }

    public void PlayAnimationFloat(GameObject gO, string name, float value)
    {
        if (gameObject == gO)
        {
            animator.SetFloat(name, value);
        }
    }

    public void PlayAnimationInt(GameObject gO, string name, int value)
    {
        if (gameObject == gO)
        {
            animator.SetInteger(name, value);
        }
    }

}
