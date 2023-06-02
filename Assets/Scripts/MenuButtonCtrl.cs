using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtonCtrl : MonoBehaviour
{
    /// <summary>
    /// 实验目的按钮
    /// </summary>
    public Button btnAim;
    /// <summary>
    /// 实验原理按钮
    /// </summary>
    public Button btnYuanLi;
    /// <summary>
    /// 实验方法按钮
    /// </summary>
    public Button btnFangFa;
    /// <summary>
    /// 实验注意事项按钮
    /// </summary>
    public Button btnZhuYi;
    /// <summary>
    /// 实验器材按钮
    /// </summary>
    public Button btnQiCai;
    /// <summary>
    /// 实验药剂按钮
    /// </summary>
    public Button btnYaoJi;
    /// <summary>
    /// 
    /// </summary>
    public Button btnBaoGao;
    // Start is called before the first frame update
    void Start()
    {
        btnAim.onClick.AddListener(() =>
        {
            OnClickOpenDialog(0);
        });
        btnYuanLi.onClick.AddListener(() =>
        {
            OnClickOpenDialog(1);
        });
        btnFangFa.onClick.AddListener(() =>
        {
            OnClickOpenDialog(2);
        });
        btnZhuYi.onClick.AddListener(() =>
        {
            OnClickOpenDialog(3);
        });
        btnBaoGao.onClick.AddListener(()=> 
        {
            UIManager.Instance.PushPanel(PanelType.PanelExperimentReport);
        });
    }

    public void OnClickOpenDialog(int index)
    {
        //UIManager.Instance.PushPanel(PanelType.PanelDialogBox);

        UIManager.Instance.GetPanel(PanelType.PanelDialogBox).SetActive(true);
        DialogBox.instance.Show(index);
    }
}
