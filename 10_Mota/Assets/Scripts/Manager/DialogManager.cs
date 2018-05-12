using Rotorz.Tile;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {

    private Player player;
    private PlayerAttributes _PA;
    private GameManager _GM;
    private GameDataManager _GDM;
    private TileManager _TM;


    public bool isShowing = false;
    public string state = "";

    private string _text;
    private string[] _choices;

    public string tipContent = "";
    public string infoDabuguo;

    public float tipTime = 0;
    public float dialogTime = 0;

    public Texture2D dialogboxbg;
    private float displayScale = 1;
    private Rect dialogbox;

    private bool pressed = false;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player").gameObject.GetComponent<Player>();
        _PA = player.gameObject.GetComponent<PlayerAttributes>();
        _GM = this.GetComponent<GameManager>();
        _GDM = this.GetComponent<GameDataManager>();
        _TM = this.GetComponent<TileManager>();
        _PA = player.gameObject.GetComponent<PlayerAttributes>();

        displayScale = Screen.width / 400f;

        var test=  Dialoguer.events;
       
        Dialoguer.events.onEnded += onEnded;
        Dialoguer.events.onTextPhase += onTextPhase;
        Dialoguer.events.onMessageEvent += onMessageEvent;
    }
	
	// Update is called once per frame
	void Update () {
        Dialoguer.SetGlobalFloat(0, _PA._jinbi);
        Dialoguer.SetGlobalFloat(1, _PA._jingyan);
        Dialoguer.SetGlobalFloat(2, _PA._key_yellow);
        Dialoguer.SetGlobalFloat(3, _PA._key_blue);
        Dialoguer.SetGlobalFloat(4, _PA._key_red);
    }

    void OnGUI()
    {
        GUI.skin.box.normal.textColor = new Vector4(1, 1, 1, 1);
        GUI.skin.box.padding = new RectOffset(15, 15, 15, 15);
        GUI.skin.box.fontSize = scaleGUI(8);
        GUI.skin.box.alignment = TextAnchor.UpperLeft;
        GUI.skin.box.normal.background = dialogboxbg;
        GUI.skin.button.fontSize = scaleGUI(8);
        switch (state)
        {
            case "dialog":
                {
                    player.canMove = false;
                    if (!string.IsNullOrEmpty(_text))
                    {
                        dialogbox = new Rect(
                            Screen.width / 2 - scaleGUI(95),
                            Screen.height / 2 - scaleGUI(113), 
                            scaleGUI(190), 
                            scaleGUI(190));
                        GUI.Box(dialogbox, _text);
                    }
                    if (_choices == null)
                    {
                        if (GUI.Button(new Rect(
                            Screen.width / 2 - scaleGUI(95),
                            Screen.height / 2 + scaleGUI(50),
                            scaleGUI(190),
                            scaleGUI(30)),
                            "继续"))
                        {
                            Dialoguer.ContinueDialogue();
                        }
                    }
                    else
                    {
                        pressed = false;
                        for (int i = _choices.Length - 1; i >= 0; i--) {
                            if (GUI.Button(new Rect(
                                Screen.width / 2 - scaleGUI(95),
                                Screen.height / 2 + scaleGUI((i - _choices.Length + 2) * 35),
                                scaleGUI(190),
                                scaleGUI(30)),
                                _choices[i]))
                            {
                                Dialoguer.ContinueDialogue(i);
                            }
                        }
                    }
                }
                break;
            case "dabuguo":
                {
                    player.canMove = false;
                    GUI.Box(new Rect(
                        Screen.width/2 - scaleGUI(72),
                        Screen.height/2 -scaleGUI(120),
                        scaleGUI(144),
                        scaleGUI(220)),
                        infoDabuguo);
                    if (GUI.Button(new Rect(
                        Screen.width / 2 - scaleGUI(60),
                        Screen.height / 2 + scaleGUI(45),
                        scaleGUI(120),
                        scaleGUI(35)),
                        "知道了"))
                    {
                        state = string.Empty;
                        player.canMove = true;
                    }
                }
                break;
            case "floorchange":
                {
                    if (dialogTime > 0)
                    {
                        dialogTime -= Time.deltaTime;
                        GUI.Box(new Rect(
                           Screen.width / 2 - scaleGUI(72),
                           Screen.height / 2 - scaleGUI(20),
                           scaleGUI(144),
                           scaleGUI(40)),
                           "当前第 " + _GM.CurrentFloor + " 层");
                    }
                    else
                    {
                        dialogTime = 0;
                        state = string.Empty;
                    }
                }
                break;
            case "tujian":
                if (Input.GetMouseButton(0))
                {
                    GameObject mycamera = GameObject.Find("Camera");
                    Vector3 position = mycamera.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
                    TileIndex ti = _TM._TileSystem.ClosestTileIndexFromWorld(position);
                    TileData tile = _TM._TileSystem.GetTile(ti);
                    if (tile != null && tile.GetUserFlag(3))
                    {
                        Guaiwu guaiwu = tile.gameObject.GetComponent<Guaiwu>();
                        string info = "";
                        if (!tile.GetUserFlag(10))
                        {
                            if (_PA._gongji <= guaiwu.fangyu)
                            {
                                info = "你破不了它的防御。\n";
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
                                info = "战胜它你将损失：" + zongshanghai + "生命。\n";
                            }
                        }
                        info += "生命：" + guaiwu.shengming + "  /  ";
                        info += "攻击：" + guaiwu.gongji + "  /  ";
                        info += "防御：" + guaiwu.fangyu + "\n";
                        info += "金币：" + guaiwu.jinbi + "  /  ";
                        info += "经验：" + guaiwu.jingyan;
                        if (position.y > -5)
                        {
                            //显示在下面
                            GUI.Box(new Rect(
                                Screen.width / 2 - scaleGUI(95),
                                Screen.height / 2 + scaleGUI(30),
                                scaleGUI(190),
                                scaleGUI(50)), 
                                info);
                        }
                        else
                        {
                            //显示在上面
                            GUI.Box(new Rect(
                                Screen.width / 2 - scaleGUI(95),
                                Screen.height / 2 - scaleGUI(113),
                                scaleGUI(190),
                                scaleGUI(50)), 
                                info);
                        }
                    }
                }
                break;
            case "feixing":
                player.canMove = false;
                for (int i = 1; i < _GM.maxFloor + 1; i++)
                {
                    int y = i % 3;
                    if (y == 0) { y = -1; } else if (y == 2) { y = 0; }
                    int z = (i - 1) / 3;
                    if (GUI.Button(
                        new Rect(Screen.width / 2 - scaleGUI(60 * y + 35),
                        Screen.height / 2 - scaleGUI(113) + scaleGUI(25 * z),
                        scaleGUI(50),
                        scaleGUI(20)), 
                        "第 " + i + " 层"))
                    {
                        _GM.changeFloor(i);
                        state = "";
                        player.canMove = true;
                    }
                }
                break;
        }
        if (tipTime > 0 && !string.IsNullOrEmpty(tipContent))
        {
            GUI.skin.box.alignment = TextAnchor.MiddleCenter;
            GUI.Box(new Rect(
                Screen.width / 2 - scaleGUI(95),
                Screen.height / 2 + scaleGUI(50),
                scaleGUI(190),
                scaleGUI(30)),
                tipContent);
            tipTime -= Time.deltaTime;
        }
        else
        {
            tipTime = 0;
            tipContent = string.Empty;
        }
        if (string.IsNullOrEmpty(state))
        {
            player.canMove = true;
        }
    }
    int scaleGUI(float number)
    {
        int resultNumber = Mathf.RoundToInt(number * displayScale);
        return resultNumber;
    }

    private void onEnded()
    {
        state = "";
        player.canMove = true;
    }

    private void onTextPhase(DialoguerTextData data)
    {
        state = "dialog";
        _text = data.text;
        _choices = data.choices;
    }

    private void onMessageEvent(string message, string metadata)
    {
        switch (message)
        {
            case "dialog_jingling_over1":
                _PA._key_yellow += 1;
                _PA._key_blue += 1;
                _PA._key_red += 1;
                tipContent = "各颜色钥匙+1";
                tipTime = 3;
                break;
            case "opendoor":
                {
                    if (_GM.clear2Door())
                    {
                        state = "tip";
                        tipContent = "二楼门已打开";
                        tipTime = 3;
                    }
                }
                break;
            case "jia_shengming":
                    if (!pressed) _PA._shengming += int.Parse(metadata);
                break;
            case "jia_gongji":
                if (!pressed) _PA._gongji += int.Parse(metadata);
                break;
            case "jia_fangyu":
                if (!pressed) _PA._fangyu += int.Parse(metadata);
                break;
            case "jia_dengji":
                if (!pressed)
                {
                    _PA._dengji += int.Parse(metadata);
                    _PA._gongji += int.Parse(metadata)*5;
                    _PA._fangyu += int.Parse(metadata) * 5;
                    _PA._shengming += int.Parse(metadata) * 600;
                }
                break;
            case "jia_key_yellow":
                if (!pressed) _PA._key_yellow += int.Parse(metadata);
                break;
            case "jia_key_blue":
                if (!pressed) _PA._key_blue += int.Parse(metadata);
                break;
            case "jia_key_red":
                if (!pressed) _PA._key_red += int.Parse(metadata);
                break;
            case "jian_jinagyan":
                if (!pressed)
                {
                    _PA._jingyan -= int.Parse(metadata);
                    pressed = true;
                }
                break;
            case "jian_jinbi":
                {
                    _PA._jinbi -= int.Parse(metadata);
                    pressed = true;
                }
                break;
        }
    }

    public void showFloor()
    {
        state = "feixing";
    }

    public void showInfo()
    {
        Image button = GameObject.Find("Button_tujian").GetComponent<Image>();
        if (button.color == new Color(200f / 255, 200f / 255, 200f / 255, 1))
        {
            button.color = new Color(1, 1, 1, 1);
            state = "";
        }
        else
        {
            button.color = new Color(200f / 255, 200f / 255, 200f / 255, 1);
            state = "tujian";
        }
    }

    public void showMenu()
    {
        state = "menu";
    }
}
