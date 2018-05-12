using Rotorz.Tile;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public int CurrentFloor = 0;
    public int maxFloor = 0;
    public GameObject globalGameObject;
    public GameObject[] floorGo;

    private DialogManager _DM;
    private PlayerAttributes _PA;
    private Transform playerTransform;
    private TileManager _TM;

    void Awake()
    {

    }

    void Start() {
        GameObject player = GameObject.Find("Player").gameObject;
        _DM = this.GetComponent<DialogManager>();
        playerTransform = player.transform;
        _PA = player.GetComponent<PlayerAttributes>();
        _TM = this.GetComponent<TileManager>();
    }

    public void changeFloor(int floor, bool checkUpDown = true)
    {
        if (CurrentFloor != floor)
        {
            floorGo[floor].SetActive(true);

            Vector3 ggoPosition = globalGameObject.transform.position;
            ggoPosition.x = 5 + (floor * (11 + 1));
            globalGameObject.transform.position = ggoPosition;
            if (checkUpDown)
            {
                if (CurrentFloor < floor)
                {
                    playerTransform.position = _PA.getPlayerPositionUp(floor);
                }
                else
                {
                    playerTransform.position = _PA.getPlayerPositionDown(floor);
                }
            }
            else {
                playerTransform.position = _PA.getPlayerPositionUp(floor);
            }
            floorGo[CurrentFloor].SetActive(false);
            CurrentFloor = floor;
            if (CurrentFloor > maxFloor)
            {
                maxFloor = CurrentFloor;
            }
            _TM._TileSystem = floorGo[CurrentFloor].GetComponent<TileSystem>();
            _DM.dialogTime = 1.5f;
            _DM.state = "floorchange";
        }
    }
    public bool clear2Door()
    {
        try
        {
            floorGo[2].SetActive(true);
            TileSystem ts_object = floorGo[2].GetComponent<TileSystem>();
            GameObject door = GameObject.Find("Floor2/chunk_0_0/door-01_3").gameObject;
            TileData tile = ts_object.GetTile(6, 1);
            GameObject.Destroy(door.gameObject);
            tile.Clear();
            //_GDM.sceneData[2][6, 1] = 1;
            floorGo[2].SetActive(false);
            return true;
        }
        catch
        {
            return false;
        }
    }

    void Update() {
        checkFix();
    }

    void checkFix()
    {
        if (Dialoguer.GetGlobalBoolean(1))// && _GDM.sceneData[2][6, 1] == 0
        {
            clear2Door();
            _DM.tipContent = "已帮你打开2楼的门，修正错误，非常抱歉！";
            _DM.tipTime = 6;
        }
    }
}
