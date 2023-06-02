using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubStepItem : MonoBehaviour
{
    //子步骤对应的大步骤
    public MainStepItem mainStepItem;
    //子步骤的索引值
    private int m_Index = 0;
    //子步骤的选择状态
    private bool m_Selected;

    public Text m_Label; //子步骤名字
    public Image m_Image; // 子步骤按钮图片

    [Space(10)]
    // 子步骤的纹理 Normal 是正常纹理
    public Sprite stepSpriteNormal;
    // 子步骤的纹理 Hover 是鼠标滑动上去显示的纹理
    public Sprite stepSpriteHover;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            UIStepControl.Instance.ToMainStep(mainStepItem.m_Index, m_Index);
        });
    }
    // 设置 label
    public void OnOnit(int index, string label, MainStepItem item)
    {
        m_Label.text = label;
        m_Index = index;
        mainStepItem = item;
    }
    // 鼠标移出
    public void OnMouseOut()
    {
        if (m_Selected)
        {
            return;
        }
        m_Image.sprite = stepSpriteNormal;
    }
    // 鼠标移入
    public void OnMouseIn()
    {
        if (m_Selected)
        {
            return;
        }
        m_Image.sprite = stepSpriteHover;
    }
    //鼠标点击选择
    public void Selected()
    {
        //点击二级步骤，查看UIStepControl中记录的二级步骤，不为空则取消其选择状态
        if (UIStepControl.Instance.subStepClick != null)
        {
            UIStepControl.Instance.subStepClick.Deselected();
        }
        //将当前点击的二级步骤设为自己
        UIStepControl.Instance.subStepClick = this;

        m_Selected = true;
        m_Image.sprite = stepSpriteHover;
    }
    // 取消选择
    public void Deselected()
    {
        m_Selected = false;
        m_Image.sprite = stepSpriteNormal;
    }
}
