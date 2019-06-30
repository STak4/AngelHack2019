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
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_target != null)
        {
            Vector3.Lerp(gameObject.transform.position, _target.transform.position, 3.0f);
        }
    }

    public void Spawn(GameObject target)
    {
        Debug.Log("Spawn");
        _target = target;
        transform.LookAt(_target.transform);
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
