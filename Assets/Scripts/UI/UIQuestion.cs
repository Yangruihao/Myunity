using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

//问题类型
public enum QuestionType
{
    /// <summary>
    /// 单选题
    /// </summary>
    TypeSingle,
    /// <summary>
    /// 多选题
    /// </summary>
    TypeMuit,
    /// <summary>
    /// 判断题
    /// </summary>
    TypeJudge
}
//问题选项
public enum QuestionSelect
{
    A,
    B,
    C,
    D,
    E
}
public class UIQuestion : UIPanelBase
{
    public static UIQuestion Instance;
    //题目类型
    public Text txtTitle;
    //题目内容
    public Text txtContent;
    //确定按钮
    public Button btnSure;
    //提示信息
    public Text txtTip;
    //记录点击
    private List<string> listOnClickSelect = new List<string>();
    //当前问题类型
    public QuestionType questionType;
    //当前问题Item
    public ItemQuestion currQuestionItem;
    //选项、答案的分隔符
    public char splitChar = '/';

    public GameObject gmeTypeBase;
    public GameObject gmeTypeJudge;
    //选择完毕后调用下一步操作
    bool isNext;
    /// <summary>
    /// 选择确定函数回调,也就是下一步的操作
    /// </summary>
    public UnityAction SelectRightCallBack;
    /// <summary>
    /// 判断回答错误函数回调
    /// </summary>
    public UnityAction JudgeWrongCallBack;

    public override void Awake()
    {
        base.Awake();

        Instance = this;

        btnSure.onClick.AddListener(OnClickBtnSure);
    }
    private void OnEnable()
    {
        txtTip.text = "";
    }
    /// <summary>
    /// 显示问题
    /// </summary>
    /// <param name="index"></param>
    /// <param name="callBack"></param>
    public void ShowQuestion(int index, UnityAction callBack = null)
    {
        currQuestionItem = DataManager.itemQuestions[index];

        CheckQuestionType(currQuestionItem);

        listOnClickSelect.Clear();

        if (callBack != null)
            SelectRightCallBack = callBack;

        gmeTypeBase.SetActive(false);
        gmeTypeJudge.SetActive(false);

        switch (questionType)
        {
            case QuestionType.TypeSingle:
                gmeTypeBase.SetActive(true);
                break;
            case QuestionType.TypeMuit:
                gmeTypeBase.SetActive(true);
                break;
            case QuestionType.TypeJudge:
                gmeTypeJudge.SetActive(true);
                break;
            default:
                break;
        }
    }
    //检测问题类型
    public void CheckQuestionType(ItemQuestion item)
    {
        if (item.arraySelect.Split('/').Length == 2)
        {
            questionType = QuestionType.TypeJudge;
            txtTitle.text = "判断题";
            txtContent.text = item.strContent;
        }
        else if (item.arrayRightSelect.Contains(splitChar.ToString()))
        {
            questionType = QuestionType.TypeMuit;
            txtTitle.text = "多选题";
            txtContent.text = item.strContent;
        }
        else
        {
            txtTitle.text = "单选题";
            txtContent.text = item.strContent;
        }
    }
    //点击记录选项
    public void AddSelectToList(string select)
    {
        //单选 清除记录后再记录
        if (questionType == QuestionType.TypeSingle ||
            questionType == QuestionType.TypeJudge)
            listOnClickSelect.Clear();

        listOnClickSelect.Add(select);
    }
    //点击移除选项
    public void RemoveSelectFromList(string select)
    {
        listOnClickSelect.Remove(select);
    }
    //点击确认选择按钮
    private void OnClickBtnSure()
    {
        //没有选择不进行判断
        if (listOnClickSelect.Count == 0)
        {
            return;
        }
        //一般单选、多选问题
        if (questionType == QuestionType.TypeSingle || questionType == QuestionType.TypeMuit)
        {
            if (!isNext)
            {
                if (CheckAnswer())
                {
                    txtTip.text = "选择正确！" + "\n" + "请点击确认按钮进行下一步。";
                }
                else
                {
                    txtTip.text = "选择错误！" + "正确选项是：" + currQuestionItem.arrayRightSelect.Replace(splitChar, '、') + "\n" + "请点击确认按钮进行下一步。";
                }

                isNext = true;
            }
            else
            {
                txtTip.text = "";
                SelectRightCallBack?.Invoke();
                SelectRightCallBack = null;
                OnHide();
                isNext = false;
            }
        }
        //判断题
        if (questionType == QuestionType.TypeJudge)
        {
            if (!isNext)
            {
                if (JudgeWrongCallBack != null)
                {
                    if (CheckAnswer())
                    {
                        SelectRightCallBack?.Invoke();
                        SelectRightCallBack = null;
                    }
                    else
                    {
                        JudgeWrongCallBack?.Invoke();
                        JudgeWrongCallBack = null;
                    }
                    OnHide();
                    isNext = false;
                }
                else
                {
                    if (CheckAnswer())
                    {
                        txtTip.text = "选择正确！" + "\n" + "请点击确认按钮进行下一步。";
                    }
                    else
                    {
                        txtTip.text = "选择错误！" + "正确选项是：" + currQuestionItem.arrayRightSelect.Replace(splitChar, '、') + "\n" + "请点击确认按钮进行下一步。";
                    }

                    isNext = true;
                }
            }
            else
            {
                txtTip.text = "";
                SelectRightCallBack?.Invoke();
                SelectRightCallBack = null;
                OnHide();
                isNext = false;
            }
        }
    }
    //答案检测
    public bool CheckAnswer()
    {
        if (questionType != QuestionType.TypeJudge)
        {
            //题目类型不为判断时，检测答案
            for (int i = 0; i < listOnClickSelect.Count; i++)
            {
                if (!currQuestionItem.arrayRightSelect.Contains(listOnClickSelect[i]))
                {
                    return false;
                }
            }

            if (currQuestionItem.arrayRightSelect.Split(splitChar).Length == listOnClickSelect.Count)
            {
                return true;
            }
        }
        else//题目类型为判断时，检测答案
        {
            if (listOnClickSelect[0] == currQuestionItem.arrayRightSelect)
            {
                return true;
            }

            return false;
        }

        return false;
    }
}
