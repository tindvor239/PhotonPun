using UnityEngine;
using Cysharp.Text;

public static class DebugExtend
{
    public static void Log(object message, Color color = default)
    {
        if (color == default)
        {
            color = Color.white;
        }
        string colorString = ColorUtility.ToHtmlStringRGBA(color);
        string formattedMessage = ZString.Format("<color=#{0}>{1}</color>", colorString, message);
        Debug.Log(formattedMessage);
    }
}