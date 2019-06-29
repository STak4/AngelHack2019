using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    [SerializeField]
    SequenceManager sequenceManager;

    public ARSessionOrigin sessionOrigin { get; set; }

    public ARSession arSession { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        if(sequenceManager != null)
        {
            Debug.Log("Game Start");
            // Ready SequeceManager
            sequenceManager.InitManager();
            // Start None Sequence
            sequenceManager.StartSequence();
        }
        else
        {
            Debug.LogError("You must set sequence Manager");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
