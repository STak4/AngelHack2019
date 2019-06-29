using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    [SerializeField]
    SequenceManager sequenceManager;

    [SerializeField]
    Camera subCamera;

    public ARSessionOrigin sessionOrigin { get; set; }

    public ARSession arSession { get; set; }

    public WorldMapController wmController { get; set; }

    public Vector2 ScreenCenter { get; set; }

    public enum Mode
	{
        None,
        Single,
        Host,
        Remote,
        Spectator
	}


    public Mode _mode { get; set; }

    bool debugMode = false;

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
            Debug.LogError("Sequence Manager Not Found, Debug mode");
            debugMode = true;
        }
        ScreenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        Debug.Log("GameManager ScreenCenter: " + ScreenCenter);
    }

    // Update is called once per frame
    void Update()
    {

    }

    #region public methods
    public void EnableAR()
    {
        arSession.enabled = true;
        SwitchCamera();
        wmController._session = arSession;

        Debug.Log("GameManager: ARStarted");
    }

    public void DisableAR()
    {
        SwitchCamera();

        arSession.enabled = false;
        Debug.Log("GameManager: ARDisabled");
    }

    public void SaveMap()
    {
        //wmController.SaveStart()
    }
    #endregion

    #region private methods
    void SwitchCamera()
    {
        Debug.Log("Camera Change from: " + Camera.main.gameObject.name);
        // Here is a point of Change Camera.main
        Camera.main.gameObject.tag = "Sub Camera";

        if (sessionOrigin.gameObject.activeSelf)
        {
            sessionOrigin.gameObject.SetActive(false);
            subCamera.tag = "MainCamera";
        }
        else
        {
            sessionOrigin.gameObject.SetActive(true);
            sessionOrigin.camera.tag = "MainCamera";
        }

        Debug.Log("to: " + Camera.main.gameObject.name);
    }

    #endregion
}
