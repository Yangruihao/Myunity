using Assets.Scripts.Net.Serial;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

/// <summary>
/// 登录验证 在Loading之前
/// </summary>
public class VerifyLogin : Singleton<VerifyLogin>
{
    /// <summary>
    /// URL数据字典
    /// </summary>
    private Dictionary<string, string> URLDataDic;
    private string urlMsg = string.Empty;

    public void StartVerifyLogin()
    {
        GetURLData();
        GetData();
        Verify();
    }
    // 获取url中的数据
    public void GetData()
    {
        if (URLDataDic.ContainsKey("token"))
        {
            DataManager.Token = "Bearer " + URLDataDic["token"]; //token值
            print("-----key：token" + "-----value：" + DataManager.Token);
        }
        if (URLDataDic.ContainsKey("practiceid"))
        {
            DataManager.PracticeId = URLDataDic["practiceid"]; //实验练习id -当前项目使用 practiceid与pubtaskid一次操作只有一个值
            print("-----key：practiceid" + "-----value：" + URLDataDic["practiceid"]);
        }
        if (URLDataDic.ContainsKey("pubtaskid"))
        {
            DataManager.PubtaskId = URLDataDic["pubtaskid"]; //实验操作任务id -当前项目不适用此值 practiceid与pubtaskid一次操作只有一个值
            print("-----key：pubtaskid" + "-----value：" + URLDataDic["pubtaskid"]);
        }
        if (URLDataDic.ContainsKey("url"))
        {
            DataManager.HttpRequest = URLDataDic["url"]; //接口地址
            print("-----key：url" + "-----value：" + URLDataDic["url"]);
        }
        if (URLDataDic.ContainsKey("checkreporttype"))
        {
            DataManager.Checkreporttype = URLDataDic["checkreporttype"]; //检查报告类型
            print("-----key：checkreporttype" + "-----value：" + URLDataDic["checkreporttype"]);
        }

        DataManager.StepIndex = 0;
        DataManager.SubStepsIndex = 0;
    }
    private void GetExamInfo(string data)
    {
        if (data != null)
        {
            string msg = "VerifyLogin GetExamInfo 考核时间: " + data;
            Debug.Log(msg);
            DataManager.examTime = 1200;
        }
        else
        {
            Debug.Log("无时间数据");
        }
    }
    /// <summary>
    /// 获取H5页面传递来的URL和参数
    /// </summary>
    /// <returns></returns>
    private void GetURLData()
    {
        URLDataDic = new Dictionary<string, string>();
#if UNITY_EDITOR
        urlMsg = "http://192.168.2.8:8849/NYSTXT/?token=91DC9060AEE2D1618C8AB1028E79116E4D561103591EFD2D223B7AE5F5122186&practiceid=1163cd8949d442cd8957010a4706fcd3&url=http://192.168.2.8:8846/api/&checkreporttype=1";
#else
        urlMsg = Application.absoluteURL; //获取url及传来的参数，显示在浏览器的地址栏中。
#endif

        //截取URL中含"?"后的字符串，判断其后是否有数据
        if (urlMsg.IndexOf('?') != -1)
        {
            //  获取"?"后的字符串
            //  例数据：
            string str = urlMsg.Split('?')[1];

            //  分解
            //"-----key：token            -----value：908FA1C16A30057136A8966601EAEA7B08EF1FC1F3B14181238E53B88BBCA206"
            //"-----key：practiceid       -----value：2884659e5bc54ed2bda9fffba0e677f4"
            //"-----key：url              -----value：http://192.168.2.8:8846/api/"
            //"-----key：checkreporttype  -----value：1"
            string[] strArr = str.Split('&');
            for (int i = 0; i < strArr.Length; i++)
            {
                if (!URLDataDic.ContainsKey("key"))
                {
                    //存入字典
                    URLDataDic.Add(strArr[i].Split('=')[0], strArr[i].Split('=')[1]);
                }
            }
        }
    }
    /// <summary>
    /// 登录验证
    /// </summary>
    private void Verify()
    {

    }
    private void HttpComplete(string data)
    {
        if (data != null)
        {
            VerifyResData vrd = JsonUtility.FromJson<VerifyResData>(data);

            if (vrd.re)
            {
                //验证成功
                Debug.Log("VerifyLogin vrd： success");

                EventCenter.InvokeEvent(GameEventModel.OnVerifySuccess);
            }
            else
            {
                Debug.Log("VerifyLogin vrd： faild");
            }
        }
        else
        {
            Debug.Log("没有数据");
        }
    }
    /// <summary>
    /// 获取练习/考核记录
    /// </summary>
    public void GetPracticeByIdRequest()
    {
        GetPracticeById data = new GetPracticeById(
            DataManager.PracticeId,
            ""
            );
        string infos = JsonMapper.ToJson(data);
        string url = DataManager.HttpRequest + AppConst.GetPracticeById_Path;
        HttpRequest.Instance.Get(url, infos, OnComplete);
    }
    /// <summary>
    /// 步骤开始，添加步骤信息，保存实验步骤记录
    /// </summary>
    public void SendAddStepStartRequest()
    {
        
        AddStep data = new AddStep(
            DataManager.PracticeId,
             (DataManager.StepIndex + 1).ToString() + "*" + DataManager.SubStepsIndex.ToString(),
            DataManager.Lab_Step[DataManager.StepIndex].step_name,
            0,
            0,
            "",
            "",
            "1",
             DataManager.UsedTime,
            0,
            100
            );
        string infos = JsonMapper.ToJson(data);
        string url = DataManager.HttpRequest + AppConst.SaveStep_Path;
        HttpRequest.Instance.Post(url, infos, OnComplete, OnError);
    }
    /// <summary>
    /// 步骤开始，添加步骤信息，保存实验步骤记录
    /// </summary>
    public void SendAddStepEndRequest()
    {
        AddStep data = new AddStep(
            DataManager.PracticeId,
            (DataManager.StepIndex + 1).ToString() + "*" + DataManager.SubStepsIndex.ToString(),
            DataManager.Lab_Step[DataManager.StepIndex].step_name,
            DataManager.perStepTime,
            0,
            "",
            "",
            "0",
            DataManager.UsedTime,
            0,
            100
            );
        string infos = JsonMapper.ToJson(data);
        string url = DataManager.HttpRequest + AppConst.SaveStep_Path;
        HttpRequest.Instance.Post(url, infos, OnComplete, OnError);
    }
    /// <summary>
    /// 保存实验临时值
    /// </summary>
    public void SaveTempValueRequest()
    {
        SaveTempValue data = new SaveTempValue(
            DataManager.PracticeId,
            "",
            "临时值"
            );
        string infos = JsonMapper.ToJson(data);
        string url = DataManager.HttpRequest + AppConst.SaveTempValue_Path;
        HttpRequest.Instance.Post(url, infos, OnComplete, OnError);
    }
    /// <summary>
    /// 保存实验报告
    /// </summary>
    public void SaveExperReportRequest()
    {
        SaveExperReport data = new SaveExperReport(
            DataManager.PracticeId,
            "实验报告内容",
            1
            );
        string infos = JsonMapper.ToJson(data);
        string url = DataManager.HttpRequest + AppConst.SaveExperReport_Path;
        HttpRequest.Instance.Post(url, infos, OnComplete, OnError);
    }
    /// <summary>
    /// 获取实验报告
    /// </summary>
    public void GetReportByStuCheckRecordIdRequest()
    {

    }
    /// <summary>
    /// 保存实验完成状态
    /// </summary>
    public void SaveExperFinishStateRequest()
    {
        SaveExperFinishState data = new SaveExperFinishState(
            DataManager.PracticeId,
            "0",
            DataManager.UsedTime
            );
        string infos = JsonMapper.ToJson(data);
        string url = DataManager.HttpRequest + AppConst.SaveExperFinishState_Path;
        HttpRequest.Instance.Post(url, infos, OnComplete, OnError);
    }

