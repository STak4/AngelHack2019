using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SessionManager : MonoBehaviour
{
    [SerializeField]
    Text countDownText;

    [SerializeField]
    GameObject timerPanel;

    [SerializeField]
    Text timerText;

    [SerializeField]
    float timeLimit = 60f;

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
        if (onSession)
        {
            timeLimit -= Time.deltaTime;
            if(timeLimit <= 0f)
            {
                Debug.Log("Game Finished!");
                OnFinished?.Invoke();
            }
        }
    }

    public void SessionStart()
    {
        // Get objects on GameStart


        StartCoroutine(CoundDown());
    }

    public void RequestHit(GameObject enemy)
    {

    }

    public void RequestShoot(BallContainer.BallType type, Pose pose, float power)
    {

    }

    public void RequestPlant(Pose pose)
    {

    }

    public void ReceiveHit(GameObject enemy)
    {

    }

    public void ReceiveShoot(BallContainer.BallType type, Pose pose, float power)
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
        countDownText.gameObject.SetActive(true);
        Debug.Log("Game Will be started...");
        for (int i = 0; i < 3; i++)
        {
            countDownText.text = (3 - i).ToString();
            yield return new WaitForSeconds(1.0f);
        }
        countDownText.gameObject.SetActive(false);
        timerPanel.SetActive(true);
        // Game Start
        onSession = true;
        OnStarted?.Invoke();
    }
   
}
