using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIOver : MonoBehaviour
{
    public Button Restart_btn, Report_btn, Stay_btn, Exit_btn;

    public Action<string> CallbackHandler;
    void Start()
    {
        Restart_btn.onClick.AddListener(OnRestart);//重新开始
        Report_btn.onClick.AddListener(OnReport);//填写报告
        Stay_btn.onClick.AddListener(OnStay);//留在当前页
        Exit_btn.onClick.AddListener(OnExit);//退出实验
    }
    private void OnRestart()
    {
        this.gameObject.SetActive(false);
        CallbackHandler?.Invoke("restart");
    }
    private void OnReport()
    {
        this.gameObject.SetActive(false);
        //UIManager.Instance.OpenPanel(PanelType.PanelExperimentReport);
    }
    private void OnStay()
    {
        this.gameObject.SetActive(false);
    }
    private void OnExit()
    {
        this.gameObject.SetActive(false);
    }
}
