using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ViewBase : MonoBehaviour
{
    public virtual void Init()
    {

    }

    public virtual void Show()
    {
        this.gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        this.gameObject.SetActive(false);
    }


    public virtual void Destory()
    {
        GameObject.Destroy(this.gameObject);
    }
}
