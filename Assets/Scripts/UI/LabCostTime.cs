using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 操作计时器、控制背景音乐
/// </summary>
public class LabCostTime : Singleton<LabCostTime>
{
    // 显示实验时长的文本
    public Text m_Time;
    // 背景音乐播放按钮组件
    public Button m_BgmPlayBtn;
    // 背景音乐静音按钮组件
    public Button m_BgmMuteBtn;
    // 背景音乐的 音频源组件
    public AudioSource m_BgmAudio;

    public int m_StartTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        m_BgmPlayBtn.onClick.AddListener(() =>
        {
            m_BgmAudio.mute = true;
            m_BgmPlayBtn.gameObject.SetActive(false);
            m_BgmMuteBtn.gameObject.SetActive(true);
        });
        m_BgmMuteBtn.onClick.AddListener(() =>
        {
            m_BgmAudio.mute = false;
            m_BgmPlayBtn.gameObject.SetActive(true);
            m_BgmMuteBtn.gameObject.SetActive(false);
        });
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (m_BgmPlayBtn.enabled)
        {
            float currZ = m_BgmPlayBtn.transform.localRotation.eulerAngles.z;
            m_BgmPlayBtn.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, currZ - 1f));
        }


        int during = m_StartTime + (int)Time.time;
        int hour = during / 3600;
        int minute = during % 3600 / 60;
        int second = during % 3600 % 60;

        string hourStr = hour > 9 ? hour.ToString() : "0" + hour.ToString();
        string minuteStr = minute > 9 ? minute.ToString() : "0" + minute.ToString(); ;
        string secondStr = second > 9 ? second.ToString() : "0" + second.ToString(); ;

        m_Time.text = hourStr + ":" + minuteStr + ":" + secondStr;
    }

    public void SetCostTime(int time)
    {
        m_StartTime = time;
    }
}
