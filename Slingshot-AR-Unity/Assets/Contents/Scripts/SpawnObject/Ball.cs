using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    GameObject plantPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
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
