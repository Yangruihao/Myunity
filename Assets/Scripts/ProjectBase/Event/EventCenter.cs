using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameEventModel
{
    //跳转场景关掉所有自定义UI页面
    public const string OnJumpScene = "JumpScene";
    //成功登录
    public const string OnVerifySuccess = "VerifySuccess";
    //步骤跳转
    public const string OnStepChangedCallback = "StepChangedCallback";
}
/// <summary>
/// 利用委托的事件中心
/// </summary>
public static class EventCenter
{
    public delegate void CallBack();
    public delegate void CallBack<T>(T t_value);
    public delegate void CallBack<T, Z>(T t_value, Z z_value);
    public delegate void CallBack<T, Z, W>(T t_value, Z z_value, W w_value);

    private static Dictionary<string, Delegate> dictEventHandle;

    /// <summary>
    /// 调用事件
    /// </summary>
    /// <param name="eventName"></param>
    public static void InvokeEvent(string eventName)
    {
        //检测字典中是否已经包含该事件
        if (dictEventHandle == null)
        {
            throw new Exception("字典中不存在此事件!");
        }

        Delegate dele;

        if (dictEventHandle.TryGetValue(eventName, out dele))
        {
            if (dele == null)
            {
                throw new Exception("委托尚未添加事件.");
            }

            CallBack call = dele as CallBack;

            if (call != null)
            {
                call();
            }
            else
            {
                throw new Exception(string.Format("事件中{0}包含着不同类型的委托!", eventName));
            }

            //((EventHandle)dele)?.Invoke();
        }
    }
    public static void InvokeEvent<T>(string eventName, T t_value)
    {
        //检测字典中是否已经包含该事件
        if (dictEventHandle == null)
        {
            throw new Exception("字典中不存在此事件!");
        }

        Delegate dele;

        if (dictEventHandle.TryGetValue(eventName, out dele))
        {
            if (dele == null)
            {
                throw new Exception("委托尚未添加事件.");
            }

            CallBack<T> call = dele as CallBack<T>;

            if (call != null)
            {
                call(t_value);
            }
            else
            {
                throw new Exception(string.Format("事件中{0}包含着不同类型的委托!", eventName));
            }
        }
    }
    public static void InvokeEvent<T, Z>(string eventName, T t_value, Z z_value)
    {
        //检测字典中是否已经包含该事件
        if (dictEventHandle == null)
        {
            throw new Exception("字典中不存在此事件!");
        }

        Delegate @dele;

        if (dictEventHandle.TryGetValue(eventName, out @dele))
        {
            if (@dele == null)
            {
                throw new Exception("委托尚未添加事件.");
            }

            CallBack<T, Z> call = @dele as CallBack<T, Z>;

            if (call != null)
            {
                call(t_value, z_value);
            }
            else
            {
                throw new Exception(string.Format("事件中{0}包含着不同类型的委托!", eventName));
            }
        }
    }
    public static void InvokeEvent<T, Z, W>(string eventName, T t_value, Z z_value, W w_value)
    {
        //检测字典中是否已经包含该事件
        if (dictEventHandle == null)
        {
            throw new Exception("字典中不存在此事件!");
        }

        Delegate dele;

        if (dictEventHandle.TryGetValue(eventName, out dele))
        {
            if (dele == null)
            {
                throw new Exception("委托尚未添加事件.");
            }

            CallBack<T, Z, W> call = dele as CallBack<T, Z, W>;

            if (call != null)
            {
                call(t_value, z_value, w_value);
            }
            else
            {
                throw new Exception(string.Format("事件中{0}包含着不同类型的委托!", eventName));
            }
        }
    }
    /// <summary>
    /// 添加监听事件
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="handle"></param>
    public static void AddListener(string eventName, CallBack handle)
    {
        AddDelegateToDict(eventName, handle);

        dictEventHandle[eventName] = (CallBack)dictEventHandle[eventName] + handle;
    }
    public static void AddListener<T>(string eventName, CallBack<T> handle)
    {
        AddDelegateToDict(eventName, handle);

        dictEventHandle[eventName] = (CallBack<T>)dictEventHandle[eventName] + handle;
    }
    public static void AddListener<T, Z>(string eventName, CallBack<T, Z> handle)
    {
        AddDelegateToDict(eventName, handle);

        dictEventHandle[eventName] = (CallBack<T, Z>)dictEventHandle[eventName] + handle;

        Debug.Log("AddOver");
    }
    public static void AddListener<T, Z, W>(string eventName, CallBack<T, Z, W> handle)
    {
        AddDelegateToDict(eventName, handle);

        dictEventHandle[eventName] = (CallBack<T, Z, W>)dictEventHandle[eventName] + handle;
    }
    /// <summary>
    /// 向字典中添加委托
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="callBack"></param>
    static void AddDelegateToDict(string eventName, Delegate callBack)
    {
        //判断字典是否为空
        if (dictEventHandle == null)
        {
            //如果为空,则初始化一次
            dictEventHandle = new Dictionary<string, Delegate>();
        }

        if (!dictEventHandle.ContainsKey(eventName))
        {
            dictEventHandle.Add(eventName, null);
        }


        //判断添加事件类型与委托类型是否一致
        Delegate dele = dictEventHandle[eventName];
     
        if (dele != null && dele.GetType() != callBack.GetType())
        {
            throw new Exception(string.Format("尝试添加两种不同类型的委托,委托1为{0},委托2为{1}", dele.GetType(), callBack.GetType()));
        }
    }
    /// <summary>
    /// 移除事件监听
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="handle"></param>
    public static void RemoveListener(string eventName, CallBack handle)
    {
        if (dictEventHandle == null)
            return;

        OnCheckListenerRemove(eventName, handle);

        dictEventHandle[eventName] = (CallBack)dictEventHandle[eventName] - handle;
    }
    public static void RemoveListener<T>(string eventName, CallBack<T> handle)
    {
        if (dictEventHandle == null)
            return;

        OnCheckListenerRemove(eventName, handle);

        dictEventHandle[eventName] = (CallBack<T>)dictEventHandle[eventName] - handle;
    }
    public static void RemoveListener<T, Z>(string eventName, CallBack<T, Z> handle)
    {
        if (dictEventHandle == null)
            return;

        OnCheckListenerRemove(eventName, handle);

        dictEventHandle[eventName] = (CallBack<T, Z>)dictEventHandle[eventName] - handle;
    }
    public static void RemoveListener<T, Z, W>(string eventName, CallBack<T, Z, W> handle)
    {
        if (dictEventHandle == null)
            return;

        OnCheckListenerRemove(eventName, handle);

        dictEventHandle[eventName] = (CallBack<T, Z, W>)dictEventHandle[eventName] - handle;
    }

