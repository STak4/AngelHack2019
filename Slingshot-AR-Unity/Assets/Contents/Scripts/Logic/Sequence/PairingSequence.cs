using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PairingSequence : BaseSequence
{
    System.Action onCalibrated;
    Pairing _pairing;

    public override void StartSequence(System.Action callback, GameObject obj)
    {
        base.StartSequence(callback, obj);
        _pairing = obj.GetComponent<Pairing>();
        _pairing.Init(EndSequence);
    }

    public override void EndSequence()
    {
        base.EndSequence();
    }
}
