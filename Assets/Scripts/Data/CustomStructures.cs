using System;
using System.Collections.Generic;



/// <summary>
/// 用户信息结构
/// </summary>
public class User
{
    private string _UserId;
    private string _LastLoginIp;
    private string _LastLoginTime;
    private string _RealName;
    private string _SchoolName;
    private string _ClassName;
    private string _Birthday;
    private string _Email;
    private string _HeadIcon;

    /// <summary>
    /// 用户类型
    /// </summary>
    public string UserType = "";
    /// <summary>
    /// 老师
    /// </summary>
    public const string UserType_Teacher = "isTeacher";
    /// <summary>
    /// 学生
    /// </summary>
    public const string UserType_Student = "isStudent";
    /// <summary>
    /// 超级管理员
    /// </summary>
    public const string UserType_Manager = "isManager";
    /// <summary>
    /// 访客
    /// </summary>
    public const string UserType_Customer = "isCustomer";
    /// <summary>
    /// 积分
    /// </summary>
    public string Integral;
    /// <summary>
    /// 经验值
    /// </summary>
    public string Experience;

    public User(Object userData)
    {
        //_UserId = userData.UserId;
        //_LastLoginIp = userData.LastLoginIp;
        //_LastLoginTime = userData.LastLoginTime;
        //_RealName = userData.RealName;
        //_SchoolName = userData.SchoolName;
        //_ClassName = userData.ClassName;
        //_Birthday = userData.Birthday;
        //_Email = userData.Email1;
        //_HeadIcon = userData.HeadIcon;
        //UserType = userData.UserType;
        //Integral = userData.Integral == undefined ? "" : userData.Integral;
        //Experience = userData.Experience == null ? "" : userData.Experience;
    }
    /// <summary>
    /// 用户ID
    /// </summary>
    /// <returns></returns>
    public string UserId()
    {
        return _UserId;
    }
    /// <summary>
    /// 用户最后一次登录时所用ip地址
    /// </summary>
    /// <returns></returns>
    public string LastLoginIp()
    {
        return _LastLoginIp;
    }
    /// <summary>
    /// 用户最后一次登录的时间
    /// </summary>
    /// <returns></returns>
    public string LastLoginTime()
    {
        return _LastLoginTime;
    }
    /// <summary>
    /// 昵称
    /// </summary>
    /// <returns></returns>
    public string RealName()
    {
        return _RealName;
    }
    /// <summary>
    /// 学校名称
    /// </summary>
    /// <returns></returns>
    public string SchoolName()
    {
        return _SchoolName;
    }
    /// <summary>
    /// 班级名称
    /// </summary>
    /// <returns></returns>
    public string ClassName()
    {
        return _ClassName;
    }
    /// <summary>
    /// 生日
    /// </summary>
    /// <returns></returns>
    public string Birthday()
    {
        return _Birthday;
    }
    /// <summary>
    /// 邮箱
    /// </summary>
    /// <returns></returns>
    public string Email()
    {
        return _Email;
    }
    /// <summary>
    /// 头像
    /// </summary>
    /// <returns></returns>
    public string HeadIcon()
    {
        return _HeadIcon;
    }
}