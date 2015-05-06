<%@ WebHandler Language="C#" Class="DianzhuApi" %>

using System;
using System.Web;

public class DianzhuApi : IHttpHandler
{

    //-----------------------试卷--------------------------
    /// <summary>
    /// 获取试卷列表
    /// </summary>
    /// <param name="conditions"></param>
    /// <returns></returns>
    private string GetPaperList(string conditions)
    {
        string result = string.Empty;

        return result;
    }
   
    /// <summary>
    /// 用户开始做一张试卷
    /// </summary>
    /// <param name="paperId"></param>
    /// <param name="userId"></param>
    /// <param name="timeBegin"></param>
    /// <returns></returns>
    private string StartExam(string paperId, string userId)
    {
        string result = string.Empty;

        return result;
    }
    /// <summary>
    /// 用户做完一题
    /// </summary>
    /// <param name="paperId">为空,则是单项练习</param>
    /// <param name="userId"></param>
    /// <param name="subjectId">题目Id</param>
    /// <param name="selectedItemId">用户选择的答案</param>
    /// <returns></returns>
    private string CompleteSubject(string paperId, string userId, string subjectId, string selectedItemId)
    {
        string result = string.Empty;
        return result;
    }
    /// <summary>
    /// 用户做完试卷
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="paperId"></param>
    /// <returns></returns>
    private string CompletePaper(string userId, string paperId)
    {
        string result = string.Empty;

        return result;
    }
    /// <summary>
    /// 获取题目列表(用于练习)
    /// </summary>
    /// <param name="conditions"></param>
    /// <returns></returns>
    private string GetSubjectList(string conditions)
    {
        string result = string.Empty;

        return result;
    }
    /// <summary>
    /// 获取考试历史记录
    /// </summary>
    /// <param name="userid"></param>
    /// <param name="conditions"></param>
    /// <returns></returns>
    private string GetExamRecordList(string userid, string conditions)
    {
        string result = string.Empty;

        return result;
    }
    /// <summary>
    /// 获取一条历史记录
    /// </summary>
    /// <param name="recordId"></param>
    /// <returns></returns>
    private string GetExamRecord(string recordId)
    {
        string result = string.Empty;

        return result;
    }
    //----------------会员-------------------
    /// <summary>
    /// 只判断用户名密码是否相符
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    private string UserLogin(string userId, string password)
    {
        string result = string.Empty;

        return result;
    }
    
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        context.Response.Write("Hello World");
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}