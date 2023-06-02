using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//按钮处理
public class TypeSelect : MonoBehaviour
{
    //子物体下所有按钮
    private List<Button> btnChild;
    public List<Button> BtnChild
    {
        get
        {
            if (btnChild == null)
            {
                btnChild = new List<Button>();

                for (int i = 0; i < transform.childCount; i++)
                {
                    btnChild.Add(transform.GetChild(i).GetComponent<Button>());
                }
            }
            return btnChild;
        }
        set { btnChild = value; }
    }
    //子物体下所有按钮控制
    private List<ButtonSelect> listBtnSelect;
    public List<ButtonSelect> ListBtnSelect
    {
        get
        {
            if (listBtnSelect == null)
            {
                listBtnSelect = new List<ButtonSelect>();

                for (int i = 0; i < transform.childCount; i++)
                {
                    listBtnSelect.Add(transform.GetChild(i).GetComponent<ButtonSelect>());
                }
            }
            return listBtnSelect;
        }
        set { listBtnSelect = value; }
    }
    private void OnEnable()
    {
        OnInit();
    }
    public void OnInit()
    {
        //按钮初始化
        string[] selects = UIQuestion.Instance.currQuestionItem.arraySelect.Split(UIQuestion.Instance.splitChar);

        for (int i = 0; i < BtnChild.Count; i++)
        {
            if (i < selects.Length)
            {
                BtnChild[i].gameObject.SetActive(true);

                ListBtnSelect[i].OnInitSelect(selects[i]);
            }
            else
            {
                ListBtnSelect[i].gameObject.SetActive(false);
            }
        }
    }
    public void ResetOtherBtnSelcet()
    {
        for (int i = 0; i < ListBtnSelect.Count; i++)
        {
            ListBtnSelect[i].OnResetSelectState();
        }
    }
}
