using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SessionManager : MonoBehaviour
{
    [SerializeField]
    GameObject countDownPanel;

    bool onSession = false;

    public UnityEvent OnStarted;
    public UnityEvent OnFinished;

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

    IEnumerator CoundDown()
    {
        countDownPanel.SetActive(true);
        for(int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(1.0f);
        }
        countDownPanel.SetActive(false);
        // Game Start
        onSession = true;
        OnStarted?.Invoke();
    }
   
}
