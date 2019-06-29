using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pairing : MonoBehaviour
{
    [SerializeField]
    BLEManager _ble;

    [SerializeField]
    Text statusText;

    System.Action _finished;

    bool _connect;

    public void Init(System.Action onFinished)
    {
        _connect = false;
        Debug.Log("Scene Start");
        _finished = onFinished;
        statusText.text = "Push Scan";
    }

    public void ScanButton()
    {
        statusText.text = "Scan starting...";
        Scan();
    }

    public void FinishButton()
    {
        if (_connect)
        {
            Finish();
        }
        else
        {
            statusText.text = "Not connect yet";
        }
    }

    public void NonBLE()
    {
        GameManager.Instance.nonBLE = true;
        Finish();
    }

    void Scan()
    {
        GameInputManager.Instance.BLEScan(OnConnected);
    }

    void OnConnected()
    {
        Debug.Log("Connected!");
        statusText.text = "Connected! Push Start";
        GameInputManager.Instance.BLESubscribe();
        _connect = true;
    }

    void Finish()
    {
        Debug.Log("Scene End");
        _finished?.Invoke();
    }
}
