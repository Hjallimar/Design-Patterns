using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private Renderer targetRenderer;
    private Color[] colorArray = new Color[] {Color.green, Color.red, Color.blue};

    // Start is called before the first frame update
    void OnEnable()
    {
        Button.Clicked += ChangeColor;
        targetRenderer.material.color = colorArray[0];
    }

    // Update is called once per frame
    void OnDisable()
    {
        Button.Clicked -= ChangeColor;
    }

    private void ChangeColor()
    {
        for(int i = 0; i < colorArray.Length; i++)
        {
            if(targetRenderer.material.color == colorArray[i])
            {
                targetRenderer.material.color = colorArray[(i + 1) % colorArray.Length];
                break;
            }
        }
    }
}
