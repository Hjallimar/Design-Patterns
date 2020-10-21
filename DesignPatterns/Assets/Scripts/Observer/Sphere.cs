using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{

    private Vector3[] positions = new Vector3[] {new Vector3(2,2,0), new Vector3(-2, -2, 0), new Vector3(0, 0, 0) };

    private Transform sphereTransform;
    
    // Start is called before the first frame update
    void Start()
    {
        sphereTransform = transform;
    }

    // Update is called once per frame
    void OnEnable()
    {
        Button.Clicked += ChangePos;
    }

    void OnDisable()
    {
        Button.Clicked -= ChangePos;
    }

    private void ChangePos()
    {
        for (int i = 0; i < positions.Length; i++)
        {
            if (sphereTransform.position == positions[i])
            {
                sphereTransform.position = positions[(i + 1) % positions.Length];
                break;
            }
        }
    }
}
