using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteSequence : BaseSequence {
    // Start is called before the first frame update
    System.Action onCalibrated;
    Host _host;

    public override void StartSequence(System.Action callback, GameObject obj)
    {
        base.StartSequence(callback, obj);
        _host = obj.GetComponent<Host>();
        _host.Init(EndSequence);
    }

    public override void EndSequence()
    {
        base.EndSequence();
    }
}
