using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryManager : MonoBehaviour
{
    RotatorFactory[] rotatorFactiories = new RotatorFactory[0];
    VerticalSinFactory[] verticalFactory = new VerticalSinFactory[0];

    private void Start()
    {
        rotatorFactiories = FindObjectsOfType<RotatorFactory>();
        verticalFactory = FindObjectsOfType<VerticalSinFactory>();

        foreach(RotatorFactory rotator in rotatorFactiories)
        {
            rotator.CreateNewProduct();
        }

        foreach(VerticalSinFactory vertical in verticalFactory)
        {
            vertical.CreateNewProduct();
        }
    }


}