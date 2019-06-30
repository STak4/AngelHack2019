using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField]
    SessionManager sessionManager;

    [SerializeField]
    LocalPlayerObj localPlayer;

    bool onGame;

    // Start is called before the first frame update
    void Start()
    {
        onGame = false;
        sessionManager.SessionStart();
        sessionManager.OnStarted.AddListener(GameStart);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShootButton()
	{
        if (onGame)
        {
            localPlayer.ShootBall(1.0f);
        }
        else
        {

        }
	}

	public void LoadButton()
	{
		Load();
	}


    void GameStart()
    {
        Debug.Log("Add Listener Buttons");
        GameInputManager.Instance.RightDown.AddListener(PressRight);
        GameInputManager.Instance.RightDown.AddListener(PressLeft);
        onGame = true;
    }

    void PressLeft()
    {
        localPlayer.SetBeforeBall();
    }

    void PressRight()
    {
        localPlayer.SetNextBall();
    }

    // For Debug
    void Load()
	{
		Debug.Log("Load World Map");
        GameManager.Instance.LoadMap(OnLoaded);
	}

    void OnLoaded(bool success)
    {
        if (success)
        {
            Debug.Log("Load Success");
        }
        else
        {
            Debug.Log("Load Failed");
        }
    }
    
}
