using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovement : MonoBehaviour
{
    #region INITIALIZE_VARS
    [SerializeField] bool charCanPush = true;
    [SerializeField] int gridSize = 1;
    [SerializeField] float moveDuration = 1.0f;
    float timeElapsed = 0f;
    float normalizedTime = 0f;
    [SerializeField] int gridX = 0;
    [SerializeField] int gridY = 0;
    [SerializeField] float playerScale = 0.5f;
    [SerializeField] Transform castPos;
    [SerializeField] bool ghost;
    Transform movingTomb = null;
    int gridXlast = 0;
    int gridYlast = 0;
    const int GRIDW = 14;
    const int GRIDH = 9;
    float slideX = 0;
    float slideY = 0;
    int hMov = 0;
    int vMov = 0;
    bool moving = false;
    int gridXstart;
    int gridYstart;
    Animator animator;
    #endregion
    void Start(){
        gridXstart = (int)Mathf.Round(transform.position.x/(float)gridSize);
        gridYstart = (int)Mathf.Round(transform.position.y/(float)gridSize);
        ResetPos();
        animator = GetComponent<Animator>();
    }
    public void ResetPos(){
        gridX = gridXstart;
        gridY = gridYstart;
        gridXlast = gridXstart;
        gridYlast = gridYstart;
    }
    // Update is called once per frame
    void Update()
    {
        
        //change grid position
        if (!moving){
            movingTomb = null;
            hMov = 0;
            vMov = 0;
            gridXlast = gridX;
            gridYlast = gridY;
            Debug.DrawLine(castPos.position,castPos.position+Vector3.right*gridSize*2,Color.red);
            Debug.DrawLine(castPos.position,castPos.position+Vector3.left*gridSize*2,Color.blue);
            Debug.DrawLine(castPos.position,castPos.position+Vector3.up*gridSize*2,Color.green);
            Debug.DrawLine(castPos.position,castPos.position+Vector3.down*gridSize*2,Color.magenta);
            //movingTomb transform is set in INPUTMOVE
            #region INPUTMOVE
            if (Input.GetKeyDown(KeyCode.D)){
                if (ghost){
                    RaycastHit2D[] Gray = Physics2D.LinecastAll(castPos.position,castPos.position+Vector3.right*gridSize,LayerMask.GetMask("GhostWall"));
                    if (Gray.Length==0){
                        GoRight();
                    }
                } else {
                    RaycastHit2D[] Rray = Physics2D.LinecastAll(castPos.position,castPos.position+Vector3.right*gridSize*2,LayerMask.GetMask("Gravestone"));
                    if (Rray.Length<2){
                        if (Rray.Length==1){
                            if (charCanPush){
                                // make sure player is 1 away from edge so they don't push tomb over gate
                                if (gridX<GRIDW-1){
                                    movingTomb = Rray[0].transform;
                                    GoRight();
                                }
                            } else {
                                // if char is not right next to tomb
                                if (Vector3.Distance(Rray[0].transform.position,castPos.position)>gridSize){
                                    GoRight();
                                }
                            }
                        } else { // no tomb
                            GoRight();
                        }
                    }
                }
                transform.localScale = new Vector3(-playerScale,playerScale,playerScale);
            } else if (Input.GetKeyDown(KeyCode.A)){
                if (ghost){
                    RaycastHit2D[] Gray = Physics2D.LinecastAll(castPos.position,castPos.position+Vector3.left*gridSize,LayerMask.GetMask("GhostWall"));
                    if (Gray.Length==0){
                        GoLeft();
                    }
                } else {
                    RaycastHit2D[] Rray = Physics2D.LinecastAll(castPos.position,castPos.position+Vector3.left*gridSize*2);
                    if (Rray.Length<2){
                        if (Rray.Length==1){
                            if (charCanPush){
                                // make sure player is 1 away from edge so they don't push tomb over gate
                                if (gridX>1 && charCanPush){
                                    movingTomb = Rray[0].transform;
                                    GoLeft();
                                }
                            } else {
                                // if char is not right next to tomb
                                if (Vector3.Distance(Rray[0].transform.position,castPos.position)>gridSize){
                                    GoLeft();
                                }
                            }
                        } else { // no tomb
                            GoLeft();
                        }
                    }
                }
                transform.localScale = new Vector3(playerScale,playerScale,playerScale);
            } else if (Input.GetKeyDown(KeyCode.S)){ // it do go down!! :V
                if (ghost){
                    RaycastHit2D[] Gray = Physics2D.LinecastAll(castPos.position,castPos.position+Vector3.down*gridSize,LayerMask.GetMask("GhostWall"));
                    if (Gray.Length==0){
                        GoDown();
                    }
                } else {
                    RaycastHit2D[] Rray = Physics2D.LinecastAll(castPos.position,castPos.position+Vector3.down*gridSize*2);
                    if (Rray.Length<2){
                        if (Rray.Length==1){
                            if (charCanPush){
                                // make sure player is 1 away from edge so they don't push tomb over gate
                                if (gridY>1 && charCanPush){
                                    movingTomb = Rray[0].transform;
                                    GoDown();
                                }
                            } else {
                                // if char is not right next to tomb
                                if (Vector3.Distance(Rray[0].transform.position,castPos.position)>gridSize){
                                    GoDown();
                                }
                            }
                        } else { // no tomb
                            GoDown();
                        }
                    }
                }
            } else if (Input.GetKeyDown(KeyCode.W)){
                if (ghost){
                    RaycastHit2D[] Gray = Physics2D.LinecastAll(castPos.position,castPos.position+Vector3.up*gridSize,LayerMask.GetMask("GhostWall"));
                    if (Gray.Length==0){
                        GoUp();
                    }
                } else {
                    RaycastHit2D[] Rray = Physics2D.LinecastAll(castPos.position,castPos.position+Vector3.up*gridSize*2);
                    if (Rray.Length<2){
                        if (Rray.Length==1){
                            if (charCanPush){
                                // make sure player is 1 away from edge so they don't push tomb over gate
                                if (gridY<GRIDH-1 && charCanPush){
                                    movingTomb = Rray[0].transform;
                                    GoUp();
                                }
                            } else {
                                // if char is not right next to tomb
                                if (Vector3.Distance(Rray[0].transform.position,castPos.position)>gridSize){
                                    GoUp();
                                }
                            }
                        } else { // no tomb
                            GoUp();
                        }
                    }
                }
            }
            #endregion

            animator.SetBool("moving",moving);
            timeElapsed = 0f;
            gridX = (int)Mathf.Clamp(gridX,0,GRIDW);
            gridY = (int)Mathf.Clamp(gridY,0,GRIDH);
        }
        
        //lerp to grid position
        if (moving){
            if (timeElapsed>moveDuration){
                moving = false;
                animator.SetBool("moving",false);
            } else {
                timeElapsed += Time.deltaTime;
                normalizedTime = timeElapsed / moveDuration;
                animator.SetFloat("normalizedTime",normalizedTime);
                animator.SetFloat("multiplier",1/moveDuration);
                normalizedTime = Easing.Cubic.Out(normalizedTime);
            }
            slideX = Mathf.Lerp(0,gridSize*hMov,normalizedTime);
            slideY = Mathf.Lerp(0,gridSize*vMov,normalizedTime);
            // lock sliding value to 10x15 grid
            if (Mathf.Abs(gridX-GRIDW/2)==GRIDW/2 && gridX==gridXlast){
                slideX = 0;
            }
            if (Mathf.Abs((float)gridY-(float)GRIDH/2)==(float)GRIDH/2 && gridY==gridYlast){
                slideY = 0;
            }
            transform.position = new Vector3(gridXlast*gridSize+slideX,gridYlast*gridSize+slideY,0);
            if (movingTomb!=null){
                if (Vector3.Distance(movingTomb.position,castPos.position)<=gridSize){
                    movingTomb.position = castPos.position + (Vector3.right*hMov + Vector3.up*vMov)*gridSize;
                }
            }
        }
    }
    void GoRight(){
        gridX++;
        hMov = 1;
        moving = true;
    }
    void GoLeft(){
        gridX--;
        hMov = -1;
        moving = true;
    }
    void GoUp(){
        gridY++;
        vMov = 1;
        moving = true;
    }
    void GoDown(){
        gridY--;
        vMov = -1;
        moving = true;
    }
}
