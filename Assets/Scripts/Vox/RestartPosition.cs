using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartPosition : MonoBehaviour
{
    Vector3 startPos;
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)){
            transform.position = startPos;
        }
    }
}
