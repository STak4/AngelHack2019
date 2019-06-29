using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteSequence : BaseSequence {
    // Start is called before the first frame update
    System.Action onCalibrated;
    Remote _remote;

    public override void StartSequence(System.Action callback, GameObject obj)
    {
        base.StartSequence(callback, obj);
        _remote = obj.GetComponent<Remote>();
        _remote.Init(EndSequence);
    }

    public override void EndSequence()
    {
        base.EndSequence();
    }
}
