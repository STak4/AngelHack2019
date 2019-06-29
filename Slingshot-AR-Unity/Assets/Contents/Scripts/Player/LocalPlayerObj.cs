using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalPlayerObj : MonoBehaviour
{
    ColorManager colorManager;
    PlayerBall playerBall;

    [SerializeField]
    BallContainer ballContainer;

    // Start is called before the first frame update
    void Start()
    {
        ballContainer = GameObject.Find("BallContainer").GetComponent<BallContainer>();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = Camera.main.transform.position;
        gameObject.transform.rotation = Camera.main.transform.rotation;
    }

    #region Session Call
    public void InitStatus()
    {
        colorManager = new ColorManager(gameObject);
        playerBall = new PlayerBall();
    }

    public void ShootBall(float tension)
    {
        //スポーンはカメラ位置
        GameObject obj = Instantiate(ballContainer.GetBallPrefab(playerBall._selected._type), Camera.main.transform.position, Quaternion.identity);

        //カメラの前方向にrefObjのy座標を元に生成したオブジェクトに力を加える
        //obj.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * -refObj.transform.localPosition.y * 2000f);
        obj.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * 2000f);
    }

    public void SetNextBall()
    {
        playerBall.ChoiseNext();
    }

    public void SetBeforeBall()
    {
        playerBall.ChoiseBefore();
    }

    #endregion

    #region Server Call
    public void SetColor(byte r, byte g, byte b, byte a)
    {
        colorManager.SetColor(new Color32(r, g, b, a));
    }

    #endregion
}
