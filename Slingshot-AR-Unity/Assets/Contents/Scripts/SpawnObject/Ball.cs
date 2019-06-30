using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    GameObject plantPrefab;

    Rigidbody _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody>();   
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "ARPlane")
        {
            var obj = Instantiate(plantPrefab, gameObject.transform.position, gameObject.transform.rotation);

            // Send Server Message

            Destroy(gameObject);
        }
    }
}
