using System;
using UnityEngine;

public class AnimationEvents
{
    private static AnimationEvents instance = null;

    public static AnimationEvents Instance 
    {
        get
        {
            if(instance == null)
            {
                instance = new AnimationEvents();
            }
            return instance;
        } 
    }

    public delegate void AnimationTrigger(GameObject gO, string trigger);
    public delegate void AnimationBool(GameObject gO, string name, bool status);
    public delegate void AnimationFloat(GameObject gO, string name, float value);
    public delegate void AnimationInt(GameObject gO, string name, int value);
}
