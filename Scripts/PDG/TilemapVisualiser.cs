using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Tilemaps;
using Debug = UnityEngine.Debug;
using UnityEngine.SceneManagement;

public class TilemapVisualiser : MonoBehaviour
{
    [SerializeField]
    private Tilemap floorTilemap;
    [SerializeField]
    private Tilemap wallTilemap;
    [SerializeField]
    private Tilemap doorTilemap;
    [SerializeField]
    private Tilemap decorTilemap;

    [SerializeField]
    private TileBase floorTile;

    [SerializeField]
    private TileBase doorTile;

    [SerializeField]
    private TileBase wallPlant1Tile;
    [SerializeField]
    private TileBase wallPlant2Tile;

 

   

    [SerializeField]
    private TileBase flower1Tile;
    [SerializeField]
    private TileBase flower2Tile;

    public bool doorGenerated = false;
    public bool playerPlaced = false;
    public bool chestPlaced = false;

    [SerializeField]
    private TileBase wallTop;
    [SerializeField]
    private TileBase wallSideRight;
    [SerializeField]
    private TileBase wallSideLeft;
    [SerializeField]
    private TileBase wallBottom;
    [SerializeField]
    private TileBase wallFull;

    [SerializeField]
    private TileBase wallInnerCornerDownLeft;
    [SerializeField]
    private TileBase wallInnerCornerDownRight;
    [SerializeField]
    private TileBase wallDiagonalCornerDownRight;
    [SerializeField]
    private TileBase wallDiagonalCornerDownLeft;
    [SerializeField]
    private TileBase wallDiagonalCornerUpLeft;
    [SerializeField]
    private TileBase wallDiagonalCornerUpRight;

    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)
    {
        PaintTiles(floorPositions, floorTilemap, floorTile);
    }

    public void PaintDecor(IEnumerable<Vector2Int> positions, Tilemap tileMap, TileBase tile)
    {
        foreach (var position in positions)
        {
            PaintSingleTile(tileMap, tile, position);
        }
    }
    

    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tileMap, TileBase tile)
    {
        Scene currentScene;
        string currentSceneName;

        currentScene = SceneManager.GetActiveScene();
        currentSceneName = currentScene.name;

        int i = 0;
        foreach (var position in positions)
        {
            i++;
            PaintSingleTile(tileMap, tile, position);

            if (playerPlaced == false)
            {
                StaticValues.playerPositionX = position.x;
                StaticValues.playerPositionY = position.y;
                
                playerPlaced = true;
            }

            if (i==5)
            {
                StaticValues.chestPositionX = position.x;
                StaticValues.chestPositionY = position.y;
                
                chestPlaced = true;
            }

            if (i == 10 && currentSceneName == "Level 1")
            {
                StaticValues.enemy1PositionX = position.x;
                StaticValues.enemy1PositionY = position.y;

            }

            if (i == 20 && currentSceneName == "Level 1")
            {
                StaticValues.enemy2PositionX = position.x;
                StaticValues.enemy2PositionY = position.y;

            }

            if (i == 10 && currentSceneName == "Level 2")
            {
                StaticValues.enemy3PositionX = position.x;
                StaticValues.enemy3PositionY = position.y;

            }

            if (i == 20 && currentSceneName == "Level 2")
            {
                StaticValues.enemy4PositionX = position.x;
                StaticValues.enemy4PositionY = position.y;

            }

            if (i == 30 && currentSceneName == "Level 2")
            {
                StaticValues.enemy5PositionX = position.x;
                StaticValues.enemy5PositionY = position.y;

            }

            if (i == 10 && currentSceneName == "Level 3")
            {
                StaticValues.enemy6PositionX = position.x;
                StaticValues.enemy6PositionY = position.y;

            }

            if (i == 20 && currentSceneName == "Level 3")
            {
                StaticValues.enemy7PositionX = position.x;
                StaticValues.enemy7PositionY = position.y;

            }

            if (i == 30 && currentSceneName == "Level 3")
            {
                StaticValues.enemy8PositionX = position.x;
                StaticValues.enemy8PositionY = position.y;

            }

            if (i == 40 && currentSceneName == "Level 3")
            {
                StaticValues.enemy9PositionX = position.x;
                StaticValues.enemy9PositionY = position.y;

            }
        }
    }

    internal void PaintSingleBasicWall(Vector2Int position, string binaryType)
    {
        int typeAsInt = Convert.ToInt32(binaryType, 2);
        TileBase tile = null;
        if(WallByteTypes.wallTop.Contains(typeAsInt))
        {
            tile = wallTop;
            
        }
        else if(WallByteTypes.wallSideRight.Contains(typeAsInt))
        {
            tile = wallSideRight;
        }
        else if (WallByteTypes.wallSideLeft.Contains(typeAsInt))
        {
            tile = wallSideLeft;
        }
        else if (WallByteTypes.wallBottom.Contains(typeAsInt))
        {
            tile = wallBottom;
        }
        else if (WallByteTypes.wallFull.Contains(typeAsInt))
        {
            tile = wallFull;
        }

        if (tile != null)
        {
            
            if (doorGenerated == false && tile == wallTop)
            {
                PaintSingleTile(doorTilemap, doorTile, position);
                doorGenerated = true;

            }
            else
            {
                PaintSingleTile(wallTilemap, tile, position);
            }
        }
        
    }


   

    internal void PaintWallDecor1(Vector2Int position, string binaryType)
    {
        int typeAsInt = Convert.ToInt32(binaryType, 2);
        TileBase tile = null;
        if (WallByteTypes.wallPlants.Contains(typeAsInt))
        {
            tile = wallPlant1Tile;

        }

        if (tile != null)
        {
           PaintSingleTile(wallTilemap, tile, position);
            
        }

    }

    internal void PaintWallDecor2(Vector2Int position, string binaryType)
    {
        int typeAsInt = Convert.ToInt32(binaryType, 2);
        TileBase tile = null;
        if (WallByteTypes.wallPlants.Contains(typeAsInt))
        {
            tile = wallPlant2Tile;

        }

        if (tile != null)
        {
            PaintSingleTile(wallTilemap, tile, position);

        }

    }

   

    private void PaintSingleTile(Tilemap tileMap, TileBase tile, Vector2Int position)
    {
        var tilePosition = tileMap.WorldToCell((Vector3Int) position);
        tileMap.SetTile(tilePosition, tile);
    }

    public void Clear()
    {
        floorTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
        doorTilemap.ClearAllTiles();
        decorTilemap.ClearAllTiles();
    }

    internal void PaintSingleCornerWall(Vector2Int position, string binaryType)
    {
        int typeAsInt = Convert.ToInt32(binaryType, 2);
        TileBase tile = null;

        if(WallByteTypes.wallInnerCornerDownLeft.Contains(typeAsInt))
        {
            tile = wallInnerCornerDownLeft;
        }
        else if (WallByteTypes.wallInnerCornerDownRight.Contains(typeAsInt))
        {
            tile = wallInnerCornerDownRight;
        }
        else if (WallByteTypes.wallDiagonalCornerDownRight.Contains(typeAsInt))
        {
            tile = wallDiagonalCornerDownRight;
        }
        else if (WallByteTypes.wallDiagonalCornerDownLeft.Contains(typeAsInt))
        {
            tile = wallDiagonalCornerDownLeft;
        }
        else if (WallByteTypes.wallDiagonalCornerUpLeft.Contains(typeAsInt))
        {
            tile = wallDiagonalCornerUpLeft;
        }
        else if (WallByteTypes.wallDiagonalCornerUpRight.Contains(typeAsInt))
        {
            tile = wallDiagonalCornerUpRight;
        }
        else if (WallByteTypes.wallFullEightDirections.Contains(typeAsInt))
        {
            tile = wallFull;
        }
        else if (WallByteTypes.wallBottomEightDirections.Contains(typeAsInt))
        {
            tile = wallBottom;
        }





        if (tile != null)
        {
            PaintSingleTile(wallTilemap, tile, position);
        }
    }
}
