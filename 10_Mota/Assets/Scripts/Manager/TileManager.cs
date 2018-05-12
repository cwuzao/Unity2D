using Rotorz.Tile;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {
    public TileSystem _TileSystem;
    public TileIndex PlayerTileIndex;
    public TileIndex OtherTileIndex;
    public TileData OtherTileData;

    private ActionManager _AM;
    private GameManager _GM;
    public Transform PlayerTransform;

	// Use this for initialization
	void Start () {
        _GM = this.GetComponent<GameManager>();
        _AM = this.GetComponent<ActionManager>();
        _TileSystem = GameObject.Find("Floor" + _GM.CurrentFloor)
            .gameObject.GetComponent<TileSystem>();
    }
	
    public bool CheckTile(string arrow)
    {
        PlayerTileIndex = _TileSystem
            .ClosestTileIndexFromWorld(PlayerTransform.position);
        int x = PlayerTileIndex.row;
        int y = PlayerTileIndex.column;
        switch (arrow)
        {
            case "up":      x -= 1; break;
            case "down":    x += 1; break;
            case "left":    y -= 1; break;
            case "right":   y += 1; break;
        }
        if (x < 0 || y < 0 || x > 10 || y > 10)
        {
            return false;
        }
        OtherTileData = _TileSystem.GetTile(x, y);
        if (OtherTileData != null)
        {
            if (OtherTileData.GetUserFlag(1))
            {
                _AM.talk(x, y, OtherTileData);
                return false;
            }
            if (OtherTileData.GetUserFlag(2))
            {
                _AM.daoju(x, y, OtherTileData);
                return true;
            }
            if (OtherTileData.GetUserFlag(3))
            {
                _AM.guaiwu(x, y, OtherTileData);
                return false;
            }
            if (OtherTileData.GetUserFlag(4))
            {
                _AM.door(x,y,OtherTileData);
                return false;
            }
            if (OtherTileData.GetUserFlag(5))
            {
                _AM.key(x,y,OtherTileData);
                return true;
            }
            if (OtherTileData.GetUserFlag(6))
            {
                _AM.stair(x, y, OtherTileData);
                return false;
            }
            if (OtherTileData.GetUserFlag(7))
            {
                _AM.tujian(x,y,OtherTileData);
                return true;
            }
            if (OtherTileData.GetUserFlag(8))
            {
                _AM.feixing(x,y,OtherTileData);
                return true;
            }
            if (OtherTileData.GetUserFlag(9))
            {
                _AM.guaiwu(x, y, OtherTileData);
                return false;
            }
            if (OtherTileData.SolidFlag)
            {
                return false;
            }
        }
        return true;
    }
}
