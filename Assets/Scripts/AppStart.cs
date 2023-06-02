using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppStart : SingletonAutoMono<AppStart>
{
    private GameObject m_MainCanvas;
    private GameObject m_LookCamera;
    // Start is called before the first frame update
    void Awake()
    {
        if (!DataManager.IsInit)
            Init();
    }
    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {
        InstantiateObj();

        LoadConfigAsync();

        DataManager.IsInit = true;
    }
    /// <summary>
    /// 加载MainCanvas与相机
    /// </summary>
    private void InstantiateObj()
    {
        //GameObject go = Resources.Load<GameObject>(AppConst.Res_LookCamera);
        //m_LookCamera = Instantiate(go);
        //m_LookCamera.name = go.name;

        GameObject go = Resources.Load<GameObject>(AppConst.Res_MainCanvas);
        m_MainCanvas = Instantiate(go);
        m_MainCanvas.name = go.name;
    }
    /// <summary>
    /// 异步加载配置文件，加载完成后开始程序
    /// </summary>
    private void LoadConfigAsync()
    {
        GlobalStorage.Instance.LoadConfigAsync<ItemList>("ExamConfig.json", (itemList) =>
        {
            //DataManager.HttpRequest = itemList.url.httpRequest;

            DataManager.HttpURL = itemList.url.httpUrl;

            DataManager.WapURL = itemList.url.wapUrl;

            //步骤场景名称
            DataManager.Lab_StepSceneName = itemList.stepSceneName;
            //步骤信息
            DataManager.Lab_Step = itemList.lab_steps;
            // 实验信息内容
            DataManager.Lab_Info = itemList.lab_info;
        });

        GlobalStorage.Instance.LoadConfigAsync<RootItemQuestion>("QuestionConfig.json", (itemList) =>
        {
            DataManager.itemQuestions = itemList.itemQuestions;

            //m_LookCamera.SetActive(true);
            m_MainCanvas.SetActive(true);

            Debug.Log("Init Over");
        });
    }
}