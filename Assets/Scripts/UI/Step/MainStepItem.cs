using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MainStepItem : MonoBehaviour
{
    // 实验步骤索引值： 0 第一步， 1 第二步，以此类推
    public int m_Index;
    // 实验步骤预制体
    public GameObject m_Step;
    // 实验步骤预制体
    public GameObject m_StepPoint;
    // 大步骤的步骤描述文本
    public Text m_Text;
    // 画线
    public Image Line;
    // 子步骤的预制体组件
    public GameObject m_SubStepContainer;

    //纹理
    [Space(10)]
    // 步骤的普通（normal）纹理
    public Sprite stepSpriteNormal;
    // 步骤的鼠标滑动上去显示的（Hover）纹理
    public Sprite stepSpriteHover;
    // 步骤被选择后显示的（Selected）纹理
    public Sprite stepSpriteSelected;

    // 实验步骤上的那个圆点的纹理
    public Sprite stepPointNormal;
    public Sprite stepPointHover;
    public Sprite stepPointSelected;

    //  按钮子步骤的自动关闭倒的等待时间
    public float m_WaitToClose = 3.0f;

    // 当前大步骤下的子步骤列表
    public List<SubStepItem> m_AllSubStep;

    // 当前大步骤状态是否被选择
    private bool m_Selected;

    private Image imagestep;
    public Image imageStep
    {
        get
        {
            if (imagestep == null)
                imagestep = m_Step.GetComponent<Image>();

            return imagestep;
        }
        set { imagestep = value; }
    }
    private Image imagesteppoint;
    public Image imageStepPoint
    {
        get
        {
            if (imagesteppoint == null)
                imagesteppoint = m_StepPoint.GetComponent<Image>();

            return imagesteppoint;
        }
        set { imagesteppoint = value; }
    }
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(() =>
        {
            UIStepControl.Instance.ToMainStep(m_Index);
        });
    }
    // 设置大步骤的内容信息。 index： 索引， label: 步骤描述
    //public void OnInit(int index, string name)
    //{
    //    m_Index = index - 1;

    //    m_Text.text = name;

    //    if (m_Index % 2 != 0)
    //    {
    //        m_Step.transform.localPosition = new Vector3(0, -46.0f, 0);
    //    }

    //    AddSubSteps();
    //}
    public void OnInit(int index, StepOneLevel stepOneLevel)
    {
        m_Index = index;

        m_Text.text = stepOneLevel.step_name;

        if (index % 2 != 0)
        {
            m_Step.transform.localPosition = new Vector3(0, -46.0f, 0);
        }

        AddSubSteps(stepOneLevel.children);
    }
    // 添加子步骤，子步骤是个列表
    public void AddSubSteps(List<LabSubStep> labSubSteps)
    {
        if (m_SubStepContainer.transform.childCount > 0)
        {
            Debug.LogWarning("只能初始化添加一次");
            return;
        }
        m_AllSubStep = new List<SubStepItem>();
        GameObject subStepItem = Resources.Load<GameObject>(AppConst.Res_SubStepItem);
        for (int i = labSubSteps.Count - 1; i >= 0; i--)
        {
            GameObject tempSubStep = Instantiate(subStepItem, m_SubStepContainer.transform);
            SubStepItem subStep = tempSubStep.GetComponent<SubStepItem>();
            m_AllSubStep.Add(subStep);
            subStep.OnOnit(i, labSubSteps[i].sub_name, this);
            //设置子步骤位置
            float topY = tempSubStep.transform.localPosition.y + (i - 1) * 40;
            tempSubStep.transform.localPosition = new Vector3(tempSubStep.transform.localPosition.x, topY, 0);
        }
        //刷新Line
        if (labSubSteps.Count > 0)
        {
            Line.GetComponent<RectTransform>().sizeDelta = new Vector2(44 + labSubSteps.Count * 40, 4);
        }
        m_SubStepContainer.SetActive(false);
        Line.gameObject.SetActive(false);
    }
    // 添加子步骤，根据内容添加。
    //public void AddSubSteps(string[] labSubSteps)
    //{
    //    if (m_SubStepContainer.transform.childCount > 0)
    //    {
    //        Debug.LogWarning("只能初始化添加一次");
    //        return;
    //    }
    //    m_AllSubStep = new List<SubStepItem>();
    //    GameObject step = Resources.Load<GameObject>(AppConst.Res_SubStepItem);
    //    //var subStepInfos = DataManager.stepData.rows.Where(a => a.stepnum == (m_Index + 1).ToString()).ToList();

    //    //for (int i = subStepInfos.Count - 1; i >= 0; i--)
    //    //{
    //    //    sortcode = 0的为大步骤
    //    //    if (subStepInfos[i].sortcode != "0")
    //    //    {
    //    //        GameObject tempSubStep = Instantiate(step, m_SubStepContainer.transform);
    //    //        SubStepItem subStep = tempSubStep.GetComponent<SubStepItem>();
    //    //        m_AllSubStep.Add(subStep);
    //    //        subStep.OnOnit(i, subStepInfos[i].itemname);
    //    //    }
    //    //}   
    //    ////设置线的长度
    //    //if (subStepInfos.Count > 0)
    //    //{
    //    //    Line.GetComponent<RectTransform>().sizeDelta = new Vector2(44 + (subStepInfos.Count - 1) * 40, 4);
    //    //}
    //    for (int i = labSubSteps.Length - 1; i >= 0; i--)
    //    {
    //        GameObject tempSubStep = Instantiate(step, m_SubStepContainer.transform);
    //        SubStepItem subStep = tempSubStep.GetComponent<SubStepItem>();
    //        m_AllSubStep.Add(subStep);
    //        subStep.OnOnit(i, labSubSteps[i]);
    //    }

    //    //设置线的长度
    //    if (labSubSteps.Length > 0)
    //    {
    //        Line.GetComponent<RectTransform>().sizeDelta = new Vector2(44 + labSubSteps.Length * 40, 4);
    //    }
    //    m_SubStepContainer.SetActive(false);
    //    Line.gameObject.SetActive(false);
    //}
    // 选择步骤
    public void SelectedHandle()
    {
        OnMouseIn();

        m_Selected = true;
        UIStepControl.Instance.MainStepClick = this;
    }
    // 取消选择步骤
    public void DeselectedHandle()
    {
        m_Selected = false;

        m_SubStepContainer.SetActive(false);
        Line.gameObject.SetActive(false);

        imageStep.sprite = stepSpriteNormal;
        imageStepPoint.sprite = stepPointNormal;
    }
    //鼠标进入
    public void OnMouseIn()
    {
        UIStepControl.Instance.CancelAllStepSelected();

        imageStep.sprite = stepSpriteHover;
        imageStepPoint.sprite = stepPointHover;

        m_SubStepContainer.SetActive(true);
        Line.gameObject.SetActive(true);

        //出现动画
        m_SubStepContainer.transform.localRotation = Quaternion.Euler(0, 90, 0);
        StopCoroutine("ToClose");
        m_SubStepContainer.transform.DOLocalRotateQuaternion(Quaternion.Euler(new Vector3(0, 0, 0)), 0.4f);
    }
    //鼠标移出
    public void OnMouseOut()
    {
        if (m_Selected)
        {
            imageStep.sprite = stepSpriteHover;
            imageStepPoint.sprite = stepPointHover;
        }
        else
        {
            imageStep.sprite = stepSpriteNormal;
            imageStepPoint.sprite = stepPointNormal;

            if (UIStepControl.Instance.MainStepClick != null)
            {
                UIStepControl.Instance.MainStepClick.imageStep.sprite = stepSpriteSelected;
            }
        }
    }


    //关闭
    private IEnumerator ToClose()
    {
        yield return new WaitForSeconds(m_WaitToClose);
        m_SubStepContainer.transform.localRotation = Quaternion.Euler(0, 0, 0);
        m_SubStepContainer.transform.DOLocalRotateQuaternion(Quaternion.Euler(new Vector3(0, 90, 0)), 0.4f);
    }
}
