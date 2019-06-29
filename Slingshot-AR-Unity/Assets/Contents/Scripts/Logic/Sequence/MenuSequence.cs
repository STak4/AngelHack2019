using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSequence : BaseSequence
{
    System.Action onCalibrated;
    Menu _menu;

    public override void StartSequence(System.Action callback, GameObject obj)
    {
        base.StartSequence(callback, obj);
        _menu = obj.GetComponent<Menu>();
        _menu.Init(EndSequence);
    }

    public override void EndSequence()
    {
        base.EndSequence();
    }

}
