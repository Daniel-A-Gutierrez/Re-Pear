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
    public GameObject[,] tiles;
    GameObject[,] highlights;
    public GameObject debugSphere;
    ///<summary> 8 blocks that wrap around the core clockwise from noon </summary>
    public GameObject[] startingTiles;

    [Range(3,24)]
    public int halfSideLength;

    void Awake()
    {
        tiles = new GameObject[halfSideLength*2,halfSideLength*2];
        tiles[halfSideLength,halfSideLength] = gameObject;
        tiles[halfSideLength-1,halfSideLength] = gameObject;
        tiles[halfSideLength,halfSideLength-1] = gameObject;
        tiles[halfSideLength-1,halfSideLength-1] = gameObject;
        highlights = new GameObject[halfSideLength*2,halfSideLength*2];
    }

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0 ; i  <halfSideLength*2 ; i++)
        {
            for(int q = 0 ; q < halfSideLength*2; q++)
            {
                if(tiles[i,q] != null)
                {
                    tiles[i,q].transform.parent = transform;
                    Vector2Int tilePos = ArrayPosToTilemapPos(i,q) ;
                
                    tiles[i,q].transform.localPosition = TilemapPosToLocalPos(tilePos.x,tilePos.y);
                } 
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Gizmos.color = Color.green;
        for(int i = -3 ; i < 3; i++)
        {
            for(int q = -3 ; q < 3 ; q++)
            {
                if(PlacementAllowed(i,q))
                    Instantiate(debugSphere,TilemapPosToLocalPos(i,q) + transform.position, Quaternion.identity);
                    //Gizmos.DrawSphere(TilemapPosToLocalPos(i,q) + transform.position , .1f);
            }
        }
    }



    ///<summary>PASS IN A PREFAB, NOT AN INSTANTIATED OBJECT. Use tilemap pos x and y </summary>
    public void AddTile(int x, int y, GameObject tile)
    {
        if(PlacementAllowed(x,y))
        {
            // tiles[x,y] = GameObject.Instantiate(tile,TilemapPosToLocalPos(x,y),Quaternion.identity,transform);
        }
    }
    ///<summary> Use tilemap Position x and y</summary>
    public void RemoveTile(int x, int y)
    {
        if((x == halfSideLength || x == halfSideLength-1) && (y==halfSideLength || y== halfSideLength-1))
        {
            return;
        }
        if(tiles[x,y] != null)
        {
            //kill the tile with its builtin coroutine
            Vector2Int arrayPos = TilemapPosToArrayPos(x,y);
            //for now just destroy it.
            Destroy(tiles[arrayPos.x,arrayPos.y]);
            tiles[arrayPos.x,arrayPos.y] = null;

            //iterate over adjacent tiles to this position, revalidate them, if theyre not ok call remove tile
            if(tiles[x+1,y] && !AdjacentOccupied(x+1,y) )
                RemoveTile(x+1,y);
            if(tiles[x-1,y] && !AdjacentOccupied(x-1,y) )
                RemoveTile(x-1,y);
            if(tiles[x,y+1] && !AdjacentOccupied(x,y+1) )
                RemoveTile(x,y+1);
            if(tiles[x,y-1] && !AdjacentOccupied(x,y-1) )
                RemoveTile(x,y-1);

        }
    }

    //[MethodImplAttribute(MethodImplOptions.AggressiveInlining)] //inline shit for SPEED
    ///<summary> takes a tilemap position </summary>
    bool AdjacentOccupied(int x, int y)//this would be complicated to get correct, A* to find core through adjacent tiles. 
    {
        bool neighbor = false;
        Vector2Int arrayPos = TilemapPosToArrayPos(x,y);
        x = arrayPos.x;
        y = arrayPos.y;
        try
        {
            neighbor |= (tiles[x,y+1] != null);
        }
        catch
        {}

        try
        {
            neighbor |= (tiles[x,y-1] != null);
        }
        catch
        {}  

        try
        {
            neighbor |= (tiles[x+1,y] != null);
        }
        catch
        {} 

        try
        {
            neighbor |= (tiles[x-1,y] != null);
        }
        catch
        {} 

        return  neighbor;
    }



    ///<summary>Takes a Tilemap Position</summary>
    public bool PlacementAllowed(int x, int y)
    {
        if(x == 2 && y == 2)
        {
            print(x);
        }
        Vector2Int arrayPos = TilemapPosToArrayPos(x,y);
        if(arrayPos.x >= halfSideLength*2 || arrayPos.y >= halfSideLength*2 || arrayPos.x < 0 || arrayPos.y < 0)//out of bounds
            return false;
        if( tiles[arrayPos.x,arrayPos.y]==null && AdjacentOccupied(x,y)) 
        {
            return true;
        }
        return false;
    }



///////////////////////


    public Vector3 TilemapPosToLocalPos(int x, int y)//this shit broke rn
    {
        return new Vector3( x +.5f , y + .5f , 0);
    }

    public Vector2Int WorldPosToTilemapPos(Vector3 pos)
    {
        if( Mathf.Abs(pos.x - transform.position.x) < (halfSideLength) &&
            Mathf.Abs(pos.y - transform.position.y) < (halfSideLength))
        {
            return new Vector2Int(   Mathf.FloorToInt((pos.x - transform.position.x )),
                                     Mathf.FloorToInt((pos.y - transform.position.y )));
        }
        else
        {
            Debug.LogWarning("WorldPos not within tilemap");
           return new Vector2Int(0,0);
        }
    }

    public Vector2Int ArrayPosToTilemapPos(int x, int y)
    {
        //DO A BOUNDS CHECK HERE
        if(x > 0 && y > 0 && x < halfSideLength*2 && y < halfSideLength*2)
            return new Vector2Int(x-halfSideLength, y-halfSideLength);
        else
        {
            Debug.LogWarning("Array pos out of bounds");
            return new Vector2Int(0,0);
        }
    }

    public Vector2Int TilemapPosToArrayPos(int x, int y) // in progress
    {
        //DO A BOUNDS CHECK HERE
        if(x < -halfSideLength || y < -halfSideLength || x >= halfSideLength || y >= halfSideLength)
        {
            Debug.LogWarning("Out of bounds");
            return Vector2Int.zero;
        }
        else
        {
            return new Vector2Int(x + halfSideLength, y + halfSideLength);
        }
    }
    

    void OnDrawGizmos()    
    {
        Gizmos.color = Color.green;
        for(int i = -24 ; i < 25; i++)
        {
            for(int q = -24 ; q < 25 ; q++)
            {
                if(PlacementAllowed(i,q))
                    Gizmos.DrawSphere(TilemapPosToLocalPos(i,q) + transform.position , .1f);
            }
        }
    }
    
}
