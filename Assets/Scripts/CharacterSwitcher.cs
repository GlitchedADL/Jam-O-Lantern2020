using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class CharacterSwitcher : MonoBehaviour
{
    public GameObject[] characters = new GameObject[3];
    // Start is called before the first frame update
    void Start()
    {
        characters[0] = GameObject.Find("Zombie");
        characters[1] = GameObject.Find("Skeleton");
        characters[2] = GameObject.Find("Ghost");
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
    }

    void ToggleActiveChar(int i)
    {
        UnityEngine.Debug.Log(i);

        foreach(GameObject obj in characters)
        {
            if(System.Array.IndexOf(characters, obj) == i)
            {
                obj.GetComponent<PlayerMovement>().enabled = true;
                obj.transform.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                obj.GetComponent<PlayerMovement>().enabled = false;
                obj.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }
}
