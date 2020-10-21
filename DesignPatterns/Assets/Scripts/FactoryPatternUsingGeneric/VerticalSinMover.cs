using UnityEngine;

public class VerticalSinMover : MonoBehaviour
{
    [SerializeField] float speed = 2f;

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, Mathf.Sin(speed * Time.time), transform.position.z);
    }
}
