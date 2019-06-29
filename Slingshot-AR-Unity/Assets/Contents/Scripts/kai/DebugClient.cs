using System;
using System.Collections;
using System.Collections.Generic;
using DarkRift;
using DarkRift.Client.Unity;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using UnityEngine.UI;

public class DebugClient : MonoBehaviour
{
    private Button spawnButton, transformButton, destroyButton;

    [SerializeField] private UnityClient client;

    [SerializeField] private GameObject localPlayer;
    // Start is called before the first frame update
    void Start()
    {
        spawnButton = GameObject.Find("SpawnButton").GetComponent<Button>();
        transformButton = GameObject.Find("TransformButton").GetComponent<Button>();
        destroyButton = GameObject.Find("DestroyButton").GetComponent<Button>();
        Assert.IsNotNull(spawnButton); Assert.IsNotNull(transformButton); Assert.IsNotNull(destroyButton);
        spawnButton.onClick.AddListener(() =>
        {
            var guid = localPlayer.GetComponent<kai.LocalPlayer>().myGuid.ToString();
            using (DarkRiftWriter newPlayerWriter = DarkRiftWriter.Create())
            {
                newPlayerWriter.Write(guid);
                var position = localPlayer.transform.position;
                var rotation = localPlayer.transform.rotation;
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
        });
        transformButton.onClick.AddListener(() =>
        {
            var guid = localPlayer.GetComponent<kai.LocalPlayer>().myGuid.ToString();
            using (DarkRiftWriter newPlayerWriter = DarkRiftWriter.Create())
            {
                newPlayerWriter.Write(guid);
                var position = localPlayer.transform.position;
                var rotation = localPlayer.transform.rotation;
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
            Debug.Log("Transform");
        });
        destroyButton.onClick.AddListener(() =>
        {
            var guid = localPlayer.GetComponent<kai.LocalPlayer>().myGuid.ToString();
            using (DarkRiftWriter newPlayerWriter = DarkRiftWriter.Create())
            {
                newPlayerWriter.Write(guid);
                var position = localPlayer.transform.position;
                using (Message newPlayerMessage = Message.Create(3, newPlayerWriter))
                {
                    client.SendMessage(newPlayerMessage, SendMode.Reliable);
                }
            }
            Debug.Log("Destroy");
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
