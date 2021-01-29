using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridO : MonoBehaviour
{

    public TileItem[,] tileItems;

    public GridO()
    {
        tileItems = new TileItem[9, 9];
    }

    public void SetThisTileItem(Vector2 TilePos, TileItem tileItem)
    {
        int X = (int)TilePos.x;
        int Y = (int)Mathf.Abs(TilePos.y);
        tileItems[X, Y] = tileItem;
    }

    public void SetTileType(Vector2 TilePos, TileType NewType)
    {
        tileItems[(int)TilePos.x, (int)TilePos.y].SetTileType(NewType);
    }

    public List<TileItem> GetTileNaighbours(TileItem tileItem)
    {

        List<TileItem> ReturnedList = new List<TileItem>();
        int X = (int)tileItem.GetMyTileObj().GetTilePos().x;
        int Y = (int)tileItem.GetMyTileObj().GetTilePos().y;

        //Get the three right squares

        if (X <= 7)
        {
            //upper right
            if (Y >= 1) ReturnedList.Add(tileItems[X + 1, Y - 1]);
            //right
            ReturnedList.Add(tileItems[X + 1, Y]);
            //lower right
            if (Y <= 7) ReturnedList.Add(tileItems[X + 1, Y + 1]);
        }

        //Get the three left squares

        if (X >= 1)
        {
            //upper left
            if (Y >= 1) ReturnedList.Add(tileItems[X - 1, Y - 1]);
            //left
            ReturnedList.Add(tileItems[X - 1, Y]);
            //lower left
            if (Y <= 7) ReturnedList.Add(tileItems[X - 1, Y + 1]);
        }

        //Get the up and down square
        {
            //upper
            if (Y >= 1) ReturnedList.Add(tileItems[X, Y - 1]);
            //lower
            if (Y <= 7) ReturnedList.Add(tileItems[X, Y + 1]);
        }
        return ReturnedList;
    }

    public void RevealThisTile(int X, int Y)
    {
        tileItems[X, Y].RevealThisTile(true);
    }

    public void GetTileType(int X, int Y)
    {
        tileItems[X, Y].GetTileType();
    }

    //Reveal All, Test methods
    public void RevealAll(bool Reveal)
    {
        for (int x = 0; x < 9; x++)
            for (int y = 0; y < 9; y++)
                tileItems[x, y].RevealThisTile(Reveal);

    }


    public void SetMines()
    {
        int NuOfMines = 10;
        while (NuOfMines > 0)
        {
            int X = UnityEngine.Random.Range(0, 9);
            int Y = UnityEngine.Random.Range(0, 9);
            if (tileItems[X, Y].GetMyTileObj().GetTileType() != TileType.mine)
            {
                tileItems[X, Y].SetTileType(TileType.mine);
                NuOfMines -= 1;
            }
        }
    }

    public void SetTilesNumbers()
    {
        for (int x = 0; x < 9; x++)
            for (int y = 0; y < 9; y++)
            {
                if (tileItems[x, y].GetTileType() == TileType.mine)
                    continue;
                int MinesCounter = GetTileMinesNo(tileItems[x, y]);
                tileItems[x, y].SetTileType(GetTileTypeFromMinesNo(MinesCounter));
            }
    }

    private TileType GetTileTypeFromMinesNo(int MinesNo)
    {
        if (MinesNo == 0)
            return TileType.empty;
        else
            return (TileType)MinesNo+2;
    }

    public int GetTileMinesNo(TileItem tileItem)
    {
        int MinesCounter = 0;
        List<TileItem> Naighbours = GetTileNaighbours(tileItem);
        for (int i = 0; i < Naighbours.Count; i++)
            if (Naighbours[i].GetTileType() == TileType.mine)
                MinesCounter += 1;
        return MinesCounter;
    }
}