using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandSprite : MonoBehaviour
{
    [SerializeField]
    Sprite[] rSprite;
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = rSprite[Random.Range(0,rSprite.Length)];
        GetComponent<SpriteRenderer>().flipX = (Random.value < 0.5f);
    }

}
