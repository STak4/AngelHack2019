using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

public class BLEManager : MonoBehaviour
{
    [SerializeField]
    string deviceToConnectTo = "MyESP32";
    [SerializeField]
    string _serviceUUID = "2225";
    [SerializeField]
    string _readCharacteristicUUID = "2226";
    [SerializeField]
    string _writeCharacteristicUUID = "2226";

    private string _FullUID = "0000****-0000-1000-8000-00805f9b34fb";     // BLE-CC41a module pattern 

    public bool isConnected = false;
    private bool _readFound = false;
    private bool _writeFound = false;
    private string _connectedID = null;

    private Dictionary<string, string> _peripheralList;
    private float _subscribingTimeout = 0f;

    private States _states = States.None;

    [SerializeField]
    UnityEvent stateChange;

    [SerializeField]
    UnityEvent buttonLeftDown, buttonRightDown, buttonCenter;

    [SerializeField]
    UnityEvent buttonLeftUp, buttonRightUp;

    [SerializeField]
    UnityEvent tensionUpdate;

    System.Action connectWait;

    private int _tension;

    public enum States
    {
        None,
        Scan,
        ScanRSSI,
        Connect,
        Subscribe,
        Unsubscribe,
        Disconnect,
    }


    public void BLEStart(System.Action onFinished)
    {
        StateChange(States.None);

        BluetoothLEHardwareInterface.Initialize(true, false, () => { onFinished?.Invoke(); },
                              (error) => { }
        );
    }

    public void ScanStart(System.Action onFinished)
    {
        // the first callback will only get called the first time this device is seen 
        // this is because it gets added to a list in the BluetoothDeviceScript 
        // after that only the second callback will get called and only if there is 
        // advertising data available 
        BluetoothLEHardwareInterface.Log("M-BLE: Scan Start");
        BluetoothLEHardwareInterface.ScanForPeripheralsWithServices(null, (address, name) => {
            AddPeripheral(name, address);
        }, (address, name, rssi, advertisingInfo) => { });
        StateChange(States.Scan);

        connectWait = onFinished;
    }

    public void SubscribeStart()
    {
        Debug.Log("Subscribe");
        StartCoroutine(Subscribe(1.0f));
    }

    public void Unsubscribe()
    {

    }

    public void Disconnect()
    {
        Debug.Log("Disconnect");
        _peripheralList = null;
        BluetoothLEHardwareInterface.DisconnectAll();
        StateChange(States.Disconnect);
    }

    public int GetTension()
    {
        return _tension;
    }

    public States GetStates()
    {
        return _states;
    }

    // Update is called once per frame 
    void Update()
    {

    }

    void sendDataBluetooth(string sData)
    {
        if (sData.Length > 0)
        {
            byte[] bytes = ASCIIEncoding.UTF8.GetBytes(sData);
            if (bytes.Length > 0)
            {
                sendBytesBluetooth(bytes);
            }
        }
    }

    void sendBytesBluetooth(byte[] data)
    {
        BluetoothLEHardwareInterface.Log(string.Format("data length: {0} uuid {1}", data.Length.ToString(), FullUUID(_writeCharacteristicUUID)));
        BluetoothLEHardwareInterface.WriteCharacteristic(_connectedID, FullUUID(_serviceUUID), FullUUID(_writeCharacteristicUUID),
           data, data.Length, true, (characteristicUUID) => {
               BluetoothLEHardwareInterface.Log("Write succeeded");
           }
        );
    }

    void AddPeripheral(string name, string address)
    {
        BluetoothLEHardwareInterface.Log("Found: " + name);

        if (_peripheralList == null)
        {
            _peripheralList = new Dictionary<string, string>();
        }
        if (!_peripheralList.ContainsKey(address))
        {
            _peripheralList[address] = name;
            if (name.Trim().ToLower() == deviceToConnectTo.Trim().ToLower())
            {
                BluetoothLEHardwareInterface.Log("Found device");
                BluetoothLEHardwareInterface.Log("Connecting to " + address);
                connectBluetooth(address);
            }
            else
            {
                BluetoothLEHardwareInterface.Log("Target device not founrd");
            }
        }
        else
        {
            BluetoothLEHardwareInterface.Log("No device found");
        }
    }