    /// <summary>
    /// 步骤结束，提交步骤信息
    /// </summary>
    public void SubmitStepData()
    {
        Debug.Log("Submit Step Data");
        AddStep data = new AddStep(
            DataManager.PracticeId,
            DataManager.StepIndex + "*" + DataManager.SubStepsIndex,
            DataManager.Lab_Step[DataManager.StepIndex].children[DataManager.SubStepsIndex].sub_name,
            DataManager.perStepTime,
            100,
            "",
            "",
            "0",
            DataManager.UsedTime,
            0,
            100
            );
        string infos = JsonMapper.ToJson(data);
        string url = DataManager.HttpRequest + AppConst.SaveStep_Path;
        HttpRequest.Instance.Post(url, infos, OnComplete, OnError);
    }

    void OnComplete(string res)
    {
        print(res);
        //ReturnMsg rmsg = JsonUtility.FromJson<ReturnMsg>(res);
        //Debug.Log("AddStep res: " + res);
        //print(rmsg.msg);
        //if (rmsg.msg.Equals("成功") || rmsg.msg.Equals("OK"))
        //{
        //    print("实验状态保存成功");
        //}
        //else
        //{
        //    print("实验状态保存失败");
        //}
    }
    void OnError(string err)
    {
        Debug.Log("AddStep err: " + err);
    }
}
