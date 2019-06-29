using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField]
    GameObject mainMenu;

    [SerializeField]
    GameObject subMenu;

    System.Action _finished;

    public void Init(System.Action onFinished)
    {
        Debug.Log("Scene Start");
        _finished = onFinished;
        mainMenu.SetActive(true);
    }

    public void SingleButton()
    {
        SingleMode();
    }

    public void MultiButton()
    {
        mainMenu.SetActive(false);
        subMenu.SetActive(true);
    }

    public void BackButton()
    {
        Back();
    }

    public void HostButton()
    {
        Host();
    }

    public void RemoteButton()
    {
        Remote();
    }

    void Back()
    {
        mainMenu.SetActive(true);
        subMenu.SetActive(false);
    }

    void SingleMode()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance._mode = GameManager.Mode.Single;
        }
        Debug.Log("Single Mode Set");
        Finish();
    }

    void MultiMode()
    {
        Debug.Log("Multi Mode Select");
    }

    void Host()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance._mode = GameManager.Mode.Host;
        }
        Finish();
    }

    void Remote()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance._mode = GameManager.Mode.Remote;
        }
        Finish();
    }

    void Finish()
    {
        Debug.Log("Scene End");
        _finished?.Invoke();
    }
}
