using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using DarkRift;
using DarkRift.Client.Unity;
using UnityEngine;
using UnityEngine.UI;

public class IPSetting : MonoBehaviour
{
    [SerializeField] private InputField ipField;
    [SerializeField] private InputField portField;
    [SerializeField] private Button IpButton;
    [SerializeField] private UnityClient client;

    public string _ip;

    public int port;
    // Start is called before the first frame update
    void Start()
    {
        ipField.onEndEdit.AddListener(message => { _ip = message; });
        portField.onEndEdit.AddListener(message => {  port= Int32.Parse(message); });
        IpButton.onClick.AddListener(() => client.Connect(IPAddress.Parse(_ip), port, IPVersion.IPv4) );
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
