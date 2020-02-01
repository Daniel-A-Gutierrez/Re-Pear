using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.CompilerServices;

public class Station : MonoBehaviour
{
    //ALL UNTESTED

    //assuming grid spaces are 1x1 unity unit (meter)
    //lets also assume for now, it cant get bigger than 16x16, and the center is 4 units. 

    //heres an idea, lets have it so you maintain a "highlight" square on each space of the grid.
    //if the player puts their cursor point over one that's a viable spot, it lights up. 

    //core is at 7,7 -> 8,8

    //or middle/2 and +1 in each dir

    GameObject highlight;
    GameObject[,] tiles;
    GameObject[,] highlights;

    [Range(3,24)]
    public int sideLength;

    void Awake()
    {
        tiles = new GameObject[16,16];
        tiles[16/2,16/2] = gameObject;
        tiles[16/2+1,16/2] = gameObject;
        tiles[16/2,16/2+1] = gameObject;
        tiles[16/2+1,16/2+1] = gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 TilemapPosToLocalPos(int x, int y)
    {
        return new Vector3( x - (float)(sideLength-1)/2 , y - (float)(sideLength-1)/2 , 0);
    }

    public Vector2Int WorldPosToTilemapPos(Vector3 pos)
    {
        if( Mathf.Abs(pos.x - transform.position.x) < (float)(sideLength)/2f &&
            Mathf.Abs(pos.y - transform.position.y) < (float)(sideLength)/2f)
        {
            return new Vector2Int(   (int)(pos.x - transform.position.x + (float)(sideLength)/2),
                                     (int)(pos.y - transform.position.y + (float)(sideLength)/2));
        }
        else
        {
            Debug.LogWarning("WorldPos not within tilemap");
           return new Vector2Int(-1,-1);
        }
    }

    ///<summary>PASS IN A PREFAB, NOT AN INSTANTIATED OBJECT.</summary>
    public void AddTile(int x, int y, GameObject tile)
    {
        if(PlacementAllowed(x,y))
        {
            tiles[x,y] = GameObject.Instantiate(tile,TilemapPosToLocalPos(x,y),Quaternion.identity,transform);
        }
    }

    public void RemoveTile(int x, int y)
    {
        if((x == 7 || x == 8) && (y==7 || y== 8))
        {
            return;
        }
        if(tiles[x,y] != null)
        {
            //kill the tile with its builtin coroutine

            //for now just destroy it.
            Destroy(tiles[x,y]);
            tiles[x,y] = null;

            //iterate over adjacent tiles to this position, revalidate them, if theyre not ok call remove tile
            if(tiles[x+1,y] && !ValidatePosition(x+1,y) )
                RemoveTile(x+1,y);
            if(tiles[x-1,y] && !ValidatePosition(x-1,y) )
                RemoveTile(x-1,y);
            if(tiles[x,y+1] && !ValidatePosition(x,y+1) )
                RemoveTile(x,y+1);
            if(tiles[x,y-1] && !ValidatePosition(x,y-1) )
                RemoveTile(x,y-1);

        }
    }

    //[MethodImplAttribute(MethodImplOptions.AggressiveInlining)] //inline shit for SPEED
    bool ValidatePosition(int x, int y)
    {
        return  (tiles[x,y+1] != null || tiles[x+1,y] != null || tiles[x-1,y] != null || tiles[x,y-1] != null);
    }

    ///<summary>Takes a Tilemap Position</summary>
    public bool PlacementAllowed(int x, int y)
    {
        if( tiles[x,y]==null && 
            (tiles[x+1,y] != null || tiles[x-1,y] != null ||tiles[x,y+1] != null ||tiles[x,y-1] != null ) )
            {
                return true;
            }
        return false;
    }
}
