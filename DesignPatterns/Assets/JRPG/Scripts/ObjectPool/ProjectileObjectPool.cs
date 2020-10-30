using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileObjectPool : MonoBehaviour
{
    private static ProjectileObjectPool instance = null;

    private Dictionary<GameObject, List<GameObject>> objectPool = new Dictionary<GameObject, List<GameObject>>();

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public static void ReturnProjectile(GameObject gO)
    {
        gO.SetActive(false);
        bool found = false;
        foreach(GameObject obj in instance.objectPool.Keys)
        {
            if(obj.GetType() == gO.GetType())
            {
                instance.objectPool[obj].Add(gO);
                found = true;
            }
        }

        if (!found)
        {
            instance.objectPool.Add(gO, new List<GameObject>());
            instance.objectPool[gO].Add(gO);
        }
    }

    public static GameObject GetProjectile(GameObject gO)
    {
        bool found = false;
        foreach(GameObject obj in instance.objectPool.Keys)
        {
            if(obj.GetType() == gO.GetType())
            {
                if (instance.objectPool[obj].Count > 0)
                {
                    GameObject returnObj = instance.objectPool[obj][0];
                    instance.objectPool[obj].RemoveAt(0);
                    return returnObj;
                }
                else
                {
                    return CreateCopy(gO);
                }
                found = true;
            }
        }
        if (!found)
        {
            return CreateCopy(gO);
        }
        else 
        {
            return null;
        }
    }

    private static GameObject CreateCopy(GameObject gO)
    {
        GameObject copy = Instantiate(gO);
        return copy;
    }
}
