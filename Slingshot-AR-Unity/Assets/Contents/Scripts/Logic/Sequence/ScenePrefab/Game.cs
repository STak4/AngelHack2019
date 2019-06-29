using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField]
    SessionManager sessionManager;

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
            Shoot(1.0f);
        }
	}

	public void LoadButton()
	{
		Load();
	}

    void Shoot(float power)
    {
        //sessionManager.RequestShoot()
    }

    void GameStart()
    {

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
