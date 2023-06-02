using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// MianScene流程脚本
/// </summary>
public class MainGmeRoot : UIPanelBase
{
   

    public GameObject obj_Canvas;


    // Start is called before the first frame update
    void Start()
    {
        UIManager.Instance.PushPanel("MainPanel");
        //Resources.
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
