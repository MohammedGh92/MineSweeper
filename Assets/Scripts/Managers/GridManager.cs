using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : Singleton<GridManager>
{
    [SerializeField]
    private RootController rootController;
    public GameObject TileItemPrefab;
    public Transform GridParentTrans;

    private GridO MyGrid;

    public Sprite ClosedSpr, EmptySpr, MineSpr;

    private int RevealedTilesCounter;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            RevealAll(true);
        else if (Input.GetKeyUp(KeyCode.Space))
            RevealAll(false);
    }

    public void GenerateNewGrid()
    {
        ShowGrid(true);
        MyGrid = new GridO();
        for (int y = 0; y > -9; y--)
            for (int x = 0; x < 9; x++)
            {
                Vector2 TilePos = new Vector2(x, y);
                GameObject TileGO = Instantiate(TileItemPrefab, TilePos, Quaternion.identity, GridParentTrans) as GameObject;
                TileItem CurrentTileItem = TileGO.GetComponent<TileItem>();
                TileItemO CurrentTileItemObj = new TileItemO(TilePos);
                CurrentTileItem.SetItem(CurrentTileItemObj, this);
                MyGrid.SetThisTileItem(TilePos, CurrentTileItem);
            }
        MyGrid.SetMines();
        MyGrid.SetTilesNumbers();
    }


    public void RevealThis(TileItem tileItem)
    {
        if (tileItem.GetTileIsRevealed())
            return;
        tileItem.RevealThisTile(true);
        RevealedTilesCounter += 1;
        if (RevealedTilesCounter >= 71)
            rootController.ChangeController(RootController.ControllerTypeEnum.GameOver);
        if (tileItem.GetTileType() == TileType.mine)
            rootController.ChangeController(RootController.ControllerTypeEnum.GameOver);
        if (tileItem.GetTileType() != TileType.empty)
            return;
        List<TileItem> Items = MyGrid.GetTileNaighbours(tileItem);
        for (int i = 0; i < Items.Count; i++)
            if (Items[i].GetTileType() != TileType.mine)
                RevealThis(Items[i]);
    }

    public Sprite GetSpriteFromTileType(TileType tileType)
    {
        switch (tileType)
        {
            case TileType.empty: return EmptySpr;
            case TileType.mine: return MineSpr;
            default:
                return EmptySpr;
        }
    }

    public int GetNumOfMinesBasedOnTileType(TileType tileType)
    {
        if ((int)tileType < 2)
            return 0;
        else
            return (int)tileType - 2;
    }

    //Reveal all, test methods
    private void RevealAll(bool Reveal)
    {
        MyGrid.RevealAll(Reveal);
    }

    public bool IsWinner()
    {
        return RevealedTilesCounter >= 71;
    }

    public void ShowGrid(bool Show) { GridParentTrans.gameObject.SetActive(Show); }

}