using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataReceiver
{
    byte[] _data;

    public enum EventType
    {
        None,

        Left,
        Center,
        Right,

        Tension
    }

    public struct EventInfo
    {
        public EventType type;
        public int value;
    }

    EventInfo _latest;

    public DataReceiver()
    {

    }

    public EventInfo GetInfo()
    {
        return _latest;
    }

    public void DetaUpdate(byte[] data)
    {
        _data = data;

        CheckUpdate();
    }

    void CheckUpdate()
    {
        if (_data[0] == 1)
        {
            _latest = ButtonCheck();
        }
        else if(_data[0] == 2)
        {
            _latest = TensionCheck();
        }
        else
        {
            _latest.type = EventType.None;
            _latest.value = 0;
        }

    }

    EventInfo ButtonCheck()
    {
        EventInfo info = new EventInfo();
        EventType type;

        switch (_data[3])
        {
            case 0:
                type = EventType.Center;
                break;
            case 1:
                type = EventType.Left;
                break;
            case 2:
                type = EventType.Right;
                break;
            default:
                type = EventType.None;
                break;
        }
        info.type = type;
        info.value = CalcValue(_data);
        Debug.Log("Value: " + info.value);

        return info;
    }

    EventInfo TensionCheck()
    {
        EventInfo info = new EventInfo();
        info.type = EventType.Tension;

        info.value = CalcValue(_data);
        return info;
    }

    int CalcValue(byte[] bytes)
    {
        string val = null;
        for (int i = 0; i < 4; i++)
        {
            val += bytes[4 + i];
        }
        int ans = int.Parse(val);

        return ans;
    }
}
