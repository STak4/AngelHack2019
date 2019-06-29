using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Remote : MonoBehaviour
{
    [SerializeField]
    Text instText;

    [SerializeField]
    Button loadBtn;

    [SerializeField]
    Button startBtn;

    [SerializeField]
    FieldController fieldController;


    System.Action _finished;
    WorldMapController _wmc;

    bool waitMap;

    public void Init(System.Action onFinished)
    {
        Debug.Log("Scene Start");
        _finished = onFinished;
        startBtn.interactable = false;
        StartAR();
    }

    public void LoadButton()
    {
        Load();
    }

    public void DebugSelect()
    {
        GameInputManager.Instance.SelectButton();
    }


    void StartAR()
    {
        if (GameManager.Instance != null)
        {
            Debug.Log("AR Start");
            GameManager.Instance.EnableAR();
            instText.text = "Push Load Button";
            _wmc = GameManager.Instance.wmController;
            _wmc.FoundWorldMap(OnMapFound);
        }
    }

    void OnMapFound()
    {
        instText.text = "WorldMapFound! Push the Select Button";
        startBtn.interactable = true;
        GameInputManager.Instance.WaitStart();
        GameInputManager.Instance.Selected.AddListener(OnStarted);
        if(fieldController._field != null)
        {
            StartCoroutine(DelayPlace(0.1f));
        }

    }


    void OnStarted()
    {
        GameInputManager.Instance.Selected.RemoveListener(OnStarted);
        Finish();
    }

    void Load()
    {
        GameManager.Instance.LoadMap(OnLoaded);
        instText.text = "Loading...";
    }

    void OnLoaded(bool success)
    {
        if (success)
        {
            instText.text = "Load Success! Look around";
        }
        else
        {
            instText.text = "Load Failed";
        }
    }

    void Finish()
    {
        Debug.Log("Scene End");
        _finished?.Invoke();
    }

    // delay Update
    IEnumerator DelayPlace(float time)
    {
        yield return new WaitForSeconds(time);
        Instantiate(fieldController._field);
    }
}
