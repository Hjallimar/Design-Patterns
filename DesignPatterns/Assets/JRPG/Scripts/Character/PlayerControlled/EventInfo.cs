using UnityEngine;

public class EventInfo
{
    public GameObject GO { get; private set; }
    public string Text { get; private set; }

    public EventInfo(GameObject gO, string text)
    {
        GO = gO;
        Text = text;
    }
}

public class AnimationBoolEvent : EventInfo
{
    public bool Status { get; private set; }
    public AnimationBoolEvent(GameObject gO, string text, bool status) : base(gO, text)
    {
        Status = status;
    }
}
public class AnimationFloatEvent : EventInfo
{
    public float Value { get; private set; }
    public AnimationFloatEvent(GameObject gO, string text, float value) : base(gO, text)
    {
        Value = value;
    }
}
public class AnimationIntEvent : EventInfo
{
    public int Value { get; private set; }
    public AnimationIntEvent(GameObject gO, string text, int value) : base(gO, text)
    {
        Value = value;
    }
}
