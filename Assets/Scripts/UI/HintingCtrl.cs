using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;

public class HintingCtrl : UIPanelBase
{
    public static HintingCtrl instance;

    public Text txtHintingTitle;
    public Text txtHintingContent;

    public Button btnSure_Hinting;

    public GameObject gmeTarget;

    float timeMove = 0.4f;
    public override void Awake()
    {
        base.Awake();

        instance = this;
    }
    public void OnInit(string strTitle = "", string strContent = "", UnityAction action = null)
    {
        txtHintingTitle.text = strTitle;
        txtHintingContent.text = strContent;

        btnSure_Hinting.onClick.RemoveAllListeners();
        btnSure_Hinting.onClick.AddListener(action);
    }
    public void OnInit(string strTitle = "", string strContent = "", GameObject targetPos = null, UnityAction action = null)
    {
        txtHintingTitle.text = strTitle;
        txtHintingContent.text = strContent;
        gmeTarget = targetPos;
        btnSure_Hinting.onClick.RemoveAllListeners();
        btnSure_Hinting.onClick.AddListener(action);
    }
    public override void OnShow()
    {
        gameObject.SetActive(true);
        transform.DOLocalMove(Vector3.zero, timeMove);
        transform.DOScale(Vector3.one, timeMove);
    }
    public override void OnHide()
    {
        transform.DOMove(gmeTarget.transform.position, timeMove);
        transform.DOScale(Vector3.zero, timeMove);
    }
}
