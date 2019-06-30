using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodAnim : MonoBehaviour
{
    [SerializeField]
    GameObject _hole;

    [SerializeField]
    GameObject _wood;

    Material _mat;
    void Start()
    {
        _mat = _wood.GetComponent<Renderer>().material;
        _mat.SetFloat("_Press",0);
    }


    void Update()
    {
        iTween.ScaleTo(_hole, iTween.Hash("x", 0f, "y", 0f, "z", 0f, "easetype", iTween.EaseType.easeInQuint, "time", 2f, "isLocal", true));
        _mat.SetFloat("_Press", Mathf.Lerp(0,1,Time.time * 0.5f));
    }
}
