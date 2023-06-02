using UnityEngine;
using UnityEngine.UI;

public class ButtonSelect : MonoBehaviour
{
    private Button BtnSelect;
    public Button btnSelect
    {
        get
        {
            if (BtnSelect == null)
                BtnSelect = GetComponent<Button>();
            return BtnSelect;
        }
        set { BtnSelect = value; }
    }

    private Text content;
    public Text txtContent
    {
        get
        {
            if (content == null)
            {
                content = transform.GetChild(0).GetComponent<Text>();
            };

            return content;
        }
        set { content = value; }
    }

    private TypeSelect select;
    public TypeSelect typeSelect
    {
        get
        {
            if (select == null)
            {
                select = transform.parent.GetComponent<TypeSelect>();
            };

            return select;
        }
        set { select = value; }
    }

    //当前按钮是否选中
    bool IsSelect = false;
    //按钮选项，区分按钮
    public QuestionSelect currSelect;

    Color colorSelect = new Color(150 / 255f, 150 / 255f, 255 / 255f);
    Color colorNormal = new Color(255 / 255f, 255 / 255f, 255 / 255f);
    // Start is called before the first frame update
    void Start()
    {
        btnSelect.onClick.RemoveAllListeners();
        btnSelect.onClick.AddListener(delegate ()
        {
            if (!IsSelect)
            {
                OnButtonSelect();
            }
            else
            {
                OnButtonCancel();
            }
        });
    }
    public void OnInitSelect(string info)
    {
        OnResetSelectState();
        //选项内容
        txtContent.text = info;
    }
    //点击选择按钮
    public void OnButtonSelect()
    {
        if (UIQuestion.Instance.questionType == QuestionType.TypeSingle)
        {
            typeSelect.ResetOtherBtnSelcet();
        }
        IsSelect = true;

        btnSelect.image.color = colorSelect;

        UIQuestion.Instance.AddSelectToList(currSelect.ToString());
    }
    //点击取消按钮
    public void OnButtonCancel()
    {
        if (UIQuestion.Instance.questionType == QuestionType.TypeSingle)
        {
            typeSelect.ResetOtherBtnSelcet();
        }
        IsSelect = false;

        btnSelect.image.color = colorNormal;

        UIQuestion.Instance.RemoveSelectFromList(currSelect.ToString());
    }
    //恢复按钮点击状态
    public void OnResetSelectState()
    {
        IsSelect = false;

        btnSelect.image.color = colorNormal;
    }
}
