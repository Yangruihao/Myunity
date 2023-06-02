using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.Events;
using System.Collections.Generic;
/// <summary>
/// 主UI控制类 - 需继承UIToolBase
/// </summary>
public class MainControl : Singleton<MainControl>
{
    void Start()
    {
        DontDestroyOnLoad(this);
        //初始化 实验步骤
        Init();
        //登录验证
        Verify();
    }

    private void Init()
    {
        // 实例化生成实验步骤栏
        //UIStepControl.Instance.Initialize(DataManager.Lab_Step);

        DataManager.StepIndex = 0;

        DataManager.SubStepsIndex = 0;

        //验证成功后 开始实验
        EventCenter.AddListener(GameEventModel.OnVerifySuccess, StartExperiment);

        EventCenter.AddListener<int, int>(GameEventModel.OnStepChangedCallback, StepChangedHandle);
    }
    private void OnDestroy()
    {
        EventCenter.RemoveListener(GameEventModel.OnVerifySuccess, StartExperiment);

        EventCenter.RemoveListener<int, int>(GameEventModel.OnStepChangedCallback, StepChangedHandle);
    }
    /// <summary>
    /// 登录验证
    /// </summary>
    private void Verify()
    {
        VerifyLogin.Instance.StartVerifyLogin();
    }
    /// <summary>
    /// 验证成功 开始实验
    /// </summary>
    private void StartExperiment()
    {
        UIStepControl.Instance.ToMainStep(DataManager.StepIndex, DataManager.SubStepsIndex);
    }
    // 步骤跳转回调
    public void StepChangedHandle(int stepIndex, int subStepIndex)
    {
        DataManager.perStepTime = 0;

        EventCenter.InvokeEvent(GameEventModel.OnJumpScene);
        // 每次会重新加载一下场景，保证一些物体的属性初始化，在对应步骤脚本中控制二级步骤初始化
        SceneManager.LoadScene(DataManager.Lab_StepSceneName[stepIndex]);
    }
    /// <summary>
    /// 提交实验中用到的数据，保存到服务器上，同时也保存到本地，以免刷新之后数据会重置。 
    /// </summary>
    private static void SubmitShiyanData()
    {
        if (DataManager.userId != "")
        {
            WWWForm wwwForm = new WWWForm();

            wwwForm.AddField(AppConst.PracticeId, DataManager.PracticeId);

            wwwForm.AddField(AppConst.CreateUserId, DataManager.userId);

            HttpRequest.Instance.Post(DataManager.HttpRequest + AppConst.AddTemValue_Path, wwwForm, null, null);
        }
    }
}
