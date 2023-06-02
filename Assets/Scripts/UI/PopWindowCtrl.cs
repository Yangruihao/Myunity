using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PopWindowCtrl : UIPanelBase
{
    public static PopWindowCtrl instance;


    public Text txtTitle;
    public Text txtTanChuang;
    public Text txtSure;

    public Button btnSure_TanChuang;

    public override void Awake()
    {
        base.Awake();

        instance = this;
    }
    public void OnInit(string strContent = "", UnityAction action = null)
    {
        txtTanChuang.text = strContent;
        txtSure.text = "确定";

        btnSure_TanChuang.onClick.RemoveAllListeners();
        btnSure_TanChuang.onClick.AddListener(delegate () { gameObject.SetActive(false); });
        btnSure_TanChuang.onClick.AddListener(action);
    }
    public void OnInit(string strTitle = "", string strContent = "", UnityAction action = null)
    {
        txtTitle.text = strTitle;
        txtTanChuang.text = strContent;
        txtSure.text = "确定";

        btnSure_TanChuang.onClick.RemoveAllListeners();
        btnSure_TanChuang.onClick.AddListener(delegate () { gameObject.SetActive(false); });
        btnSure_TanChuang.onClick.AddListener(action);
    }
    public void OnInit(string strTitle = "", string strContent = "", string strSure = "", UnityAction action = null)
    {
        txtTitle.text = strTitle;
        txtTanChuang.text = strContent;
        txtSure.text = strSure;

        btnSure_TanChuang.onClick.RemoveAllListeners();
        btnSure_TanChuang.onClick.AddListener(delegate () { gameObject.SetActive(false); });
        btnSure_TanChuang.onClick.AddListener(action);
    }
}
