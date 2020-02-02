using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSelector : Interactable
{
    public Vector2Int tilemapPosition;
    public Station station;

    public void CreateTile(Vector2Int position, Station station)
    {
        this.station = station;
        tilemapPosition = position;
    }

    // Start is called before the first frame update
    void Start()
    {
        topickup = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Highlight(PlayerController p)
    {
        if(station.PlacementAllowed(tilemapPosition.x,tilemapPosition.y) && p.HasTileInInventory() )
        {
            GetComponent<LineRenderer>().enabled = true;
        }

    }

    public override void UnHighlight(PlayerController p)
    {
        GetComponent<LineRenderer>().enabled = false;
    }

    public override void Use(PlayerController player)
    {
        if(station.PlacementAllowed(tilemapPosition.x,tilemapPosition.y) && player.HasTileInInventory())
        {
            //get tile from player to spawn. 
            station.AddTile(tilemapPosition.x,tilemapPosition.y,player.GetTile());
            //i should get a corresponding tile
            //
            //GetComponent<LineRenderer>().enabled = true;
        }
    }
}
