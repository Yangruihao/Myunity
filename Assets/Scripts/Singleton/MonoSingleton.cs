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
                //���ɳ��ص����ű��Ŀ�����
                GameObject Gme = new GameObject(typeof(T).ToString());
                //����ģʽ��������������Ƴ�
                DontDestroyOnLoad(Gme);
                //instance��ֵ
                instance = Gme.AddComponent<T>();
            }

            return instance;
        }

    }
}
