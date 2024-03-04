using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool isOccupied;
    public Color greenC;
    public Color redC;

    private SpriteRenderer rend;

    void Start(){
        rend = GetComponent<SpriteRenderer>();
    }

    void Update(){
        if(isOccupied){
            rend.color = redC;
        }
        else{
            rend.color = greenC;
        }
    }
}
