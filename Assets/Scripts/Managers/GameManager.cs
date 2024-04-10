using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int gold;
    public Text goldDisplay;
    public GameObject grid;
    public CustomCursor CC;
    public Tile[] tiles;

    private Building bAColocar;

    private int goldRate = 0;
    private float goldTimer = 1.0f;
    private float timer = 0.0f;

    public int SumGoldRate
    {
        get { return goldRate; }
        set { goldRate += value; }
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Update(){
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            gold += goldRate;
            timer = goldTimer;
        }

        goldDisplay.text = gold.ToString();

        if(Input.GetMouseButtonDown(0) && bAColocar != null){
            Tile nearestTile = null;
            float shortestDistance = 100;
            foreach(Tile tile in tiles){
                float dist = Vector2.Distance(tile.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));

                if(dist < shortestDistance){
                    shortestDistance = dist;
                    nearestTile = tile;
                }
            }

            if (!nearestTile.isOccupied)
            {
                Instantiate(bAColocar, nearestTile.transform.position, Quaternion.identity);
                nearestTile.isOccupied = true;
                grid.SetActive(false);
                CC.gameObject.SetActive(false);
                Cursor.visible = true;
                bAColocar = null;
            }
        }
    }

    public void BuyBuilding(Building b){
        if (gold >= b.cost){
            GameObject Edificio = b.gameObject;
            CC.gameObject.SetActive(true);
            CC.setCursor(Edificio.GetComponent<SpriteRenderer>());
            Cursor.visible = false;
            gold -= b.cost;
            grid.SetActive(true);
            bAColocar = b;
        }
            
    }
}
