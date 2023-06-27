using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class UIPanelBase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //用于界面的控制显示和隐藏
    private CanvasGroup canvasGroup;
    
    public virtual void Awake()
    {
        if (GetComponent<CanvasGroup>() == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
        else
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }
    }
    //鼠标移入界面
    public void OnPointerEnter(PointerEventData eventData)
    {
        DataManager.InDialog = true;
    }
    //鼠标移出界面
    public void OnPointerExit(PointerEventData eventData)
    {
        DataManager.InDialog = false;
    }

    /// <summary>
    /// 面板显示
    /// </summary>
    public virtual void OnShow()
    {
        gameObject.SetActive(true);
    }
    /// <summary>
    /// 面板隐藏
    /// </summary>
    public virtual void OnHide()
    {
        gameObject.SetActive(false);
    }
    /// <summary>
    /// 面板暂停
    /// </summary>
    public virtual void OnPause()
    {
        canvasGroup.blocksRaycasts = false;
    }
    /// <summary>
    /// 面板恢复
    /// </summary>
    public virtual void OnResume()
    {
        canvasGroup.blocksRaycasts = true;
    }
    /// <summary>
    /// 展开弹窗 
    /// </summary>
    /// <param name="timeNum">时间</param>
    public virtual void OnDoShowGme(GameObject gme, float timeNum = 0.5f)
    {

        gme.transform.localScale = Vector3.zero;
        gme.SetActive(true);
        gme.transform.DOScale(Vector3.one, timeNum);
    }

    public virtual void OnDoShowGme(float timeNum = 0.5f)
    {

        gameObject.transform.localScale = Vector3.zero;
        gameObject.SetActive(true);
        gameObject.transform.DOScale(Vector3.one, timeNum);
    }

    public virtual void OnDoHideGme()
    {
        //gameObject.transform.localScale = Vector3.zero;
        //gameObject.SetActive(true);
        gameObject.transform.DOScale(Vector3.zero, 0.5f);
    }
    public virtual void OnDoHideGme(GameObject gme, float timeNum = 0.5f)
    {
        gme.transform.localScale = Vector3.one;
        gme.transform.DOScale(Vector3.zero, timeNum);
        //gme.SetActive(false);
    }
}
