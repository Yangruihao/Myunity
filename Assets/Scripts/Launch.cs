using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// 启动场景脚本
/// 
/// </summary>
public class Launch : MonoBehaviour
{
    public GameObject m_MainCanvas;

    //UIManager
    private void Awake()
    {
        InitObj();
        //获取本地配置
    }

   
    /// <summary>
    /// 初始化
    /// </summary>
    private void InitObj()
    {
        //初始化UICanvas
        GameObject go = Resources.Load<GameObject>(AppConst.Res_MainCanvas);
        m_MainCanvas = Instantiate(go);
        m_MainCanvas.name = go.name;

        //加载主场景
        DataManager.ActiveScene = "MainScene";
        SceneManager.LoadSceneAsync(DataManager.ActiveScene);
        SceneManager.LoadScene("Loading");
    }
}
