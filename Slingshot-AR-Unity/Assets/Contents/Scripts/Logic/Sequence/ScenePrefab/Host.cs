using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Host : MonoBehaviour
{
    [SerializeField]
    Text instText;

    [SerializeField]
    Button sendBtn;

    [SerializeField]
    FieldController fieldController;

    System.Action _finished;
    WorldMapController _wmc;

    bool waitMap;

    public void Init(System.Action onFinished)
    {
        Debug.Log("Scene Start");
        _finished = onFinished;
        sendBtn.interactable = false;
        StartAR();
    }

    public void SendButton()
    {
        Send();
    }

    public void DebugSelect()
    {
        GameInputManager.Instance.SelectButton();
    }


    void StartAR()
    {
        if(GameManager.Instance != null)
        {
            Debug.Log("AR Start");
            GameManager.Instance.EnableAR();
            instText.text = "Please Look Around";
            _wmc = GameManager.Instance.wmController;
            _wmc.CreateWorldMap(OnMapCreate);
        }
    }

    void OnMapCreate()
    {
        instText.text = "Place field on the floor by tap";
        GameInputManager.Instance.WaitPlace(OnPlaced);
        GameInputManager.Instance.Selected.AddListener(FieldUpdate);
    }

    void OnPlaced()
    {
        instText.text = "You can send ARWorldMap or adjust field";
        sendBtn.interactable = true;

    }

    void FieldUpdate()
    {
        StartCoroutine(DelayUpdate(0.1f));
    }

    void Send()
    {
        GameInputManager.Instance.Selected.RemoveListener(FieldUpdate);
        GameManager.Instance.SaveMap(SaveFinish);
        Finish();
    }

    void SaveFinish(bool success)
    {
        if (success)
        {
            instText.text = "Waiting connect player";
        }
        else
        {
            instText.text = "Save failed";
        }
    }

    void Finish()
    {
        Debug.Log("Scene End");
        _finished?.Invoke();
    }

    // delay Update
    IEnumerator DelayUpdate(float time)
    {
        yield return new WaitForSeconds(time);
        fieldController.PlaceField(GameInputManager.Instance.latestHit);
    }
}
