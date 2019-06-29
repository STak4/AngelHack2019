using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBall : MonoBehaviour
{
    /// <summary>
    /// スポーンするプレハブ
    /// </summary>
    [SerializeField]
    GameObject spawnPrefab;

    /// <summary>
    /// AddForceの際に参照するオブジェクト
    /// </summary>
    [SerializeField]
    GameObject refObj;

    public void Spawn()
    {
        //スポーンはカメラ位置
        GameObject obj = Instantiate(spawnPrefab, Camera.main.transform.position, Quaternion.identity);

        //カメラの前方向にrefObjのy座標を元に生成したオブジェクトに力を加える
        obj.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * -refObj.transform.localPosition.y * 2000f);
    }
}
