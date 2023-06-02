using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

/// <summary>
/// 工具类
/// </summary>
public static class StaticUtils
{
    /// <summary>
    /// 延迟后执行方法
    /// </summary>
    /// <param name="mono"></param>
    /// <param name="delayTime"></param>
    /// <param name="action"></param>
    /// <param name="ignoreTimeScale"></param>
    /// <returns></returns>
    public static Coroutine DelayToDo(this MonoBehaviour mono, float delayTime, Action action, bool isIgnoreTimeScale = false)
    {
        Coroutine coroutine = null;

        if (isIgnoreTimeScale)
        {
            coroutine = mono.StartCoroutine(DelayIgnoreTimeToDo(delayTime, action));
        }
        else
        {
            coroutine = mono.StartCoroutine(DelayToInvokeDo(delayTime, action));
        }
        return coroutine;
    }

    public static IEnumerator DelayToInvokeDo(float delaySeconds, Action action)
    {
        yield return new WaitForSeconds(delaySeconds);
        action();
    }

    public static IEnumerator DelayIgnoreTimeToDo(float delaySeconds, Action action)
    {
        float start = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup < start + delaySeconds)
        {
            yield return null;
        }
        action();
    }

    public static bool IsNullOrEntry(this string str)
    {
        return String.IsNullOrEmpty(str);
    }

    #region Transform拓展
    public static void SetPositionX(this Transform t, float newX)
    {
        t.position = new Vector3(newX, t.position.y, t.position.z);
    }

    public static void SetPositionY(this Transform t, float newY)
    {
        t.position = new Vector3(t.position.x, newY, t.position.z);
    }

    public static void SetPositionZ(this Transform t, float newZ)
    {
        t.position = new Vector3(t.position.x, t.position.y, newZ);
    }
    public static void SetLocalPositionX(this Transform t, float newX)
    {
        t.localPosition = new Vector3(newX, t.localPosition.y, t.localPosition.z);
    }

    public static void SetLocalPositionY(this Transform t, float newY)
    {
        t.localPosition = new Vector3(t.localPosition.x, newY, t.localPosition.z);
    }

    public static void SetLocalPositionZ(this Transform t, float newZ)
    {
        t.localPosition = new Vector3(t.localPosition.x, t.localPosition.y, newZ);
    }
    public static void SetLocalAngleX(this Transform t, float newX)
    {
        t.localEulerAngles = new Vector3(newX, t.localEulerAngles.y, t.localEulerAngles.z);
    }

    public static void SetLocalAngleY(this Transform t, float newY)
    {
        t.localPosition = new Vector3(t.localEulerAngles.x, newY, t.localEulerAngles.z);
    }

    public static void SetLocalAngleZ(this Transform t, float newZ)
    {
        t.localPosition = new Vector3(t.localEulerAngles.x, t.localEulerAngles.y, newZ);
    }
    public static void SetLocalScaleX(this Transform t, float newX)
    {
        t.localScale = new Vector3(newX, t.localScale.y, t.localScale.z);
    }

    public static void SetLocalScaleY(this Transform t, float newY)
    {
        t.localScale = new Vector3(t.localScale.x, newY, t.localScale.z);
    }

    public static void SetLocalScaleZ(this Transform t, float newZ)
    {
        t.localScale = new Vector3(t.localScale.x, t.localScale.y, newZ);
    }
    #endregion

    public static void AddEventTriggerListener(this EventTrigger eventTrigger, EventTriggerType triggerType, UnityAction<BaseEventData> action)
    {
        EventTrigger.Entry entry = eventTrigger.triggers.Find((trigger) => trigger.eventID == triggerType);

        if (entry == null)
        {
            entry = new EventTrigger.Entry() { eventID = triggerType };
            eventTrigger.triggers.Add(entry);
        }
        entry.callback.AddListener(action);
    }
}
