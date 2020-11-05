using System;
using UnityEngine;

public class HelpComponent : MonoBehaviour
{
    [field: SerializeField] private static Camera mainCam = null;

    public static Action<Transform> TargetAssigner = delegate { };

    public static float TAU { get
        {
            return Mathf.PI * 2;
        } 
    }

    public void Awake()
    {
        mainCam = Camera.main;
    }

    public static Ray GetMouseRay()
    {
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        return ray;
    }

}
