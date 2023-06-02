using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 弹框（实验目的，实验原理...）
/// </summary>
public class DialogBox : UIPanelBase
{
    public static DialogBox instance;
    // 工具栏标题
    public Text m_TitleTxt;
    // 工具栏内容
    public Text txtContent;

    private int index = -1;
    public int showIndex
    {
        get { return index; }
        set { index = value; }
    }
    //关闭按钮
    Button btnClose;

    public bool isShow = false;
    public override void Awake()
    {
        base.Awake();
        instance = this;

        btnClose = transform.Find("CloseBtn").GetComponent<Button>();
        btnClose.onClick.AddListener(delegate ()
        {
            OnHide();
        });
    }
    public void OnInit(int index)
    {
        m_TitleTxt.text = DataManager.Lab_Info[index + 1].name;
        txtContent.text = DataManager.Lab_Info[index + 1].content;

        showIndex = index;
    }
    public void Show(int index)
    {
        if (index != showIndex)
        {
            transform.localScale = Vector3.one * 0.2f;

            transform.DOScale(1, 0.2f);

            isShow = true;

            OnInit(index);
        }
        else
        {
            OnHide();
        }
    }
    public override void OnShow()
    {
        //面板显示动画
        if (!isShow)
        {
            transform.DOScale(1, 0.2f);

            isShow = true;

            //OnInit();
        }
        else
        {
            OnHide();
        }
    }

    public override void OnHide()
    {
        //面板隐藏动画
        transform.DOScale(0.01f, 0.2f).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });

        isShow = false;
        showIndex = -1;
    }
}