    /// <summary>
    /// 移除事件判断
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="callBack"></param>
    static void OnCheckListenerRemove(string eventName, Delegate callBack)
    {
        if (dictEventHandle.ContainsKey(eventName))
        {
            Delegate dele = dictEventHandle[eventName];

            if (dele != null && dele.GetType() != callBack.GetType())
            {
                throw new Exception(string.Format("尝试移除不同类型的事件,事件名{0},储存的事件类型{1},移除的事件类型{2}", eventName, dele.GetType(), callBack.GetType()));
            }
        }
        else
        {
            throw new Exception(string.Format("当前事件名:{0}不存在!", eventName));
        }
    }
    
    /// <summary>
    /// 移除所有委托的监听事件
    /// </summary>
    public static void Clear()
    {
        if (dictEventHandle == null)
        {
            return;
        }

        foreach (var item in dictEventHandle)
        {
            Internal_RemoveEvent(item.Key, false);
        }

        dictEventHandle.Clear();
    }
    
    static void Internal_RemoveEvent(string eventName, bool IsRemoveFromDic)
    {
        if (dictEventHandle == null)
            return;

        if (dictEventHandle.ContainsKey(eventName))
        {
            var callBack = dictEventHandle[eventName];

            //得到委托的调用列表
            Delegate[] invokeList = callBack.GetInvocationList();

            foreach (var item in invokeList)
            {
                Delegate.Remove(callBack, item);
            }

            if (IsRemoveFromDic)
            {
                dictEventHandle.Remove(eventName);
            }
        }
    }
}
