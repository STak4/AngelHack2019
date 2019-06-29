using System;
using System.Collections;
using System.Collections.Generic;
using DarkRift;
using DarkRift.Client;
using DarkRift.Client.Unity;
using UnityEngine;

namespace kai
{
    public class LocalPlayer : MonoBehaviour
    {
        [SerializeField]
        UnityClient client;
        public Guid myGuid;
        // Start is called before the first frame update
        void Start()
        {
            myGuid = Guid.NewGuid();
            client.MessageReceived += OnMessageReceived;
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        void sendTransform()
        {
            using (DarkRiftWriter newPlayerWriter = DarkRiftWriter.Create())
            {
                newPlayerWriter.Write(myGuid.ToString());
                newPlayerWriter.Write(transform.position.x);
                using (Message newPlayerMessage = Message.Create(10, newPlayerWriter))
                {
                    
                    client.SendMessage(newPlayerMessage, SendMode.Reliable);
                }
            }
        }
    
        void OnMessageReceived(object sender, MessageReceivedEventArgs e)
        {
            //sendTransform();
        }
    }
}
