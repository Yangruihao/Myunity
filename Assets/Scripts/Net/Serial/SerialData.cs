/**
 * 暂时没啥用，测试 unity 数据请求。具体实验需要根据后台数据返回重写
 */ 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ListClass
{
    public string status;
    public DataList data;
}

[System.Serializable]
public class DataList
{
    public string name;
    public string version;
    public LinkClass link;
}

[System.Serializable]
public class LinkClass
{
    public string cn;
    public string en;
}