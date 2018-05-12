using Rotorz.Tile;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActionManager : MonoBehaviour {

    private GameDataManager _GDM;
    private GameManager _GM;
    private DialogManager _DM;
    private PlayerAttributes _PA;

	// Use this for initialization
	void Start () {
        _GM = this.GetComponent<GameManager>();
        _GDM = this.GetComponent<GameDataManager>();
        _DM = this.GetComponent<DialogManager>();
        GameObject player = GameObject.Find("Player").gameObject;
        _PA = player.GetComponent<PlayerAttributes>();
	}

    

    public void talk(int x, int y, TileData otherTileData)
    {
        Talk talk = otherTileData.gameObject.GetComponent<Talk>();
        Dialoguer.StartDialogue(talk.dialogureID);
    }

    public void door(int x, int y, TileData otherTileData)
    {
        TRAnimation door = otherTileData.gameObject.GetComponent<TRAnimation>();
        if (_PA._key_yellow > 0 && door.CurrentIndex == 1)
        {
            GameObject.Destroy(door.gameObject);
            otherTileData.Clear();
            _PA._key_yellow -= 1;
            _DM.tipContent = "黄钥匙-1";
            _DM.tipTime = 3f;
        }
        if (_PA._key_blue > 0 && door.CurrentIndex == 2)
        {
            GameObject.Destroy(door.gameObject);
            otherTileData.Clear();
            _PA._key_blue -= 1;
            _DM.tipContent = "蓝钥匙-1";
            _DM.tipTime = 3f;
        }
        if (_PA._key_red > 0 && door.CurrentIndex == 3)
        {
            GameObject.Destroy(door.gameObject);
            otherTileData.Clear();
            _PA._key_red -= 1;
            _DM.tipContent = "红钥匙-1";
            _DM.tipTime = 3f;
        }
        if (door.SpriteTexture.name == "door-02")
        {
            GameObject.Destroy(door.gameObject);
            otherTileData.Clear();
        }
    }

    public void stair(int x, int y, TileData otherTileData)
    {
        Stair stair = otherTileData.gameObject.GetComponent<Stair>();
        _GM.changeFloor(stair.floor);
    }

    public void guaiwu(int x, int y, TileData otherTileData)
    {
        Guaiwu guaiwu = otherTileData.gameObject.GetComponent<Guaiwu>();
        _DM.infoDabuguo = "你打不过它。\r\n";
        _DM.infoDabuguo += "怪物属性:\n";
        _DM.infoDabuguo += "生命:" + guaiwu.shengming + "\n";
        _DM.infoDabuguo += "攻击:" + guaiwu.gongji + "\n";
        _DM.infoDabuguo += "防御:" + guaiwu.fangyu + "\n";
        _DM.infoDabuguo += "金币:" + guaiwu.jinbi + "\n";
        _DM.infoDabuguo += "经验:" + guaiwu.jingyan + "\n";
        if (_PA._gongji <= guaiwu.fangyu)
        {
            _DM.state = "dabuguo";
        }
        else
        {
            int shanghai = _PA._gongji - guaiwu.fangyu;
            float cishu = Mathf.Ceil(guaiwu.shengming / shanghai);
            float zongshanghai = 0;
            if (guaiwu.gongji > _PA._fangyu)
            {
                float shoushang = guaiwu.gongji - _PA._fangyu;
                zongshanghai = shoushang * cishu;
            }
            if (zongshanghai >= _PA._shengming)
            {
                _DM.state = "dabuguo";
            }
            else
            {
                _PA._shengming -= (int)zongshanghai;
                _PA._jingyan += guaiwu.jingyan;
                _PA._jinbi += guaiwu.jinbi;
                _DM.tipContent = "经验+" + guaiwu.jingyan + ",金币+" + guaiwu.jinbi;
                _DM.tipTime = 3f;
                GameObject.Destroy(guaiwu.gameObject);
                if (otherTileData.GetUserFlag(9))
                {
                    SceneManager.LoadScene(1);
                }
                otherTileData.Clear();
            }
        }
    }

    public void key(int x, int y, TileData otherTileData)
    {
        Key key = otherTileData.gameObject.GetComponent<Key>();
        _PA._key_yellow += key.key_yellow;
        _PA._key_blue += key.key_blue;
        _PA._key_red += key.key_red;
        _DM.tipContent = key.tip;
        _DM.tipTime = 3f;
        GameObject.Destroy(key.gameObject);
        otherTileData.Clear();
    }

    public void daoju(int x, int y, TileData otherTileData)
    {
        Daoju daoju = otherTileData.gameObject.GetComponent<Daoju>();
        _PA._gongji += daoju.gongji;
        _PA._fangyu += daoju.fangyu;
        _PA._shengming += daoju.shengming;
        _PA._jinbi += daoju.jinbi;
        _PA._dengji += daoju.dengji;
        _PA._gongji += daoju.dengji * 7;
        _PA._fangyu += daoju.dengji * 7;
        _PA._shengming += daoju.dengji * 600;
        _DM.tipContent = daoju.tip;
        _DM.tipTime = 3f;
        GameObject.Destroy(daoju.gameObject);
        otherTileData.Clear();
    }

    public void tujian(int x,int y,TileData otherTileData)
    {
        _DM.tipContent = "开启图鉴，开启后可点击怪物查看信息";
        _DM.tipTime = 3f;
        _PA._daoju_tujian = true;
        GameObject.Destroy(otherTileData.gameObject);
        otherTileData.Clear();
    }

    public void feixing(int x, int y, TileData otherTileData)
    {
        _DM.tipContent = "开启传送，可以传送到其他楼层";
        _DM.tipTime = 3f;
        _PA._daoju_feixing = true;
        GameObject.Destroy(otherTileData.gameObject);
        otherTileData.Clear();
    }
}
