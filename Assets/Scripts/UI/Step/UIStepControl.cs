using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
using System.Linq;

/// <summary>
/// 实验步骤控制
/// </summary>
public class UIStepControl : Singleton<UIStepControl>
{
    // 一级步骤的UIStepBase链表
    private List<MainStepItem> uIStepBases = new List<MainStepItem>();

    //记录点击的一级步骤
    [HideInInspector]
    public MainStepItem MainStepClick;
    //记录点击的二级步骤
    [HideInInspector]
    public SubStepItem subStepClick;
    void Start()
    {

    }

    private bool initialized = false;
    /// <summary>
    /// 初始化生成步骤按钮，只能初始化一次
    /// </summary>
    /// <param name="lab_data"> 步骤内容 </param>
    public void Initialize(List<StepOneLevel> lab_data)
    {
        if (initialized) return;
        initialized = true;
        RectTransform m_StepContainer = transform.Find("StepContainer").GetComponent<RectTransform>();

        // 加载实验步骤的大步骤预制体
        GameObject mainStepPrefab = Resources.Load<GameObject>(AppConst.Res_MainStepItem);

        m_StepContainer.sizeDelta = new Vector2(lab_data.Count * (160 + 20) + 200, m_StepContainer.sizeDelta.y);

        for (int i = 0; i < lab_data.Count; i++)
        {
            // 根据配置生成大步骤
            GameObject tempStep = Instantiate(mainStepPrefab, m_StepContainer.transform);

            uIStepBases.Add(tempStep.GetComponent<MainStepItem>());
            //大步骤初始化
            tempStep.GetComponent<MainStepItem>().OnInit(i, lab_data[i]);
        }
    }

    /// <summary>
    /// 跳转步骤
    /// </summary>
    /// <param name="stepIndex"> 一级步骤，从0开始 </param>
    /// <param name="subStepIndex"> 二级步骤 </param>
    public void ToMainStep(int stepIndex, int subStepIndex = 0)
    {
        if (uIStepBases.Count == 0)
            return;

        DataManager.StepIndex = stepIndex; // 一级步骤
        DataManager.SubStepsIndex = subStepIndex;//二级步骤

        CancelAllStepSelected();

        MainStepItem tempStepScript = uIStepBases[DataManager.StepIndex];
        //选中大步骤
        tempStepScript.SelectedHandle();
        //选中小步骤
        if (tempStepScript.m_AllSubStep.Count > 0)
            tempStepScript.m_AllSubStep[tempStepScript.m_AllSubStep.Count - subStepIndex - 1].Selected();
        //开始跳转场景
        EventCenter.InvokeEvent(GameEventModel.OnStepChangedCallback, DataManager.StepIndex, DataManager.SubStepsIndex);

        print(DataManager.StepIndex);
    }
    // 取消所有大步骤的选中状态
    public void CancelAllStepSelected()
    {
        for (int i = 0; i < uIStepBases.Count; i++)
        {
            uIStepBases[i].DeselectedHandle();
        }
    }

    // 切换
    public void Toggle()
    {
        if (this.gameObject.activeSelf)
        {
            transform.DOScaleY(0, 0.2f).OnComplete(() => this.gameObject.SetActive(false));
        }
        else
        {
            this.gameObject.SetActive(true);
            transform.DOScaleY(1.0f, 0.2f);
        }
    }
    //跳转下一个大步骤
    public void ToNextStep()
    {
        ToMainStep(DataManager.StepIndex + 1, 0);
    }
    //跳转下一个小步骤
    public void ToNextSubStep()
    {
        ToMainStep(DataManager.StepIndex, DataManager.SubStepsIndex + 1);
    }
}
