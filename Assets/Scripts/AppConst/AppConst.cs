using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 配置文件静态类
/// </summary>
public class AppConst
{
    // 标记不换行的空格（换行空格Unicode编码为/u0020，不换行的/u00A0）
    public const string BreakingSpace = "\u00A0\u00A0";
    //实验接口地址
    #region HttpConst
    //获取练习/考核记录 -- 根据获取到的练习记录id或考核记录id，查询详情 
    public const string GetPracticeById_Path = "Experiment/GetPracticeById";
    //保存实验步骤记录 -- 实验进行中保存每一步的操作数据，步骤开始和结束都需要调用
    public const string SaveStep_Path = "Experiment/AddStep";
    //保存实验临时值 -- 如果实验中需要存临时值，则调用该接口
    public const string SaveTempValue_Path = "Experiment/SaveTempValue";
    //保存实验报告 -- 保存实验中填写的实验报告数据
    public const string SaveExperReport_Path = "Experiment/SaveExperReport";
    //获取实验报告 -- 根据获取到的练习记录id或考核记录id查询实验报告
    public const string GetReportByStuCheckRecordId_Path = "Experiment/GetReportByStuCheckRecordId";
    //保存实验完成状态 -- 保存实验中填写的实验报告数据
    public const string SaveExperFinishState_Path = "Experiment/SaveExperFinishState";

    public const string OperateCheckId = "OperateCheckId";

    //public const string PracticeId = "PracticeId";

    public const string CreateUserId = "CreateUserId";

    //public const string UsedTime = "UsedTime";

    //public const string ExamDuration = "ExamDuration";

    public const string Code = "Code";

    //public const string StepId = "stepId";

    //public const string IsStart = "IsStart";

    public const string UserId = "UserId";
    public const string CreateUserName = "CreateUserName";
    public const string EClassId = "EClassId";
    public const string Instructor = "Instructor";
    public const string EName = "EName";
    public const string Purpose = "Purpose";
    public const string Principle = "Principle";
    public const string Objects = "Objects";
    public const string Quipment = "Quipment";
    public const string Reagent = "Reagent";
    public const string Step = "Step";
    public const string Result = "Result";
    public const string Discuss = "Discuss";
    public const string Conclusion = "conclusion";
    public const string MedicinePracticeId = "MedicinePracticeId";


    public const string GetOperateCheck_Path = "Experiment/GetOperateCheck";

    public const string CheckLoginState_Path = "Login/CheckLoginState/";
    //实验数据提交路径
    public const string AddTemValue_Path = "Experiment/AddTemValue";

    public const string Restart_Path = "Experiment/Restart";

    public const string AddStep_Path = "Experiment/AddStep";

    public const string GetPracticeStep_Path = "PracticeLog/GetPracticeStep";

    public const string GetPracticeUsedTime_Path = "Experiment/GetPracticeUsedTime";

    public const string SaveMedicineExperiment_Path = "Virtual/SaveMedicineExperiment";

    public const string GetStepData_Path = "api/MedicineExperimentsteps/GetDataList?";
    #endregion
    //实验属性常量
    #region HttpPropertyConst
    //请求头
    public static string Authorization = "Authorization";
    //token值
    public static string Token = "token";
    //实验练习id -当前项目使用 practiceid与pubtaskid一次操作只有一个值
    public static string PracticeId = "practiceid";
    //实验操作任务id -当前项目不适用此值 practiceid与pubtaskid一次操作只有一个值
    public static string PubtaskId = "pubtaskid";
    //考核记录id
    public static string CheckRecordId = "checkrecordid";
    //接口地址
    public static string URL = "url";
    //检查报告类型
    public static string Checkreporttype = "checkreporttype";
    //步骤号 如：1*0
    public const string StepId = "stepId";
    //步骤名称
    public const string StepName = "stepname";
    //步骤用时 秒
    public const string ExamDuration = "examduration";
    //步骤得分
    public const string StepScore = "stepscore";
    //步骤评价 - 非必传参数
    public const string StepComment = "stepcomment";
    //赋分模型 - 非必传参数
    public const string ScoreModel = "scoremodel";
    //是否开始   -- 0 步骤结束，1 步骤开始
    public const string IsStart = "isstart";
    //实验累计用时 秒
    public const string UsedTime = "usedtime";
    //合理用时 秒 - 非必传参数
    public const string ExperctTime = "expercttime";
    //步骤满分 - 非必传参数
    public const string MaxScore = "maxscore";
    #endregion
    //预制体路径存储
    #region Aseets Path
    public const string Res_Config = "Config/config";
    //配音文件路径
    public const string Res_AudioPeiYin = "Audios/Peiyin";

    public const string Res_MainCanvas = "Prefab/UI/MainCanvas";
    //主步骤Item
    public const string Res_MainStepItem = "Prefab/UI/MainStepItem";
    //子步骤Item
    public const string Res_SubStepItem = "Prefab/UI/SubStepItem";

    public const string Res_ToolsItem = "Prefab/UI/Item";

    public const string Res_PptView = "Prefab/UI/PptView";

    public const string Res_LookCamera = "Prefab/LookCamera";

    public const string Res_Panel = "Prefab/UI/Panel/";

    #endregion
}
