using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public GameObject GameController;
    private DialogManager _DM;
    private TileManager _TM;
    private TRAnimation _TRA;

    public Vector3 leftDistance = Vector3.zero;
    public bool isMoving = false;
    public bool canMove = true;

    private Collider2D _Collider2D;
    private float lastMoveTime = 0f;

    void Start () {
        _DM = GameController.GetComponent<DialogManager>();
        _TM = GameController.GetComponent<TileManager>();
        _TRA = this.GetComponent<TRAnimation>();

        _Collider2D = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (canMove)
        {

            if (Input.GetKey(KeyCode.UpArrow) && (Time.time - lastMoveTime) > 0.15)
            {
                lastMoveTime = Time.time;
                _TRA.CurrentIndex = 4;
                _TRA.SendMessage("initSpriteAnimation");
                if (_TM.CheckTile("up"))
                {
                    isMoving = true;
                    leftDistance = Vector3.up;
                    MovePlayer();
                }
            }
            else if (Input.GetKey(KeyCode.DownArrow) && (Time.time - lastMoveTime) > 0.15)
            {
                lastMoveTime = Time.time;
                _TRA.CurrentIndex = 1;
                _TRA.SendMessage("initSpriteAnimation");
                if (_TM.CheckTile("down"))
                {
                    isMoving = true;
                    leftDistance = Vector3.down;
                    MovePlayer();
                }
            }
            else if (Input.GetKey(KeyCode.LeftArrow) && (Time.time - lastMoveTime) > 0.15)
            {
                lastMoveTime = Time.time;
                _TRA.CurrentIndex = 2;
                _TRA.SendMessage("initSpriteAnimation");
                if (_TM.CheckTile("left"))
                {
                    isMoving = true;
                    leftDistance = Vector3.left;
                    MovePlayer();
                }
            }
            else if (Input.GetKey(KeyCode.RightArrow) && (Time.time - lastMoveTime) > 0.15)
            {
                lastMoveTime = Time.time;
                _TRA.CurrentIndex = 3;
                _TRA.SendMessage("initSpriteAnimation");
                if (_TM.CheckTile("right"))
                {
                    isMoving = true;
                    leftDistance = Vector3.right;
                    MovePlayer();
                }
            }
        }
    }

    void MovePlayer()
    {
        transform.position += leftDistance;
        isMoving = false;
    }

}
