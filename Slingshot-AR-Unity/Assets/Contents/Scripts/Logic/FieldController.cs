using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldController : MonoBehaviour
{
    [SerializeField]
    GameObject fieldPrefab;

    [SerializeField]
    float fieldWidth = 2.0f;

    [SerializeField]
    float fieldDepth = 6.0f;

    public GameObject _field { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        _field = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        if(_field != null)
        {
            Destroy(_field);
        }
    }

    public void PlaceField(Pose pose)
    {
        if(_field == null)
        {
            _field = Instantiate(fieldPrefab, pose.position, pose.rotation, gameObject.transform);
            _field.transform.localScale = new Vector3(fieldWidth, 0.01f, fieldDepth);
        }
        else
        {
            _field.transform.position = pose.position;
        }

    }

}
