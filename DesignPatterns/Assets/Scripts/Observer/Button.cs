using System;
using UnityEngine;

public class Button : MonoBehaviour
{

    public static Action Clicked = delegate { };
    private Rect rect = new Rect(10f, 10f, 100f, 50f);

    private void OnGUI()
    {
        if(GUI.Button(rect, "Clicked"))
        {
            Clicked();
        }
    }

}
