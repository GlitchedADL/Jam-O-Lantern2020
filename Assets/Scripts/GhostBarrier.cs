using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBarrier : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Collide with ghost, ignore all other collisions
        if(collision.gameObject.layer != 8)
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>(), true);
        }
    }
}
