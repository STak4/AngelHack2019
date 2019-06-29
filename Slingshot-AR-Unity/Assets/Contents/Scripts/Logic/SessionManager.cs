using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionManager : MonoBehaviour
{
    bool onSession = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SessionStart()
    {
        onSession = true;

        // Get objects on GameStart

    }

    public void RequestHit(GameObject enemy)
    {

    }

    public void RequestShoot(PlayerBall.Ball ball, Pose pose, float power)
    {

    }

    public void RequestPlant(Pose pose)
    {

    }

    public void ReceiveHit(GameObject enemy)
    {

    }

    public void ReceiveShoot(PlayerBall.Ball ball, Pose pose, float power)
    {

    }

    public void ReceivePlant(Pose pose)
    {

    }

    public void ReceiveEnemySpawn(Pose pose, GameObject target)
    {

    }

   
}
