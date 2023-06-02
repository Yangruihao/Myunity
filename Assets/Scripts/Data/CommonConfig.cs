using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonConfig
{
}
/// <summary>
/// 结构解析，需要和配置文件CommonConfig.json同步更新结构
/// </summary>
[System.Serializable]
public class ItemList
{
    public List<string> stepSceneName;
    public List<StepOneLevel> lab_steps;
    public List<LabInfo> lab_info;
    public Url url;
}
/// <summary>
/// 实验步骤结构类
/// </summary>
[System.Serializable]
public class StepOneLevel
{
    /// <summary>
    /// 实验一级步骤名称
    /// </summary>
    public string step_name;
    /// <summary>
    /// 实验二级步骤名称
    /// </summary>
    public List<LabSubStep> children;
    public StepOneLevel(string name, List<LabSubStep> children)
    {
        step_name = name;
        this.children = children;
    }
}

[System.Serializable]
public class Url
{
    public string httpRequest;
    public string httpUrl;
    public string wapUrl;
}

/// <summary>
/// 实验信息内容结构类
/// </summary>
[System.Serializable]
public class LabInfo
{
    public string name;
    public string content;
    public LabInfo(string name, string content)
    {
        this.name = name;
        this.content = content;
    }
}

[System.Serializable]
public class LabSubStep
{
    public string sub_name;
}
[Serializable]
public class RowsItem
{
    /// <summary>
    /// 
    /// </summary>
    public string experimentstepid { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string createdate { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string createuser { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string description { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string experimentid { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string itemcode { get; set; }
    /// <summary>
    /// 1.信息核对与登记
    /// </summary>
    public string itemname { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string modifydate { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string modifyuser { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int ordernum { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string parentid { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string sortcode { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string stepnum { get; set; }
}
//步骤信息结构类
public class StepData
{
    /// <summary>
    /// 
    /// </summary>
    public bool success { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int code { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int total { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<RowsItem> rows { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string msg { get; set; }
}
//题目信息结构类
[System.Serializable]
public class RootItemQuestion
{
    public List<ItemQuestion> itemQuestions;
}
[System.Serializable]
public class ItemQuestion
{
    /// <summary>
    /// 题目内容
    /// </summary>
    public string strContent;
    /// <summary>
    /// 各项选择
    /// </summary>
    public string arraySelect;
    /// <summary>
    /// 正确选择
    /// </summary>
    public string arrayRightSelect;

    public ItemQuestion(string content, string selects, string rightSelect)
    {
        strContent = content;
        arraySelect = selects;
        arrayRightSelect = rightSelect;
    }
}

