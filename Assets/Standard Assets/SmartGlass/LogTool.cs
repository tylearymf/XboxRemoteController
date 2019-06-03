
using System;
using UnityEngine;

public class LogTool
{
    static public void Log(string pMsg)
    {
        Debug.Log(pMsg);
    }
    static public void LogTrace(string pMsg, params object[] pParam)
    {
        Debug.LogFormat(pMsg, pParam);
    }

    static public void LogWarning(string pMsg)
    {
        Debug.LogWarning(pMsg);
    }

    static public void LogError(string pMsg)
    {
        Debug.LogError(pMsg);
    }
    static public void LogError(string pMsg, params object[] pParam)
    {
        Debug.LogErrorFormat(pMsg, pParam);
    }

    static public void LogError(Exception pMsg1, string pMsg2)
    {
        Debug.LogException(pMsg1);
        Debug.LogError(pMsg2);
    }

}