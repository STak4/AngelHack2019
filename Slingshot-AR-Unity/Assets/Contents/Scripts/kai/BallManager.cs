using System;
using System.Collections;
using System.Collections.Generic;
using DarkRift;
using DarkRift.Client.Unity;
using UnityEngine;
using UnityEngine.UI;

public class BallManager : MonoBehaviour
{
    [SerializeField] private Button shotButton;
    [SerializeField] private GameObject prefab;
    [SerializeField] private GameObject localPlayer;
    [SerializeField] private UnityClient client;
    public Dictionary<string, GameObject> Balls = new Dictionary<string, GameObject>(); 
    // Start is called before the first frame update
    void Start()
    {
        shotButton.onClick.AddListener(() =>
        {
            var _position = localPlayer.transform.position;
            var _rotation = localPlayer.transform.rotation;
            var newLocalBall = Instantiate(prefab,new Vector3(_position.x,_position.y,_position.z), _rotation);
            newLocalBall.GetComponent<kai.Ball>().isLocal = true;
            var force = Camera.main.transform.forward * 40f;
            newLocalBall.GetComponent<Rigidbody>().AddForce(force);
            var guid = Guid.NewGuid();
            Balls.Add(guid.ToString(), newLocalBall);
            using (DarkRiftWriter newPlayerWriter = DarkRiftWriter.Create())
            {
                newPlayerWriter.Write(guid.ToString());
                newPlayerWriter.Write(_position.x);
                newPlayerWriter.Write(_position.y);
                newPlayerWriter.Write(_position.z);
                newPlayerWriter.Write(force.x);
                newPlayerWriter.Write(force.y);
                newPlayerWriter.Write(force.z);
                using (Message newPlayerMessage = Message.Create(4, newPlayerWriter)) //tag4 is BallSpawn
                {
                    client.SendMessage(newPlayerMessage, SendMode.Reliable);
                }
            }
        });
        client.MessageReceived += (sender, e) =>
        {
            var message = e.GetMessage();
            var reader = message.GetReader();
            if (message.Tag == 4)
            {
                var guid = reader.ReadString();
                var x = reader.ReadSingle();
                var y = reader.ReadSingle();
                var z = reader.ReadSingle();
                var ax = reader.ReadSingle();
                var ay = reader.ReadSingle();
                var az = reader.ReadSingle();
                Debug.Log($"Spawn Ball with Guid: {guid}\n" +
                          $"x: {x} y: {y} z: {z} ax: {ax} ay: {ay} az: {az}");
                Spawn(guid, new Vector3(x, y, z), new Vector3(ax,ay,az));
            }
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void Spawn(string guid, Vector3 _position, Vector3 _force)
    {
        GameObject _gameObject;
        if (Balls.TryGetValue(guid, out _gameObject))
        {
            var newRemoteBall = Instantiate(prefab,new Vector3(_position.x,_position.y,_position.z), Quaternion.identity);
            newRemoteBall.GetComponent<Rigidbody>().AddForce(_force);
            Debug.Log($"Already Exist Ball: {guid}");
        }
        else
        {
            var newRemoteBall = Instantiate(prefab,new Vector3(_position.x,_position.y,_position.z), Quaternion.identity);
            newRemoteBall.GetComponent<Rigidbody>().AddForce(_force);
            Balls.Add(guid, newRemoteBall);
            Debug.Log($"Spawned Ball: {guid}");
        }
    }
}
