using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UIManager.Instance.PushPanel(PanelType.PanelDialogBox);
        DialogBox.instance.OnInit(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
