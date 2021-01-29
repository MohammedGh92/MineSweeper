using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class TileItem : MonoBehaviour
{

    private TileItemO MyTileItemObj;
    [SerializeField]
    private TextMeshPro Num_Of_Mines;
    [SerializeField]
    private SpriteRenderer MySprRenderer;
    private GridManager gridManager;

    public void SetItem(TileItemO NewTileItemObj, GridManager gridManager)
    {
        MyTileItemObj = NewTileItemObj;
        
        this.gridManager = gridManager;
    }

    public void SetTileType(TileType tileType)
    {
        MyTileItemObj.SetTileType(tileType);
    }

    public void OnMouseDown()
    {
        if (!MyTileItemObj.GetTileIsRevealed())
            gridManager.RevealThis(this);
    }

    public TileItemO GetMyTileObj()
    {
        return this.MyTileItemObj;
    }

    public void RevealThisTile(bool Reveal)
    {
        MyTileItemObj.SetTileIsRevealed(Reveal);
        TileType MyType = GetTileType();
        if (Reveal)
        {
            MySprRenderer.sprite = gridManager.GetSpriteFromTileType(MyType);
            if (gridManager.GetNumOfMinesBasedOnTileType(MyType) == 0)
                Num_Of_Mines.SetText("");
            else
                Num_Of_Mines.SetText("" + gridManager.GetNumOfMinesBasedOnTileType(MyType));
        }
        else
        {
            MySprRenderer.sprite = gridManager.ClosedSpr;
            Num_Of_Mines.SetText("");
        }
    }

    public TileType GetTileType()
    {
        return MyTileItemObj.GetTileType();
    }

    public bool GetTileIsRevealed() { return MyTileItemObj.GetTileIsRevealed(); }

}