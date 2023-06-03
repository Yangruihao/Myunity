# if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.UI;
public class ChangeFontWindow : EditorWindow
{
    [MenuItem("Tools/������")]
    public static void Open()
    {
        /*��һ�������������ͣ��������ڲ����߼�
         * �ڶ�������ȷ���Ƿ�Ϊ�������ڣ�ѡ��false����ͣ��Ч��
         * ���������� ��ʾ���ڵı���
         * ���ĸ� Ŀǰ��֪��ʲô��˼
         */
        EditorWindow.GetWindow(typeof(ChangeFontWindow), true);
    }

    //public Font toChange = new Font("Arial");
    public Font toChange;
    static Font toChangeFont;

    void OnGUI()
    {
        toChange = (Font)EditorGUILayout.ObjectField("ѡ����Ҫ����������", toChange, typeof(Font), true, GUILayout.MinWidth(100));
        toChangeFont = toChange;
        if (GUILayout.Button("ȷ�ϸ���"))
        {
            Change();
        }
    }

    public static void Change()
    {
        Object[] Texts = Selection.GetFiltered(typeof(Text), SelectionMode.Deep);
        foreach (Object text in Texts)
        {
            if (text)
            {
                //�����UGUI��UILabel����Text�Ϳ���  
                Text TempText = (Text)text;
                Undo.RecordObject(TempText, TempText.gameObject.name);
                TempText.font = toChangeFont;
                Debug.Log(text.name + ":" + TempText.text);
                EditorUtility.SetDirty(TempText);
            }
        }
    }
}
#endif
