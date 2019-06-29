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
        private int count;
        
        // Start is called before the first frame update

        void Start()
        {
            myGuid = Guid.NewGuid();
            client.MessageReceived += OnMessageReceived;
            var guid = myGuid.ToString();
            using (DarkRiftWriter newPlayerWriter = DarkRiftWriter.Create())
            {
                newPlayerWriter.Write(guid);
                var position = transform.position;
                var rotation = transform.rotation;
                newPlayerWriter.Write(position.x);
                newPlayerWriter.Write(position.y);
                newPlayerWriter.Write(position.z);
                newPlayerWriter.Write(rotation.x);
                newPlayerWriter.Write(rotation.y);
                newPlayerWriter.Write(rotation.z);
                newPlayerWriter.Write(rotation.w);
                using (Message newPlayerMessage = Message.Create(1, newPlayerWriter))
                {
                    client.SendMessage(newPlayerMessage, SendMode.Reliable);
                }
            }
            Debug.Log("Spawn");
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (count % 2 == 0)
            {
                var guid = myGuid.ToString();
                using (DarkRiftWriter newPlayerWriter = DarkRiftWriter.Create())
                {
                    newPlayerWriter.Write(guid);
                    var position = transform.position;
                    var rotation = transform.rotation;
                    newPlayerWriter.Write(position.x);
                    newPlayerWriter.Write(position.y);
                    newPlayerWriter.Write(position.z);
                    newPlayerWriter.Write(rotation.x);
                    newPlayerWriter.Write(rotation.y);
                    newPlayerWriter.Write(rotation.z);
                    newPlayerWriter.Write(rotation.w);
                    using (Message newPlayerMessage = Message.Create(2, newPlayerWriter))
                    {
                        client.SendMessage(newPlayerMessage, SendMode.Reliable);
                    }
                }

                //Debug.Log("Transform");
            }
        }

        void SendTransform()
        {
            using (DarkRiftWriter newPlayerWriter = DarkRiftWriter.Create())
            {
                newPlayerWriter.Write(myGuid.ToString());
                newPlayerWriter.Write(transform.position.x);
                using (Message newPlayerMessage = Message.Create(10, newPlayerWriter))
                {
                    
                    client.SendMessage(newPlayerMessage, SendMode.Unreliable);
                }
            }
        }
    
        void OnMessageReceived(object sender, MessageReceivedEventArgs e)
        {
            //sendTransform();
        }
    }
}
