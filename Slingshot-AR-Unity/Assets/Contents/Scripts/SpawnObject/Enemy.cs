using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    GameObject _target;

    // Start is called before the first frame update
    void Start()
    {
        //Spawn(_target);
    }

    // Update is called once per frame
    void Update()
    {
        if(_target != null)
        {
        }
    }

    public void Spawn(GameObject target)
    {
        Debug.Log("Spawn");
        transform.LookAt(_target.transform);
        iTween.MoveTo(gameObject,iTween.Hash("x",0f,"y",0f,"z",0f,"easetype", iTween.EaseType.easeInQuad, "time", 3.0f));
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Delete the model, Play particle
        if(collision.gameObject.tag == "Ball")
        {
            Destroy(transform.GetChild(0).gameObject);

        }
    }

    IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(gameObject);
    }
}
