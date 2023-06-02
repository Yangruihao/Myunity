using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 数据控制类
/// </summary>
public class DataManager   // : Singleton<DataManager>
{
    /// <summary>
    /// 实验结束时间  new Date().toString();
    /// </summary>
    public static string EndTime;
    /// <summary>
    /// 每个小节用时（如果步骤有多个小步骤，则指的是小步骤，如果步骤只有一个步骤，则指的是大步骤） 
    /// </summary>
    public static int perStepTime;
    /// <summary>
    /// 当前用户信息
    /// </summary>
    public static User CurrUser;
    /// <summary>
    /// token值
    /// </summary>
    public static string Token = "";
    /// <summary>
    /// 当前的练习id
    /// </summary>
    public static string PracticeId = "";
    /// <summary>
    /// 实验操作任务id -当前项目不适用此值 practiceid与pubtaskid一次操作只有一个值
    /// </summary>
    public static string PubtaskId = "";

    private static int _UsedTime = 0; //用时

    /// <summary>
    /// 实验操作的总用时
    /// </summary>
    /// <param name="value"></param>
    public static int UsedTime
    {
        get { return _UsedTime; }
        set { _UsedTime = value; }
    }
    /// <summary>
    /// 网络请求地址
    /// </summary>
#if UNITY_EDITOR
    //public static string HttpRequest = "http://192.168.2.8:8008/";
    public static string HttpRequest = "";
#else
    public static string HttpRequest = "";
#endif
    /// <summary>
    /// 页面跳转地址
    /// </summary>
#if UNITY_EDITOR
    public static string HttpURL = "http://192.168.2.8:8010/";
#else
    public static string HttpURL = "";
#endif
    /// <summary>
    /// 移动端返回地址
    /// </summary>
#if UNITY_EDITOR
    public static string WapURL = "http://192.168.2.8:8009/";
#else
    public static string WapURL = "";
#endif
    public static string HttpStepURL = "http://192.168.2.8:8621";
    /// <summary>
    /// 检查报告类型
    /// </summary>
    public static string Checkreporttype = "";
    /// <summary>
    /// 根据不同的类型判断是在pc端还是移动端
    /// 1、2是PC端   3、4是手机端
    /// </summary>
    public static string urlType;
    public static string userId;
    /// <summary>
    /// 是虚拟实验还是实体实验
    /// true 虚拟实验   false 实体实验
    /// </summary>
    public static bool isSimulation;
    /// <summary>
    /// 考核模式：是否是考核  默认不是考核
    /// </summary>
    public static bool isExam = false;
    /// <summary>
    /// 考核模式：考核时间  从服务器获取的每次规定的考试时间
    /// </summary>
    public static int examTime = 30;
    /// <summary>
    /// 考核模式：实验步骤是否完成
    /// </summary>
    public static bool isSyStepComplete = false;
    /// <summary>
    /// 考核模式：考核是否完成，超时算没有完成
    /// </summary>
    public static bool isExamComplete = false;
    /// <summary>
    /// 考核模式：考核是否包括实验报告，如果包括实验报告，则必须提交实验报告才算考核结束，如果不包括实验报告，实验步骤完成就算考核结束
    /// </summary>
    public static bool isNeedBaogao = true;
    /// <summary>
    /// 所有步骤用时的总和，不包括停在页面不动的时间，是第1个小步骤用时+第二个小步骤用时+第三个小步骤用时累加的总和
    /// </summary>
    public static int totalTime = 0;

    ///// <summary>
    ///// 总用时，以秒为单位，同一个练习ID只记录一次总的用时 get
    ///// </summary>
    //public static int UsedTime()
    //{
    //    //int local = Number(LocalStorage.getItem('usedTime' + Data.PracticeId));
    //    //_UsedTime = local;
    //    return _UsedTime;
    //}
    ///// <summary>
    ///// 花费时长 set
    ///// </summary>
    ///// <param name="value"></param>
    //public static void UsedTime(int value)
    //{
    //    _UsedTime = value;
    //    //LocalStorage.setItem('usedTime' + Data.PracticeId, value.toString());
    //}
    ///// <summary>
    /// 实验步骤场景名称
    /// </summary>
    public static List<string> Lab_StepSceneName;
    /// <summary>
    /// 实验步骤列表
    /// </summary>
    public static List<StepOneLevel> Lab_Step;
    /// <summary>
    /// 实验信息列表
    /// </summary>
    public static List<LabInfo> Lab_Info;
    /// <summary>
    /// 实验问题列表
    /// </summary>
    public static List<ItemQuestion> itemQuestions;
    /// <summary>
    /// 当前一级步骤索引
    /// </summary>
    public static int StepIndex = 0;
    /// <summary>
    /// 当前二级步骤索引
    /// </summary>
    public static int SubStepsIndex = 0;
    /// <summary>
    /// 鼠标是否在打开的界面上
    /// </summary>
    public static bool InDialog;
    /// <summary>
    /// 实验是否初始化
    /// </summary>
    public static bool IsInit = false;
    /// <summary>
    /// 主界面
    /// </summary>
    public static bool isFirst=true;
    /// <summary>
    /// 场景返回步骤
    /// 用于主界面显示的窗口
    /// </summary>
    public static string sceneStep;

    /// <summary>
    /// 当前活跃场景
    /// </summary>
    public static string ActiveScene;

}
