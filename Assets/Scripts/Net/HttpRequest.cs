using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Http请求类
/// </summary>
public class HttpRequest : Singleton<HttpRequest>
{/// <summary>
 /// Get函数
 /// </summary>
 /// <param name="url"></param>
 /// <param name="callback"></param>
    public void Get(string url, string data, Action<string> callback)
    {
        StartCoroutine(GetRequest(url, data, callback));
    }
    IEnumerator GetRequest(string url, string data, Action<string> callback)
    {
        using (UnityWebRequest webRequest = new UnityWebRequest(url, "GET"))
        {
            byte[] postBytes = System.Text.Encoding.UTF8.GetBytes(data);
            webRequest.uploadHandler = new UploadHandlerRaw(postBytes);
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader(AppConst.Authorization, DataManager.Token);//Token在获取后已加"Bearer "
            webRequest.SetRequestHeader("Content-Type", "application/json;");
            yield return webRequest.SendWebRequest();
            if (string.IsNullOrEmpty(webRequest.error))
            {
                string result = webRequest.downloadHandler.text;
                callback(result);
            }
            else
            {
                Debug.LogError(webRequest.error);
            }
        }
    }
    /// <summary>
    /// Get函数
    /// </summary>
    /// <param name="url"></param>
    /// <param name="callback"></param>
    public void Get(string url, Action<string> callback)
    {
        StartCoroutine(GetRequest(url, callback));
    }
    public void Get<T>(string url, Func<string, T> callback, Action<T> callback2)
    {
        StartCoroutine(GetRequest(url, callback, callback2));
    }
    IEnumerator GetRequest<T>(string url, Func<string, T> callback, Action<T> callback2)
    {
        UnityWebRequest m_Http = UnityWebRequest.Get(url);
        yield return m_Http.SendWebRequest();
        if (m_Http.result==UnityWebRequest.Result.ConnectionError || m_Http.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogWarning(m_Http.error);
        }
        else
        {
            Debug.Log("result:" + m_Http.downloadHandler.text);
            callback2?.Invoke(callback(m_Http.downloadHandler.text));
        }
    }
    IEnumerator GetRequest(string url, Action<string> callback)
    {
        UnityWebRequest m_Http = UnityWebRequest.Get(url);
        m_Http.SetRequestHeader("Content-Type", "application/json");
        m_Http.SetRequestHeader("Authorization", "Bearer 2420CE8E46F3ADD898872D2D16E3B489338649A7B3C962F1BB824241562B018E");
        yield return m_Http.SendWebRequest();
        if (m_Http.result == UnityWebRequest.Result.ConnectionError || m_Http.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(m_Http.error);
        }
        else
        {
            string result = m_Http.downloadHandler.text;

            callback(result);
        }
    }
    /// <summary>
    /// Post函数
    /// </summary>
    /// <param name="url"> 地址 </param>
    /// <param name="formData"> 数据 </param>
    /// <param name="complete"></param>
    /// <param name="error"></param>
    public void Post(string url, string data, Action<string> complete, Action<string> error)
    {
        Debug.Log("Start Post Request");
        StartCoroutine(PostRequest(url, data, complete, error));
    }
    IEnumerator PostRequest(string url, string data, Action<string> complete, Action<string> error)
    {
        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(data));
            request.SetRequestHeader(AppConst.Authorization, DataManager.Token);//Token在获取后已加"Bearer "
            request.SetRequestHeader("Content-Type", "application/json;");
            request.downloadHandler = new DownloadHandlerBuffer();

