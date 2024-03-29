﻿/**
 * 
 * Usage
 * To make any class a singleton, simply inherit from the Singleton base class instead of MonoBehaviour, like so:

    public class MySingleton : Singleton<MySingleton>
    {
        // (Optional) Prevent non-singleton constructor use.
        protected MySingleton() { }
 
        // Then add whatever code to the class you need as you normally would.
        public string MyTestString = "Hello world!";
    }
   // Now you can access all public fields, properties and methods from the class anywhere using <ClassName>.Instance:

    public class MyClass : MonoBehaviour
    {
        private void OnEnable()
        {
            Debug.Log(MySingleton.Instance.MyTestString);
        }
    }
 */
using UnityEngine;

/// <summary>
/// 单例基类 - 继承本类实现单例
/// e.g. public class MyClassName : Singleton<MyClassName> {}
/// </summary>
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    // Check to see if we're about to be destroyed.
    private static bool m_ShuttingDown = false;
    private static object m_Lock = new object();
    private static T m_Instance;

    /// <summary>
    /// Access singleton instance through this propriety.
    /// </summary>
    public static T Instance
    {
        get
        {
            //if (m_ShuttingDown)
            //{
            //    Debug.LogWarning("[Singleton] Instance '" + typeof(T) + "' already destroyed. Returning null.");
            //    return null;
            //}
            lock (m_Lock)
            {
                if (m_Instance == null)
                {
                    // Search for existing instance.
                    m_Instance = (T)FindObjectOfType(typeof(T));
                    // Create new  instance if one doesn't already exist.
                    if (m_Instance == null)
                    {
                        // Need to create a new GameObject to attach the singleton to.
                        var singletonObject = new GameObject();
                        m_Instance = singletonObject.AddComponent<T>();
                        singletonObject.name = typeof(T).ToString() + " (Singleton)";
                        // Make instance persistent.
                        DontDestroyOnLoad(singletonObject);
                    }
                }
                return m_Instance;
            }
        }
    }

    private void OnApplicationQuit()
    {
        //m_ShuttingDown = true;
    }

    private void OnDestroy()
    {
        m_ShuttingDown = true;
    }

}
