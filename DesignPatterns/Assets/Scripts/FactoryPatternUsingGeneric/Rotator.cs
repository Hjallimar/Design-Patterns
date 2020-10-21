using UnityEngine;

public class Rotator : MonoBehaviour
{
    float speed = 120f;

    private void Update()
    {
        transform.Rotate(0f, 0f, speed * Time.deltaTime);
    }
}
