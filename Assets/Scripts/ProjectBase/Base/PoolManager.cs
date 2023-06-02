using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PoolData
{
    public GameObject gmePushFather;

    public List<GameObject> listPool;

    public PoolData(GameObject obj, GameObject gmePoolRoot)
    {
        listPool = new List<GameObject>();

        gmePushFather = new GameObject(obj.name);
        gmePushFather.transform.parent = gmePoolRoot.transform;
        PoolPushObj(obj);
    }
    /// <summary>
    /// 获取物体
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public GameObject PoolGetObj()
    {
        GameObject obj = null;

        obj = listPool[0];
        listPool.RemoveAt(0);
        obj.SetActive(true);
        obj.transform.SetParent(null);
        return obj;
    }
    /// <summary>
    /// 存储物体
    /// </summary>
    /// <param name="name"></param>
    /// <param name="obj"></param>
    public void PoolPushObj(GameObject obj)
    {
        listPool.Add(obj);
        obj.SetActive(false);
        obj.transform.SetParent(gmePushFather.transform);
    }

}
/// <summary>
/// 缓存池
/// </summary>
public class PoolManager : SingletonAutoMono<PoolManager>
{
    /// <summary>
    /// 缓存池字典容器
    /// </summary>
    private Dictionary<string, PoolData> poolDict = new Dictionary<string, PoolData>();

    private GameObject gmePoolRoot;
    /// <summary>
    /// 从缓存池中取出对象
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public GameObject PoolGetObj(string path)
    {
        GameObject obj = null;

        if (poolDict.ContainsKey(path) && poolDict[path].listPool.Count > 0)
        {
            obj = poolDict[path].PoolGetObj();
        }
        else
        {
            obj = GameObject.Instantiate(Resources.Load<GameObject>(path));
            obj.name = GetName(path);
        }

        return obj;
    }

    public GameObject PoolGetObj(string path, Vector3 position, Quaternion rotation)
    {
        GameObject obj = null;

        string name = GetName(path);

        if (poolDict.ContainsKey(name) && poolDict[name].listPool.Count > 0)
        {
            obj = poolDict[name].PoolGetObj();
        }
        else
        {
            obj = GameObject.Instantiate(Resources.Load<GameObject>(path));
            obj.name = GetName(path);
        }

        obj.transform.position = position;
        obj.transform.rotation = rotation;

        return obj;
    }

    /// <summary>
    /// 向缓存池中放回对象
    /// </summary>
    /// <param name="name"></param>
    /// <param name="obj"></param>
    public void PoolPushObj(string name, GameObject obj)
    {
        if (gmePoolRoot == null)
            gmePoolRoot = new GameObject("RootPool");

        if (poolDict.ContainsKey(name))
        {
            poolDict[name].PoolPushObj(obj);
        }
        else
        {
            poolDict.Add(name, new PoolData(obj, gmePoolRoot));
        }
    }

    public string GetName(string path)
    {
        string[] paths = path.Split('/');
     
        return paths[paths.Length - 1].ToString();
    }
    /// <summary>
    /// 清空缓存池
    /// </summary>
    public void PoolClear()
    {
        poolDict.Clear();
        gmePoolRoot = null;
    }
}
