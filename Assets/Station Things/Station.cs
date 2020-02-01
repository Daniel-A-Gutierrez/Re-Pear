using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.CompilerServices;

public class Station : MonoBehaviour
{
    //ALL UNTESTED
    //FOCUS ON TESTING THE FLOAT-INT INTERACTIONS AND PARSING, THOSE ARE DMB

    //assuming grid spaces are 1x1 unity unit (meter)
    //lets also assume for now, it cant get bigger than sideLengthxsideLength, and the center is 4 units. 

    //heres an idea, lets have it so you maintain a "highlight" square on each space of the grid.
    //if the player puts their cursor point over one that's a viable spot, it lights up. 

    //core is at 7,7 -> 8,8

    //or middle/2 and +1 in each dir

    public GameObject highlight;
    GameObject[,] tiles;
    GameObject[,] highlights;

    [Range(3,24)]
    public int sideLength;

    void Awake()
    {
        tiles = new GameObject[sideLength,sideLength];
        tiles[sideLength/2,sideLength/2] = gameObject;
        tiles[sideLength/2+1,sideLength/2] = gameObject;
        tiles[sideLength/2,sideLength/2+1] = gameObject;
        tiles[sideLength/2+1,sideLength/2+1] = gameObject;
        highlights = new GameObject[sideLength,sideLength];
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        if((x == sideLength/2 || x == sideLength/2+1) && (y==sideLength/2 || y== sideLength/2+1))
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
        if(x > sideLength-1 || y > sideLength -1 || x < 0 || y < 0)//out of bounds
            return false;
        if( tiles[x,y]==null && 
            (tiles[x+1,y] != null || tiles[x-1,y] != null ||tiles[x,y+1] != null ||tiles[x,y-1] != null ) )
            {
                return true;
            }
        return false;
    }





    public Vector3 TilemapPosToLocalPos(int x, int y)//this shit broke rn
    {
        return new Vector3( x - (float)(sideLength%2)/2 , y - (float)(sideLength%2)/2 , 0);
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

    public Vector2Int ArrayPosToTilemapPos(int x, int y)
    {
        //DO A BOUNDS CHECK HERE
        if(x > 0 && y > 0 && x < sideLength && y < sideLength)
            return new Vector2Int((int)(x-(float)sideLength/2), (int)(y-(float)sideLength/2));
        else
        {
            Debug.LogWarning("Array pos out of bounds");
            return new Vector2Int(10000,10000);
        }
    }

    public Vector2Int TilemapPosToArrayPos(int x, int y) // in progress
    {
        //DO A BOUNDS CHECK HERE
        if(x < -sideLength/2)
            return new Vector2Int(x +sideLength/2, y+sideLength/2);
        return Vector2Int.zero;
    }

    // void OnDrawGizmosSelected()
    // {
    //     Gizmos.color = Color.green;
    //     for(int i = -24 ; i < 25; i++)
    //     {
    //         for(int q = -24 ; q < 25 ; q++)
    //         {
    //             if(PlacementAllowed(i,q))
    //                 Gizmos.DrawSphere(TilemapPosToLocalPos(i,q) + transform.position , .1f);
    //         }
    //     }
    // }
}
