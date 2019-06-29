using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// [Utility]
/// Show debug.log message to logText.
/// </summary>
public class LogMessage : MonoBehaviour
{
    [SerializeField]
    Text logText;

    // Start is called before the first frame update
    void Awake()
    {
        Application.logMessageReceived += OnLogMessage;
        logText.text = "";
    }

    void OnDestroy()
    {
        Application.logMessageReceived -= OnLogMessage;
    }

    private void OnLogMessage(string i_logText, string i_stackTrace, LogType i_type)
    {
        if (string.IsNullOrEmpty(i_logText))
        {
            return;
        }

        if (!string.IsNullOrEmpty(i_stackTrace))
        {
            switch (i_type)
            {
                case LogType.Error:
                case LogType.Assert:
                case LogType.Exception:
                    i_logText += System.Environment.NewLine + i_stackTrace;
                    break;
                default:
                    break;
            }
        }

        switch (i_type)
        {
            case LogType.Log:
                i_logText = string.Format("<color=white>{0}</color>", i_logText);
                break;
            case LogType.Error:
            case LogType.Assert:
            case LogType.Exception:
                i_logText = string.Format("<color=red>{0}</color>", i_logText);
                break;
            case LogType.Warning:
                i_logText = string.Format("<color=yellow>{0}</color>", i_logText);
                break;
            default:
                break;
        }

        logText.text += i_logText + System.Environment.NewLine;

    }
}
