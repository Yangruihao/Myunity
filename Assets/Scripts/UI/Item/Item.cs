/**
 * 实验器材和实验试剂的单个预制体脚本
 */ 
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    private Sprite _sprite;
    private string _label;

    public Sprite icon
    {
        get
        {
            return _sprite;
        }
    }

    public string label
    {
        get
        {
            return _label;
        }
    }

    private void Start()
    {
    }

    //public void SetContent(string spritePath, string label)
    //{
    //    transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(spritePath);
    //    transform.GetChild(1).GetComponent<Text>().text = label;
    //}

    public void SetContent(Sprite sprite, string label)
    {
        transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = sprite;
        transform.GetChild(1).GetComponent<Text>().text = label;
        _sprite = sprite;
        _label = label;
    }

    public string GetLabel()
    {
        return _label;
    }

    public Button.ButtonClickedEvent onClick
    {
        get
        {
            return transform.GetChild(0).GetComponent<Button>().onClick;
        }
    }
}
