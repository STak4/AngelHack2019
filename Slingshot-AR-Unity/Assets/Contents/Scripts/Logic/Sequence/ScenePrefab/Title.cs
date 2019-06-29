using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    System.Action _finished;

    public void Init(System.Action onFinished)
    {
        Debug.Log("Scene Start");
        _finished = onFinished;
    }

    public void FinishButton()
    {
        Finish();
    }

    void Finish()
    {
        Debug.Log("Scene End");
        _finished?.Invoke();
    }

}