            yield return request.SendWebRequest();
            if (string.IsNullOrEmpty(request.error))
            {
                string result = request.downloadHandler.text;
                complete?.Invoke(result);
                print("结果:" + result);
            }
            else
            {
                print(url + "\n" + request.error);
                error?.Invoke(request.error);
            }
        }
    }

    /// <summary>
    /// Post函数
    /// </summary>
    /// <param name="url"> 地址 </param>
    /// <param name="formData"> 数据 </param>
    /// <param name="complete"></param>
    /// <param name="error"></param>
    public void Post(string url, WWWForm formData, Action<string> complete, Action<string> error)
    {
        Debug.Log("Start Post Request");
        StartCoroutine(PostRequest(url, formData, complete, error));
    }
    IEnumerator PostRequest(string url, WWWForm formData, Action<string> complete, Action<string> error)
    {
        UnityWebRequest m_Http = UnityWebRequest.Post(url, formData);
        //string infos = JsonMapper.ToJson(objInfo);
        //byte[] postBytes = System.Text.Encoding.UTF8.GetBytes(infos);
        //m_Http.uploadHandler = new UploadHandlerRaw(postBytes);
        //m_Http.downloadHandler = new DownloadHandlerBuffer();
        //m_Http.SetRequestHeader("Content-Type", "application/json");
        yield return m_Http.SendWebRequest();
        if (m_Http.result == UnityWebRequest.Result.ConnectionError || m_Http.result == UnityWebRequest.Result.ProtocolError)
        {
            print(m_Http.error);

            error?.Invoke(m_Http.error);
        }
        else
        {
            string result = m_Http.downloadHandler.text;
            complete?.Invoke(result);
            print(result);
        }
    }

    ///// <summary>
    ///// Post函数
    ///// </summary>
    ///// <param name="url"> 地址 </param>
    ///// <param name="formData"> 数据 </param>
    ///// <param name="complete"></param>
    ///// <param name="error"></param>
    //public void Post2(string url, Dictionary<string, string> objInfo, Action<string> OnCompleteCallBack, Action<string> OnErrorCallBack = null)
    //{
    //    Debug.Log("Start Post Request");
    //    StartCoroutine(PostRequest2(url, objInfo, OnCompleteCallBack, OnErrorCallBack));
    //}
    //IEnumerator PostRequest2(string url, Dictionary<string, string> objInfo, Action<string> OnCompleteCallBack, Action<string> OnErrorCallBack = null)
    //{
    //    m_Http = new UnityWebRequest(url, "POST");

    //    string infos = JsonMapper.ToJson(objInfo);
    //    byte[] postBytes = System.Text.Encoding.UTF8.GetBytes(infos);
    //    m_Http.uploadHandler = new UploadHandlerRaw(postBytes);
    //    m_Http.downloadHandler = new DownloadHandlerBuffer();
    //    m_Http.SetRequestHeader("Authorization", DataManager.loginData.rows[LoginKey.tokentype.ToString()] + " " + DataManager.loginData.rows[LoginKey.accesstoken.ToString()]);
    //    yield return m_Http.SendWebRequest();

    //    if (m_Http.isNetworkError || m_Http.isHttpError)
    //    {
    //        OnErrorCallBack?.Invoke(m_Http.error);

    //        print(m_Http.error);
    //    }
    //    else
    //    {
    //        OnCompleteCallBack?.Invoke(m_Http.downloadHandler.text);
    //    }
    //}
}
/// <summary>
/// 获取练习/考核记录 GET
/// </summary>
public class GetPracticeById
{
    public GetPracticeById() { }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_practiceid"></param>
    /// <param name="_checkrecordid"></param>
    public GetPracticeById(string _practiceid, string _checkrecordid)
    {
        practiceid = _practiceid;
        checkrecordid = _checkrecordid;
    }
    public string practiceid;
    public string checkrecordid;
}
/// <summary>
/// 保存实验步骤记录 POST
/// </summary>
public class AddStep
{
    //练习id
    public string practiceid;
    //步骤号
    public string stepid;
    //步骤名称
    public string stepname;
    //步骤用时 秒
    public int examduration;
    //步骤得分
    public int stepscore;
    //步骤评价
    public string stepcomment; //非必填
    //赋分模型
    public string scoremodel; //非必填
    //是否开始 0 步骤结束 1 步骤开始
    public string isstart;
    //实验累计用时 秒
    public int usedtime;
    //合理用时 秒
    public int expercttime; //非必填
    //步骤满分
    public int maxscore; //非必填
    public AddStep() { }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_practiceid"></param>
    /// <param name="_stepid"></param>
    /// <param name="_stepname"></param>
    /// <param name="_examduration"></param>
    /// <param name="_stepscore"></param>
    /// <param name="_stepcomment"></param>
    /// <param name="_scoremodel"></param>
    /// <param name="_isstart"></param>
    /// <param name="_usedtime"></param>
    /// <param name="_expercttime"></param>
    /// <param name="_maxscore"></param>
    public AddStep(string _practiceid, string _stepid, string _stepname, int _examduration, int _stepscore, string _stepcomment, string _scoremodel, string _isstart, int _usedtime, int _expercttime, int _maxscore)
    {
        practiceid = _practiceid;
        stepid = _stepid;
        stepname = _stepname;
        examduration = _examduration;
        stepscore = _stepscore;
        stepcomment = _stepcomment;
        scoremodel = _scoremodel;
        isstart = _isstart;
        usedtime = _usedtime;
        expercttime = _expercttime;
        maxscore = _maxscore;
    }
}
/// <summary>
/// 保存实验临时值 POST
/// </summary>
public class SaveTempValue
{
    public SaveTempValue() { }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_practiceid"></param>
    /// <param name="_checkrecordid"></param>
    /// <param name="_tempvalue"></param>
    public SaveTempValue(string _practiceid, string _checkrecordid, string _tempvalue)
    {
        practiceid = _practiceid;
        checkrecordid = _checkrecordid;
        tempvalue = _tempvalue;
    }
    public string practiceid;
    public string checkrecordid;
    public string tempvalue;
}
/// <summary>
/// 保存实验报告 POST
/// </summary>
public class SaveExperReport
{
    public SaveExperReport() { }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_practiceid"></param>
    /// <param name="_projectreport"></param>
    /// <param name="_checkreporttype"></param>
    public SaveExperReport(string _practiceid, string _projectreport, int _checkreporttype)
    {
        practiceid = _practiceid;
        projectreport = _projectreport;
        checkreporttype = _checkreporttype;
    }
    public string practiceid;
    public string projectreport;
    public int checkreporttype;
}
/// <summary>
/// 获取实验报告 GET
/// </summary>
public class GetReportByStuCheckRecordId
{
    public GetReportByStuCheckRecordId() { }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_practiceid"></param>
    /// <param name="_projectreport"></param>
    public GetReportByStuCheckRecordId(string _practiceid, string _projectreport)
    {
        practiceid = _practiceid;
        checkrecordid = _projectreport;
    }
    public string practiceid;
    public string checkrecordid;
}
/// <summary>
/// 保存实验完成状态 POST
/// </summary>
public class SaveExperFinishState
{
    public SaveExperFinishState() { }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_practiceid"></param>
    /// <param name="_projectreport"></param>
    /// <param name="_usedtime"></param>
    public SaveExperFinishState(string _practiceid, string _projectreport, int _usedtime)
    {
        practiceid = _practiceid;
        checkrecordid = _projectreport;
        usedtime = _usedtime;
    }
    public string practiceid;
    public string checkrecordid;
    public int usedtime;
}
