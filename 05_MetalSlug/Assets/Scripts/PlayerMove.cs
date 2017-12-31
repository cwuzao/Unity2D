using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState {
    PlayerGround,
    PlayerDown,
    PlayerJump
}

public class PlayerMove : MonoBehaviour {

    public PlayerGround playerGround;
    public PlayerDown playerDown;
    public PlayerJump playerJump;
    public float speed = 3;
    public float jumpSpeed = 4;
    private Rigidbody rigidBody;
    private bool isGround = false;
    private int groundLayerMask;
    public PlayerState state = PlayerState.PlayerJump;
    private bool isBottonButtonClick = false;


    private void Start()
    {
        groundLayerMask = LayerMask.GetMask("Ground");
    }

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();     
    }

    // Update is called once per frame
    void Update () {

        if (Input.GetKeyDown(KeyCode.S))
        {
            isBottonButtonClick = true;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            isBottonButtonClick = false;
        }
        float h = Input.GetAxis("Horizontal");
        Vector3 v = rigidBody.velocity;
        rigidBody.velocity = new Vector3(h * speed, v.y, v.z);
        v = rigidBody.velocity;

        RaycastHit hitinfo;
        isGround = Physics.Raycast(transform.position
            + Vector3.up * 0.1f,
            Vector3.down,
            out hitinfo,
            0.2f,
            groundLayerMask);
        if (isGround == false)
        {
            state = PlayerState.PlayerJump;
        }
        else if (isBottonButtonClick)
        {
            state = PlayerState.PlayerDown;
        }
        else
        {
            state = PlayerState.PlayerGround;
        }

        if (isGround && Input.GetKeyDown(KeyCode.K))
        {
            rigidBody.velocity = new Vector3(v.x, jumpSpeed, v.z);
        }

        switch (state)
        {
            case PlayerState.PlayerDown:
                playerDown.gameObject.SetActive(true);
                playerGround.gameObject.SetActive(false);
                playerJump.gameObject.SetActive(false);
                break;
            case PlayerState.PlayerGround:
                playerDown.gameObject.SetActive(false);
                playerGround.gameObject.SetActive(true);
                playerJump.gameObject.SetActive(false);
                break;
            case PlayerState.PlayerJump:
                playerDown.gameObject.SetActive(false);
                playerGround.gameObject.SetActive(false);
                playerJump.gameObject.SetActive(true);
                break;
        }

        float x = 1;
        if (rigidBody.velocity.x > 0.05f)
        {
            x = -1;
        }
        else if (rigidBody.velocity.x < -0.05f)
        {
            x = 1;
        }
        else {
            x = 0;
        }

        if (x != 0)
        {
            playerGround.transform.localScale = new Vector3(x,1,1);
            playerJump.transform.localScale = new Vector3(x, 1, 1);
            playerDown.transform.localScale = new Vector3(x, 1, 1);
        }

        if (Mathf.Abs(rigidBody.velocity.x) > 0.05f)
        {
            playerGround.status = AnimStatus.Walk;
            playerDown.status = AnimStatus.Walk;
        }
        else
        {
            playerGround.status = AnimStatus.Idle;
            playerDown.status = AnimStatus.Idle;
        }
	}
}
