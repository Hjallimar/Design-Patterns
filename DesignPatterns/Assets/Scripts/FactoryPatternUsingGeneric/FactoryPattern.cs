using UnityEngine;

public class FactoryPattern<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] T product;

    public virtual void CreateNewProduct()
    {
        Instantiate(product);
    }


    public void Do(float f)
    {

    }

    public void Do<T>(T f)
    {

    }
}
