﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSwitching : MonoBehaviour
{
    //just a modified version of SwitchCharacters.cs so it works with the GridMovement.cs

    public GameObject[] characters = new GameObject[3];
    // Start is called before the first frame update
    void Start()
    {
        characters[0] = GameObject.Find("zombie");
        characters[1] = GameObject.Find("skele");
        characters[2] = GameObject.Find("ghost");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            ToggleActiveChar(0);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            ToggleActiveChar(1);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            ToggleActiveChar(2);
        }
        // reset character positions when pressed r
        if (Input.GetKeyDown(KeyCode.R)){
            foreach(GameObject obj in characters)
            {
                obj.GetComponent<GridMovement>().ResetPos();
            }
        }
    }

    void ToggleActiveChar(int i)
    {
        foreach(GameObject obj in characters)
        {
            if(System.Array.IndexOf(characters, obj) == i)
            {
                obj.GetComponent<GridMovement>().enabled = true;
                obj.transform.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                obj.GetComponent<GridMovement>().enabled = false;
                obj.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }
}
