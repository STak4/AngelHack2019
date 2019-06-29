using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BLEReceiveTest : MonoBehaviour
{
    [SerializeField]
    BLEManager _ble;

    [SerializeField]
    Text t_states, t_type, t_data;

    // Start is called before the first frame update
    void Start()
    {
        _ble.BLEStart(OnStarted);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnStarted()
    {
        Debug.Log("BLE Initialized");
    }

    void OnScanned()
    {
        Debug.Log("Scan Finish(Connect)");
    }

    public void ScanStart()
    {
        _ble.ScanStart(OnScanned);
    }

    public void StateTextUpdate()
    {
        t_states.text = _ble.GetStates().ToString();
    }

    public void TensionUpdate()
    {
        t_data.text = _ble.GetTension().ToString();
    }
}
