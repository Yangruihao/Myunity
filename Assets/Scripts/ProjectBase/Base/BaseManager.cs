using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//一个简单的单例基类
public class BaseManager<T> : MonoBehaviour where T : MonoBehaviour, new()
{
    private static T instance;

    public static T GetInstance()
    {
        if (instance == null)
        {
            if (FindObjectsOfType(typeof(T)) == null)
            {
                instance = new T();
            }
            else
            {
                instance = FindObjectOfType(typeof(T)) as T;
            }
        }

        return instance;
    }
}
