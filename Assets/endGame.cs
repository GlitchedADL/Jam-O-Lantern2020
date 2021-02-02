using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endGame : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] chars = new GameObject[3];
    private bool levelEnd;
    void Start()
    {
        chars[0] = GameObject.Find("zombie");
        chars[1] = GameObject.Find("skele");
        chars[2] = GameObject.Find("ghost");
    }

    // Update is called once per frame
    void Update()
    {
        levelEnd = true;
        foreach(GameObject charCheck in chars){
            if(!charCheck.GetComponent<GridMovement>().completed){
                levelEnd = false;
            }
        }
        if(levelEnd){
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
         
          
        }
    }
}
