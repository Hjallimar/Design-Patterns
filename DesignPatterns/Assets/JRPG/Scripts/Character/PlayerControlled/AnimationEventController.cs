using System;

public class AnimationEventController
{
    private static AnimationEventController instance;

    private static AnimationEventController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new AnimationEventController();
            }
            return instance;
        }
    }

    public static Action<EventInfo> SetAnimTrigger = delegate { };
    public static Action<EventInfo> SetAnimBool = delegate { };
    public static Action<EventInfo> SetAnimFloat = delegate { };
    public static Action<EventInfo> SetAnimInt = delegate { };
}
