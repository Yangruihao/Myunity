using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
/// <summary>
/// 通过名字 动态加载 
/// 加载路径 Resources/Prefab/UI/Panel/
/// </summary>
public class PanelType
{
    public const string PanelPopWindow = "PanelPopWindow";
    public const string PanelHinting = "PanelHinting";
    public const string PanelQuestion = "PanelQuestion";
    public const string PanelDialogBox = "DialogBox";
    public const string PanelPPT = "PPT";
    public const string PanelDialogTool = "DialogTool";
    public const string PanelMultChoice = "PanelMultChoice";
    public const string PanelExperimentReport = "PanelExperimentReport";
    public const string PanelOver = "PanelOver";
}

public class UIManager : Singleton<UIManager>
{
    //所有Panel的集合字典
    private Dictionary<string, UIPanelBase> dictAllPanel = new Dictionary<string, UIPanelBase>();
    //储存面板的栈
    private Stack<UIPanelBase> panelStack;

    private void Start()
    {
        EventCenter.AddListener(GameEventModel.OnJumpScene, CloseAllPanel);
    }
    private void OnDestroy()
    {
        EventCenter.RemoveListener(GameEventModel.OnJumpScene, CloseAllPanel);
    }
    public GameObject GetPanel(string panelName)
    {
        GameObject go = null;

        if (dictAllPanel.ContainsKey(panelName))
        {
            go = dictAllPanel[panelName].gameObject;

            return go;
        }
        else
        {
            go = CreatePanel(panelName);
        }

        return go;
    }
    /// <summary>
    /// 获取界面的基类
    /// </summary>
    /// <param name="panelType"></param>
    /// <returns></returns>
    public UIPanelBase GetPanelBase(string panelName)
    {
        UIPanelBase panelBase = null;
        if (dictAllPanel.ContainsKey(panelName))
        {
            dictAllPanel[panelName].gameObject.SetActive(true);
            return dictAllPanel[panelName];
        }

        panelBase = CreatePanel(panelName).GetComponent<UIPanelBase>();
        panelBase.gameObject.SetActive(true);
        return panelBase;
    }
    /// <summary>
    /// 入栈，显示面板
    /// </summary>
    /// <param name="panelType"></param>
    public void PushPanel(string panelType)
    {
        if (panelStack == null)
            panelStack = new Stack<UIPanelBase>();

        UIPanelBase topPanel = null;
        UIPanelBase panel = GetPanelBase(panelType);

        if (panelStack.Count > 0)
        {
            topPanel = panelStack.Peek();

            if (topPanel != panel)
            {
                topPanel.OnHide();
            }
        }

        panelStack.Push(panel);
        panel.OnShow();
    }
    /// <summary>
    /// 出栈，隐藏面板
    /// </summary>
    public void PopPanel()
    {
        if (panelStack == null)
            panelStack = new Stack<UIPanelBase>();

        if (panelStack.Count <= 0)
            return;

        UIPanelBase topPanel = panelStack.Pop();
        topPanel.OnHide();

        if (panelStack.Count > 0)
        {
            UIPanelBase nextPanel = panelStack.Peek();
            nextPanel.OnResume();
        }
    }
    /// <summary>
    /// 创建面板
    /// </summary>
    /// <param name="panelName"></param>
    /// <returns></returns>
    private GameObject CreatePanel(string panelName)
    {
        GameObject go = Resources.Load<GameObject>(AppConst.Res_Panel + panelName);
        GameObject panelObj = null;
        if (go != null)
        {
            panelObj = Instantiate(go, transform);
            panelObj.name = panelName;
            dictAllPanel.Add(panelName, panelObj.GetComponent<UIPanelBase>());
        }
        else
        {
            Debug.LogWarning("Panel could not found!");
        }

        return panelObj;
    }
    /// <summary>
    /// 关闭所有面板
    /// </summary>
    public void CloseAllPanel()
    {
        foreach (UIPanelBase item in dictAllPanel.Values)
        {
            item.gameObject.SetActive(false);
        }
    }
}
