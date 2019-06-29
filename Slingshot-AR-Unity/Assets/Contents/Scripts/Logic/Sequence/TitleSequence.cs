using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSequence : BaseSequence
{
    System.Action onCalibrated;
    Title _title;
    public override void StartSequence(System.Action callback, GameObject obj)
    {
        base.StartSequence(callback, obj);
        _title = obj.GetComponent<Title>();
        //_title.Init(EndSequence);
    }

    public override void EndSequence()
    {
        base.EndSequence();
    }

}