    void connectBluetooth(string addr)
    {
        BluetoothLEHardwareInterface.ConnectToPeripheral(addr, (address) => {
        },
           (address, serviceUUID) => {
           },
           (address, serviceUUID, characteristicUUID) => {

               // discovered characteristic 
               if (IsEqual(serviceUUID, _serviceUUID))
               {
                   _connectedID = address;
                   isConnected = true;

                   if (IsEqual(characteristicUUID, _readCharacteristicUUID))
                   {
                       _readFound = true;
                   }
                   if (IsEqual(characteristicUUID, _writeCharacteristicUUID))
                   {
                       _writeFound = true;
                   }

                   StateChange(States.Connect);

                   BluetoothLEHardwareInterface.Log("Connected. Stop scanning" + address);
                   BluetoothLEHardwareInterface.StopScan();
                   connectWait?.Invoke();
               }
           }, (address) => {

               // this will get called when the device disconnects 
               // be aware that this will also get called when the disconnect 
               // is called above. both methods get call for the same action 
               // this is for backwards compatibility 
               isConnected = false;
           });

    }


    void sendData(string s)
    {
        sendDataBluetooth(s);
    }

    void StateChange(States next)
    {
        _states = next;
        Debug.Log("Now BLE State: " + _states);
        stateChange.Invoke();
    }

    void TensionUpdate(int val)
    {
        _tension = val;
        tensionUpdate.Invoke();
    }

    IEnumerator Subscribe(float timeout)
    {
        yield return new WaitForSeconds(timeout);

        DataReceiver receiver = new DataReceiver();

        if(_states == States.Connect)
        {
            StateChange(States.Subscribe);
            BluetoothLEHardwareInterface.SubscribeCharacteristicWithDeviceAddress(
               _connectedID, FullUUID(_serviceUUID), FullUUID(_readCharacteristicUUID),
               (deviceAddress, notification) => {
               },
               (deviceAddress2, characteristic, data) => {
                   BluetoothLEHardwareInterface.Log("id: " + _connectedID);
                   if (deviceAddress2.CompareTo(_connectedID) == 0)
                   {
                       BluetoothLEHardwareInterface.Log(string.Format("data length: {0}", data.Length));
                       if (data.Length == 0)
                       {
                       // do nothing
                        }
                       else
                       {
                           BluetoothLEHardwareInterface.Log("Receive: " + data);

                           receiver.DetaUpdate(data);

                           DataReceiver.EventInfo info = receiver.GetInfo();

                           switch (info.type)
                           {
                               case DataReceiver.EventType.Center:
                                   if (info.value == 1)
                                   {
                                       Debug.Log("Select Button pushed");
                                       buttonCenter.Invoke();
                                   }
                                   break;

                               case DataReceiver.EventType.Left:
                                   if (info.value == 0)
                                   {
                                       Debug.Log("Left Button Released");
                                       buttonLeftUp.Invoke();
                                   }
                                   else if (info.value == 1)
                                   {
                                       Debug.Log("Left Button Pushed");
                                       buttonLeftDown.Invoke();
                                   }
                                   break;

                               case DataReceiver.EventType.Right:
                                   if (info.value == 0)
                                   {
                                       Debug.Log("Right Button Released");
                                       buttonRightDown.Invoke();
                                   }
                                   else if (info.value == 1)
                                   {
                                       Debug.Log("Right Button Pushed");
                                       buttonRightDown.Invoke();
                                   }
                                   break;


                               case DataReceiver.EventType.Tension:
                                   Debug.Log("Tension Update");
                                   TensionUpdate(info.value);
                                   break;

                               case DataReceiver.EventType.None:

                                   break;
                           }
                       }
                   }
               });
        }
        else
        {
            Debug.Log("Not Connecet yet");
        }
    }

    // ------------------------------------------------------- 
    // some helper functions for handling connection strings 
    // ------------------------------------------------------- 
    string FullUUID(string uuid)
    {
        return _FullUID.Replace("****", uuid);
    }

    bool IsEqual(string uuid1, string uuid2)
    {
        if (uuid1.Length == 4)
        {
            uuid1 = FullUUID(uuid1);
        }
        if (uuid2.Length == 4)
        {
            uuid2 = FullUUID(uuid2);
        }
        return (uuid1.ToUpper().CompareTo(uuid2.ToUpper()) == 0);
    }

}
