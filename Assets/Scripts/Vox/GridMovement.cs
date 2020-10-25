using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovement : MonoBehaviour
{
    [SerializeField] int gridSize = 1;
    [SerializeField] float moveDuration = 1.0f;
    float timeElapsed = 0f;
    float normalizedTime = 0f;
    [SerializeField] int gridX = 0;
    [SerializeField] int gridY = 0;
    [SerializeField] float playerScale = 0.5f;
    int gridXlast = 0;
    int gridYlast = 0;
    float slideX = 0;
    float slideY = 0;
    int hMov = 0;
    int vMov = 0;
    bool moving = false;
    Animator animator;
    void Start(){
        gridX = (int)Mathf.Round(transform.position.x/(float)gridSize);
        gridY = (int)Mathf.Round(transform.position.y/(float)gridSize);
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        //change grid position
        if (!moving){
            hMov = 0;
            vMov = 0;
            gridXlast = gridX;
            gridYlast = gridY;
            if (Input.GetKeyDown(KeyCode.D)){
                gridX++;
                hMov = 1;
                moving = true;
                transform.localScale = new Vector3(-playerScale,playerScale,playerScale);
            }
            if (Input.GetKeyDown(KeyCode.A)){
                gridX--;
                hMov = -1;
                moving = true;
                transform.localScale = new Vector3(playerScale,playerScale,playerScale);
            }
            if (Input.GetKeyDown(KeyCode.S)){
                gridY--;
                vMov = -1;
                moving = true;
            }
            if (Input.GetKeyDown(KeyCode.W)){
                gridY++;
                vMov = 1;
                moving = true;
            }
            animator.SetBool("moving",moving);
            timeElapsed = 0f;
            gridX = (int)Mathf.Clamp(gridX,0,14);
            gridY = (int)Mathf.Clamp(gridY,0,9);
        }
        
        //lerp to grid position
        if (moving){
            if (timeElapsed>moveDuration){
                moving = false;
                animator.SetBool("moving",false);
            } else {
                timeElapsed += Time.deltaTime;
                normalizedTime = timeElapsed / moveDuration;
                normalizedTime = Easing.Cubic.Out(normalizedTime);//Easing.Cubic.InOut(normalizedTime);
            }
            slideX = Mathf.Lerp(0,gridSize*hMov,normalizedTime);
            slideY = Mathf.Lerp(0,gridSize*vMov,normalizedTime);
            // lock sliding value to 10x15 grid
            if (Mathf.Abs(gridX-7)==7 && gridX==gridXlast){
                slideX = 0;
            }
            if (Mathf.Abs((float)gridY-4.5f)==4.5f && gridY==gridYlast){
                slideY = 0;
            }
            transform.position = new Vector3(gridXlast*gridSize+slideX,gridYlast*gridSize+slideY,0);
        }
    }
}
