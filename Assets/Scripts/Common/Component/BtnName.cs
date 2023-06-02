using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

/// <summary>
/// UI按钮名称提示 - 自定义通用组件
/// </summary>
public class BtnName : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // 按钮的 label 滑出方向
    public enum X_MOVE_TO_DIR
    {
        left,
        right
    }
    /** 按钮text的滑出方向 */
    public X_MOVE_TO_DIR direction_X = X_MOVE_TO_DIR.left;
    /** 按钮text的滑出距离 */
    public float move_Distance_X = 80f;
    /** 按钮text的滑出时间 */
    public float m_ElasticTime = 0.2f;
    /** 滑出方向 */
    private int m_Direction;
    // 按钮文本描述
    private Text m_Text;

    private readonly Color SHOW_COLOR = new Color(255, 255, 255, 255); // 鼠标移入显示透明度为最大 255
    private readonly Color HIDE_COLOR = new Color(255, 255, 255, 0); // 鼠标移入显示透明度为最小 0

    // Start is called before the first frame update
    void Awake()
    {
        m_Text = transform.parent.GetChild(0).GetComponent<Text>();

        if (direction_X == X_MOVE_TO_DIR.left)
        {
            m_Direction = -1;
        }
        else
        {
            m_Direction = 1;
        }
    }
    // 鼠标移入
    public void OnPointerEnter(PointerEventData eventData)
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(m_Text.transform.DOLocalMoveX(move_Distance_X * m_Direction, m_ElasticTime));
        sequence.Insert(0, m_Text.DOColor(SHOW_COLOR, m_ElasticTime));
    }
    // 鼠标移出
    public void OnPointerExit(PointerEventData eventData)
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(m_Text.transform.DOLocalMoveX(0, m_ElasticTime));
        sequence.Insert(0, m_Text.DOColor(HIDE_COLOR, m_ElasticTime));
    }
}
