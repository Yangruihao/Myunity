using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Networking;

/// <summary>
/// 语音提示控制
/// </summary>
public class UITipControl : Singleton<UITipControl>
{
    // 缓动动画时长 
    public float m_ElasticTime = 0.2f;
    // 如果提示语自动隐藏，自动隐藏的等待时间
    private float m_AudioPlayingTime;
    // 提示与音频源
    public AudioSource m_PeiyinAudio;
    public AudioSource BackAudio;
    // 提示语的文本
    public Text m_Text;
    // 提示语弹框的默认Y轴位置 - 隐藏
    private const float m_StartPosY = 740;
    // 提示语弹框的移动Y轴位置 - 显示
    private const float m_EndPosY = 450;
    //所有的提示音频文件
    private Dictionary<string, AudioClip> keyValuesOfClip = new Dictionary<string, AudioClip>();
    Tweener tweener;
    RectTransform rect;
    private void Start()
    {
        if (m_Text == null)
            m_Text = transform.GetChild(0).GetComponent<Text>();
        
        OnInit();

        EventCenter.AddListener(GameEventModel.OnJumpScene, OnResetState);
    }
    private void OnInit()
    {
        var clips = Resources.LoadAll<AudioClip>(AppConst.Res_AudioPeiYin);

        for (int i = 0; i < clips.Length; i++)
        {
            keyValuesOfClip.Add(clips[i].name, clips[i]);
        }

        tweener = transform.DOLocalMoveY(m_EndPosY, m_ElasticTime);
        tweener.SetAutoKill(false);
        tweener.Pause();

        rect = GetComponent<RectTransform>();
    }
    private void OnDestroy()
    {
        EventCenter.RemoveListener(GameEventModel.OnJumpScene, OnResetState);
    }

    // 显示提示
    public void ShowTip(string tipContent)
    {
        tweener.Pause();
        StopAllCoroutines();

        string strTipContent = tipContent;

        //一行最多限制35个字符
        rect.sizeDelta = new Vector2(rect.sizeDelta.x, 70 + 20 * (strTipContent.Length / 35));

        if (strTipContent.Length > 35)
        {
            //一行最多限制35个字符
            for (int i = 0; i < strTipContent.Length / 35; i++)
            {
                strTipContent = strTipContent.Insert(35 * (i + 1), "\n");
            }
        }

        m_Text.text = strTipContent;

        tweener.OnComplete(() =>
        {
            m_AudioPlayingTime = 3.0f;
            
            //查找配音文件
            if (GetClip(tipContent))
            {
                m_PeiyinAudio.clip = GetClip(tipContent);
                m_AudioPlayingTime = m_PeiyinAudio.clip.length;
                m_PeiyinAudio.Play();
            }

            StartCoroutine(HideMove());
        }).Restart();
    }
    // 隐藏提示框协程
    private IEnumerator HideMove()
    {
        yield return new WaitForSeconds(m_AudioPlayingTime);
        HideTip();
    }
    //获取音频文件
    public AudioClip GetClip(string nameClip)
    {
        if (keyValuesOfClip.ContainsKey(nameClip))
        {
            return keyValuesOfClip[nameClip];
        }
        return null;
    }
    /// <summary>
    /// 隐藏提示语
    /// </summary>
    public void HideTip()
    {
        if (transform.localPosition.y == (m_StartPosY))
            return;

        tweener.OnComplete(() =>
        {
            m_Text.text = "";
        }).PlayBackwards(); ;
    }
    public void BackMusicOnPause() 
    {
        BackAudio.Pause();
    }
    public void BackMusicOnResume()
    {
        BackAudio.Play();
    }

    public void OnResetState()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, m_StartPosY, 0);
    }
}