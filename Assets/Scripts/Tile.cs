using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool isOccupied;
    [SerializeField] private Color greenC;
    [SerializeField] private Color redC;

    public Building buildingHere;

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
