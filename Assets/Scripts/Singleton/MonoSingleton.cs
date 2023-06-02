using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T>:MonoBehaviour where T:MonoBehaviour  
{
    private static T instance;

    public static T Instance {
        get 
        {
            if (instance == null)
            {
                //生成承载单例脚本的空物体
                GameObject Gme = new GameObject(typeof(T).ToString());
                //单例模式对象更换场景不移除
                DontDestroyOnLoad(Gme);
                //instance赋值
                instance = Gme.AddComponent<T>();
            }

            return instance;
        }

    }
}
