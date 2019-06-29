using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSequence
{
    /// <summary>
    /// onFinished will call when Finish sequence
    /// </summary>
    protected System.Action onFinished;

    /// <summary>
    /// Sequence prefab (contents of sequence)
    /// </summary>
    protected GameObject prefab;

    public virtual void StartSequence(System.Action callback, GameObject obj)
    {
        onFinished = callback;
        prefab = obj;
    }

    public virtual void EndSequence()
    {
        onFinished?.Invoke();
    }
}
