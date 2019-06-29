using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowARCameraTransform : MonoBehaviour
{
    private Text arPositionText;
    private GameObject arCamera;
    // Start is called before the first frame update
    void Start()
    {
        arPositionText = GameObject.Find("ARPositionText").GetComponent<Text>();
        arCamera = GameObject.Find("AR Camera");
    }

    // Update is called once per frame
    void Update()
    {
        arPositionText.text = $"AR Camera Text:\n x: {arCamera.transform.position.x:F2} y: {arCamera.transform.position.y:F2} z: {arCamera.transform.position.z:F2}";
    }
}
