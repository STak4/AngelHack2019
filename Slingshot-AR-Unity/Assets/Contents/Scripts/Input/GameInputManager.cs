using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class GameInputManager : SingletonMonoBehaviour<GameInputManager>
{
    [SerializeField]
    BLEManager _ble;

    [SerializeField]
    ARRaycastManager _raycast;

    public Pose latestHit { get; set; }
    public UnityEvent Selected;
    public UnityEvent LeftDown, LeftUp;
    public UnityEvent RightDown, Rightup;

    bool bleStarted = false;
    bool bleInput = false;

    System.Action bleWait;
    System.Action tapWait;

    public enum InputState
    {
        None,
        Place,
        Await,
        Game
    }

    InputState inputState;
    // Start is called before the first frame update
    void Start()
    {
        inputState = InputState.None;
        BLEStart();
    }

    // Update is called once per frame
    void Update()
    {
        switch (inputState)
        {
            case InputState.Place:
                PlaceCheck();
                break;

            case InputState.Await:
                break;
            case InputState.Game:
                break;

            case InputState.None:
            default:
                break;
        }
    }

    #region public methods
    public void WaitPlace(System.Action onFirstPlace)
    {
        tapWait = onFirstPlace;
        inputState = InputState.Place;
    }

    public void WaitStart()
    {
        inputState = InputState.Await;
    }

    // For Non BLE mode
    public void SelectButton()
    {
        switch (inputState)
        {
            case InputState.Place:
                if (PlaceCheck())
                {
                    if (tapWait != null)
                    {
                        tapWait.Invoke();
                        tapWait = null;
                    }
                    Selected?.Invoke();
                }
                break;
            case InputState.Await:
                if (tapWait != null)
                {
                    tapWait.Invoke();
                    tapWait = null;
                }
                Selected?.Invoke();
                break;

            case InputState.Game:
                break;

            case InputState.None: 
            default:
                break;
        }
    }
    #endregion

    #region private methods

    bool PlaceCheck()
    {
        List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

        if (!TryGetTouchPosition(out Vector2 touchPosition))
            return false;

        // Screen Center is target position
        if (_raycast.Raycast(new Vector2(0,0), s_Hits, TrackableType.PlaneWithinPolygon))
        {
            // Raycast hits are sorted by distance, so the first one
            // will be the closest hit.
            var hitPose = s_Hits[0].pose;
            latestHit = hitPose;

            return true;
        }
        else
        {
            return false;
        }

    }

    #endregion

    #region BLE
    public void BLEScan(System.Action onFinished)
    {
        if (bleStarted)
        {
            Debug.Log("InputManager: BLE Connecting...");
            bleWait = onFinished;
            _ble.ScanStart(OnBLEConnect);
        }
        else
        {
            Debug.Log("InputManager: You must initialize BLE");
        }
    }

    public void BLESubscribe()
    {
        Debug.Log("BLE Subsribe");
        _ble.SubscribeStart();
    }

    public void BLEDisconnect()
    {
        _ble.Disconnect();
    }

    private void OnBLEConnect()
    {
        Debug.Log("InputManager: Connected");
        bleWait?.Invoke();
        bleInput = true;
    }

    void BLEStart()
    {
        _ble.BLEStart(BLEInitialized);
    }

    void BLEInitialized()
    {
        bleStarted = true;
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
    #endregion
}
