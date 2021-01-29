using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileItemO
{

    private bool IsRevealed;
    private TileType tileType;
    private Vector2 tilePos;

    public TileItemO(Vector2 tilePos)
    {
        SetTileType(TileType.empty);
        SetTilePos(tilePos);
    }

    public void SetTilePos(Vector2 tilePos)
    {
        int X = (int)tilePos.x;
        int Y = (int)Mathf.Abs(tilePos.y);
        this.tilePos = new Vector2(X, Y);
        SetTileIsRevealed(false);
    }

    public Vector2 GetTilePos()
    {
        return this.tilePos;
    }

    public void SetTileType(TileType tileType) { this.tileType = tileType; }
    public TileType GetTileType() { return tileType; }

    public void SetTileIsRevealed(bool IsRevealed) { this.IsRevealed = IsRevealed; }
    public bool GetTileIsRevealed() { return this.IsRevealed; }

}