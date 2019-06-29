using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager
{
    Material _mat;

    public ColorManager(GameObject target)
    {
        _mat = target.GetComponent<Material>();
    }

    public void SetColor(Color32 color)
    {
        _mat.color = color;
    }
}
