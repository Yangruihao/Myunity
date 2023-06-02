using Assets.Scripts.Net.Serial;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PStep : MonoBehaviour
{
    [HideInInspector]
    public Transform[] MoveTargets = new Transform[0];
    [HideInInspector]
    public Transform TargetRoot;
    private void Awake()
    {
        //submitStepData();
        //getStepData();
    }
    protected virtual void Start()
    {
        TargetRoot = GameObject.Find("TargetPos")?.transform;
        if (TargetRoot)
        {
            MoveTargets = new Transform[TargetRoot.childCount];
            for (int i = 0; i < TargetRoot.childCount; i++)
            {
                MoveTargets[i] = TargetRoot.GetChild(i);
            }
        }
        ChangeStep();
    }

    protected virtual void ChangeStep()
    {
        SubmitStepData();
    }

    protected void MoveTo(int index)
    {
        if (index >= MoveTargets.Length)
        {
            Debug.LogError("Index out Range");
            return;
        }
        LookCamera.Instance.MoveTo(MoveTargets[index]);
    }
    public void MoveTo(Transform target)
    {
        LookCamera.Instance.MoveTo(target);
    }

    public void ShowTip(string tip)
    {
        UITipControl.Instance.ShowTip(tip);
    }
   
    //获取步骤信息
    void GetStepData()
    {
        WWWForm form = new WWWForm();

        form.AddField(AppConst.PracticeId, DataManager.PracticeId);

        string url = DataManager.HttpRequest + AppConst.GetPracticeStep_Path;

        HttpRequest.Instance.Post(url, form, delegate (string res)
        {
            Debug.Log("=========== GetPracticeStep : " + res);
            int StepIndex = int.Parse(res.Split('*')[0]);
            int SubStepIndex = int.Parse(res.Split('*')[1]);
            UIStepControl.Instance.ToMainStep(StepIndex, SubStepIndex);
        }, delegate (string error)
        {
            Debug.LogError("error: " + error);
        });


        WWWForm form1 = new WWWForm();
        form1.AddField(AppConst.PracticeId, DataManager.PracticeId);

        string url1 = DataManager.HttpRequest + AppConst.GetPracticeUsedTime_Path;

        HttpRequest.Instance.Post(url1, form1, delegate (string res)
        {
            Debug.Log("=========== GetPracticeUsedTime : " + res);
            if (res != null)
            {
                GetUsedTime gut = JsonUtility.FromJson<GetUsedTime>(res);
                LabCostTime.Instance.SetCostTime(int.Parse(gut.Body));
            }
            else
            {

            }
        }, delegate (string error)
        {
            Debug.LogError("error: " + error);
        });
    }
    //跳转步骤时，提交步骤信息
    public void SubmitStepData()
    {
        Debug.Log("Submit Step Data");
        WWWForm form = new WWWForm();

        form.AddField(AppConst.PracticeId, DataManager.PracticeId);
        form.AddField(AppConst.StepId, DataManager.StepIndex + "*" + DataManager.SubStepsIndex);
        form.AddField(AppConst.Code, DataManager.userId);
        form.AddField(AppConst.IsStart, 0);
        form.AddField(AppConst.ExamDuration, 0);

        string url = DataManager.HttpRequest + AppConst.AddStep_Path;

        HttpRequest.Instance.Post(url, form, TestComplete, OnError);
    }

    void TestComplete(string res)
    {
        Debug.Log("AddStep res: " + res);
    }

    void OnError(string err)
    {
        Debug.Log("AddStep err: " + err);
    }

    private void SendAddStepRequest(int subStepIndex, bool changeStep)
    {
        WWWForm form = new WWWForm();

        form.AddField(AppConst.PracticeId, DataManager.PracticeId);
        form.AddField(AppConst.StepId, DataManager.StepIndex + "*" + DataManager.SubStepsIndex);
        form.AddField(AppConst.Code, DataManager.userId);
        form.AddField(AppConst.ExamDuration, DataManager.perStepTime);
        //form.AddField(AppConst.UsedTime, DataManager.UsedTime().ToString());

        HttpRequest.Instance.Post(DataManager.HttpRequest + AppConst.AddStep_Path, form, null, null);
    }

    public void endShiyan()
    {
        SendAddStepRequest(DataManager.SubStepsIndex + 1, false);

        if (DataManager.isExam)
        {
            DataManager.isSyStepComplete = true;
            if (DataManager.isNeedBaogao)
            {

            }
            else
            {

            }
        }
    }

    public void ReStart()
    {
        WWWForm form = new WWWForm();

        form.AddField(AppConst.PracticeId, DataManager.PracticeId);

        HttpRequest.Instance.Post(DataManager.HttpRequest + AppConst.Restart_Path, form, GetData, null);
    }

    void GetData(string res)
    {
        Debug.Log("---------------- 实验结束， 重新开始");
        UIStepControl.Instance.ToMainStep(0, 0);
    }

}
