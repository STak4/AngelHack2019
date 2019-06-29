using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalPlayer : MonoBehaviour
{
    ColorManager colorManager;
    PlayerBall playerBall;

    // Start is called before the first frame update
    void Start()
    {
        colorManager = new ColorManager(gameObject);
        playerBall = new PlayerBall();
    }

    // Update is called once per frame
    void Update()
    {

    }

    #region Session Call
    public void ShootBall(float tension)
    {
        //Camera.main.transform
    }

    public void SetBall(string tag)
    {

    }
    #endregion

    #region Server Call
    public void SetColor(byte r, byte g, byte b, byte a)
    {
        colorManager.SetColor(new Color32(r, g, b, a));
    }

    #endregion
}
