using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


public class Prototyping : MonoBehaviour
{
    [SerializeField]
    ARSessionOrigin _origin;

    FieldController _fc;
    ARRaycastManager _raycast;

    [SerializeField]
    ARSession _session;

    [SerializeField]
    FieldController fieldController;

    WorldMapController _wc;

    enum GameState
    {
        None,
        Standby,
        Place,
        Search,
        Game,
        Result
    }

    GameState _state;

    // Not AR Camera
    Camera _nonAR;

    #region MonoBehavior
    // Start is called before the first frame update
    void Start()
    {
        _fc = GameObject.Find("FieldController").GetComponent<FieldController>();

        if(_fc == null)
        {
            Debug.LogError("Not found");
        }
        else
        {
            Debug.Log("Wait push AR Start...");
        }
        _nonAR = Camera.main;
        StateChange(GameState.Standby);
    }

    // Update is called once per frame
    void Update()
    {
        if(_state == GameState.Place)
        {
            PlaceCheck();
        }
    }
    #endregion



    #region Button Action
    public void ARStart()
    {
        Debug.Log("AR Start Pushed");

        EnableAR();

    }

    public void AREnd()
    {
        Debug.Log("AR End Pushed");
        DisableAR();
    }

    public void ARReset()
    {
        Debug.Log("AR Reset Pushed");
        _session.Reset();
    }

    public void SendWorldMap()
    {
        if(_state == GameState.Place && _fc._field != null)
        {
            Debug.Log("Save Button Pushed");
            _wc.SaveStart(SaveCheck);
        }
        else
        {
            Debug.LogWarning("Save can do after field placed");
        }
    }

    public void LoadWorldMap()
    {
        if (_state == GameState.Place)
        {
            Debug.Log("Load Button Pushed");
            _wc.LoadStart(LoadCheck);
        }
        else
        {
            Debug.LogWarning("Load is unavailable now.");
        }
    }

    #endregion

    #region private methods
    void StateChange(GameState next)
    {
        Debug.Log("StateChange: " + next);
        _state = next;
    }

    bool PlaceCheck()
    {
        List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

        if (!TryGetTouchPosition(out Vector2 touchPosition))
            return false;

        if (_raycast.Raycast(touchPosition, s_Hits, TrackableType.PlaneWithinPolygon))
        {
            Debug.Log("Hit");
            // Raycast hits are sorted by distance, so the first one
            // will be the closest hit.
            var hitPose = s_Hits[0].pose;
            fieldController.PlaceField(hitPose);

            return true;
        }
        else
        {
            return false;
        }

    }

    void SaveCheck(bool sucess)
    {
        if (sucess)
        {
            Debug.Log("Save Success");
            StateChange(GameState.Game);
        }
        else
        {
            Debug.LogError("Save failed");
        }
    }

    void LoadCheck(bool sucess)
    {
        if (sucess)
        {
            Debug.Log("Load success");
            StateChange(GameState.Game);
        }
        else
        {
            Debug.LogError("Load failed");
        }
    }

    void EnableAR()
    {
        SwitchCamera();

        if (_raycast == null)
            _raycast = GameObject.Find("AR Session Origin").GetComponent<ARRaycastManager>();

        _session.enabled = true;
        _wc = new WorldMapController(_session);

        StateChange(GameState.Place);
        Debug.Log("ARStarted");
    }

    void DisableAR()
    {
        SwitchCamera();

        _session.enabled = false;
        _raycast = null;
        _wc = null;

        StateChange(GameState.Standby);
        Debug.Log("ARDisabled");
    }
    #endregion

    #region utils
    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
#if UNITY_EDITOR
        if (Input.GetMouseButton(0))
        {
            var mousePosition = Input.mousePosition;
            touchPosition = new Vector2(mousePosition.x, mousePosition.y);
            return true;
        }
#else
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }
#endif

        touchPosition = default;
        return false;
    }


    void SwitchCamera()
    {
        Debug.Log("Camera Change from: " + Camera.main.gameObject.name);
        // Here is a point of Change Camera.main
        Camera.main.gameObject.tag = "Sub Camera";

        if (_origin.gameObject.activeSelf)
        {
            _origin.gameObject.SetActive(false);
            _nonAR.tag = "MainCamera";
        }
        else
        {
            _origin.gameObject.SetActive(true);
            _origin.camera.tag = "MainCamera";
        }

        Debug.Log("to: " + Camera.main.gameObject.name);
    }
    #endregion

}


