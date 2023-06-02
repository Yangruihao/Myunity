using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Ingredient
{
    public Text ShiyanDuixiang;
    public Text ShiyanQicai;
    public Text ShiyanShiji;
}
public class UIExperimentReport : UIPanelBase
{
    [Space(10)]
    public Text ShiyanLaoshi;
    public Text ShiyanRenyuan;
    public Text ShiyanMingcheng;
    public Text ShiyanMudi;
    public Text ShiyanYuanli;

    [Space(10)]
    public Ingredient ShiyanFangFa;

    [Space(10)]
    public Text ShiyanBuzhou;
    public Text ShiyanJieguo;
    public Text ShiyanTaolun;
    public Text ShiyanJielun;

    [Header("http request status")]
    public GameObject LoadingAnim;
    public Button SubmitButton;
    public Button btnClose;
    public Text LoadingMsg;

    public bool isShow;
    public override void Awake()
    {
        base.Awake();

        btnClose = transform.Find("CloseBtn").GetComponent<Button>();
        btnClose.onClick.AddListener(delegate() 
        {
            OnHide();
        });
    }
    private void Update()
    {
        //进度图片旋转
        if (LoadingAnim.activeSelf)
        {
            LoadingAnim.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, LoadingAnim.transform.localRotation.eulerAngles.z - 1));
        }
    }
    public void Submit()
    {
        LoadingAnim.SetActive(true);
        SubmitButton.interactable = false;
        LoadingMsg.text = "正在提交，请等待……";

        WWWForm form = new WWWForm();

        form.AddField(AppConst.UserId, DataManager.userId);
        form.AddField(AppConst.CreateUserName, ShiyanRenyuan.text.Trim());
        form.AddField(AppConst.EClassId, "");
        form.AddField(AppConst.Instructor, ShiyanLaoshi.text.Trim());
        form.AddField(AppConst.EName, ShiyanMingcheng.text.Trim());
        form.AddField(AppConst.Purpose, ShiyanMudi.text.Trim());
        form.AddField(AppConst.Principle, ShiyanYuanli.text.Trim());
        form.AddField(AppConst.Objects, ShiyanFangFa.ShiyanDuixiang.text.Trim());
        form.AddField(AppConst.Quipment, ShiyanFangFa.ShiyanQicai.text.Trim());
        form.AddField(AppConst.Reagent, ShiyanFangFa.ShiyanShiji.text.Trim());
        form.AddField(AppConst.Step, ShiyanBuzhou.text.Trim());
        form.AddField(AppConst.Result, ShiyanJieguo.text.Trim());
        form.AddField(AppConst.Discuss, ShiyanTaolun.text.Trim());
        form.AddField(AppConst.Conclusion, ShiyanJielun.text.Trim());
        form.AddField(AppConst.MedicinePracticeId, DataManager.PracticeId);

        HttpRequest.Instance.Post(DataManager.HttpRequest + AppConst.SaveMedicineExperiment_Path, form, OnComplete, null);

    }
    //提交实验报告
    private void OnComplete(string result)
    {
        SubmitButton.interactable = true;
        LoadingAnim.SetActive(false);
        LoadingMsg.text = "提交成功！";
        StartCoroutine(OnCompleteNext());
    }

    private void OnError(string errorMsg)
    {
        SubmitButton.interactable = true;
        LoadingAnim.SetActive(false);
        LoadingMsg.text = string.Format("提交失败: {0}", errorMsg);
    }
    IEnumerator OnCompleteNext()
    {
        yield return new WaitForSeconds(3f);

        gameObject.SetActive(false);
    }

    public override void OnShow()
    {
        //面板显示动画
        if (!isShow)
        {
            transform.DOScale(1, 0.2f);

            isShow = true;
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
    }

    public override void OnPause()
    {
        throw new NotImplementedException();
    }

    public override void OnResume()
    {
        throw new NotImplementedException();
    }
}
