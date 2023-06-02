using UnityEngine;
using System;
/// <summary>
/// 配置文件读取
/// </summary>
public class GlobalStorage : Singleton<GlobalStorage>
{
    public T LoadConfig<T>(string path)
    {
        string data = Resources.Load<TextAsset>(path).text;
        return JsonUtility.FromJson<T>(data);
    }
    public void LoadConfigAsync<T>(string path, Action<T> callback)
    {
        HttpRequest.Instance.Get(Application.streamingAssetsPath+"/" + path, OnLoadConfigOver<T>, callback);
    }
    public T OnLoadConfigOver<T>(string text)
    {
        return JsonUtility.FromJson<T>(text);
    }
}
