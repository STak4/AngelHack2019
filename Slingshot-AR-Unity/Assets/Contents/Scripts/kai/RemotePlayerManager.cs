using System;
using System.Collections;
using System.Collections.Generic;
using DarkRift.Client;
using DarkRift.Client.Unity;
using UnityEngine;

public class RemotePlayerManager : MonoBehaviour
{
    [SerializeField] private UnityClient client;
    [SerializeField] private GameObject prefab;
    public Dictionary<string, GameObject> remotePlayers = new Dictionary<string, GameObject>(); 
    // Start is called before the first frame update
    void Start()
    {
        client.MessageReceived += OnReceive;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnReceive(object sender, MessageReceivedEventArgs e)
    {
        var message = e.GetMessage();
        var reader = message.GetReader();
        string type = reader.ReadString();
        if (type != "Player")
        {
            Debug.Log("Not Player type");
            return;
        }
        string guid;
        float x, y, z;
        float qx, qy, qz, qw;
        if (e.GetMessage().Tag == 1)
        {
            guid = reader.ReadString();
            x = reader.ReadSingle();
            y = reader.ReadSingle();
            z = reader.ReadSingle();
            qx = reader.ReadSingle();
            qy = reader.ReadSingle();
            qz = reader.ReadSingle();
            qw = reader.ReadSingle();
            Debug.Log($"Spawn Player with Type: {type} Guid: {guid}\n" +
                      $"x: {x} y: {y} z: {z} qx:{qx} qy: {qy} qz: {qz} qw: {qw}");
            Spawn(guid, new Vector3(x, y, z), new Quaternion(qx, qy, qz, qw));
        } 
        else if (e.GetMessage().Tag == 2)
        {
            guid = reader.ReadString();
            x = reader.ReadSingle();
            y = reader.ReadSingle();
            z = reader.ReadSingle();
            qx = reader.ReadSingle();
            qy = reader.ReadSingle();
            qz = reader.ReadSingle();
            qw = reader.ReadSingle();
            Debug.Log($"Change Transform of Player with Type: {type} Guid: {guid} to\n" +
                      $"x: {x} y: {y} z: {z} qx:{qx} qy: {qy} qz: {qz} qw: {qw}");
            ChangeTransform(guid, new Vector3(x, y, z), new Quaternion(qx, qy, qz, qw));
        }

        else if (e.GetMessage().Tag == 3)
        {
            guid = reader.ReadString();
            Debug.Log($"Destroy Player with Type: {type} Guid: {guid}");
            DestroyPlayer(guid);
        }
    }

    void Spawn(string guid, Vector3 _position, Quaternion _rotation)
    {
        GameObject _gameObject;
        if (remotePlayers.TryGetValue(guid, out _gameObject))
        {
            Debug.Log($"Already Exist Player: {guid}");
        }
        else
        {
            var newRemotePlayer = Instantiate(prefab,new Vector3(_position.x,_position.y,_position.z), _rotation);
            remotePlayers.Add(guid, newRemotePlayer);
            Debug.Log($"Spawned: {guid}");
        }
    }

    void ChangeTransform(string guid, Vector3 _position, Quaternion _rotation)
    {
        GameObject _gameObject;
        if (remotePlayers.TryGetValue(guid, out _gameObject))
        {
            Debug.Log($"Find Guid: {guid}");
            _gameObject.transform.position = _position;
            _gameObject.transform.rotation = _rotation;
        }
        else
        {
            Debug.Log($"Not Found Guid: {guid}");
        }
    }

    void DestroyPlayer(string guid)
    {
        GameObject _gameObject;
        if (remotePlayers.TryGetValue(guid, out _gameObject))
        {
            Destroy(_gameObject);
            remotePlayers.Remove(guid);
            Debug.Log($"Destroyed Guid: {guid}");
        }
        else
        {
            Debug.Log($"Not Found Guid: {guid}");
        }
    }
}
