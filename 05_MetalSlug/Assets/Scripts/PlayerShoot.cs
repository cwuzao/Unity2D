using UnityEngine;

public enum ShootDir
{
    Left,
    RIght,
    Top,
    Down
}

public class PlayerShoot : MonoBehaviour{
    public int shootRate = 7;

    public PlayerGround playerGround;
    public PlayerDown playerDown;
    public PlayerJump playerJump;

    private float shootTimerInterval = 0;
    private float timer = 0;
    private bool canShoot = true;
    private PlayerMove playerMove;
    private bool isTopKeyDown = false;
    private bool isBottomKeyDown = false;
    private Rigidbody rigidBody;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        shootTimerInterval = 1f / shootRate;
        playerMove = GetComponent<PlayerMove>();
    }

    private void Update()
    {
        if (canShoot == false) {
            timer += Time.deltaTime;
            if (timer >= shootTimerInterval) {
                canShoot = true;
                timer -= shootTimerInterval;
            }
        }

        if (Input.GetKeyDown(KeyCode.W)) {
            isTopKeyDown = true;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            isTopKeyDown = true;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            isBottomKeyDown = true;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            isBottomKeyDown = false;
        }
        if (canShoot && Input.GetKeyDown(KeyCode.J))
        {
            //gameObject.GetComponent<AudioSource>().Play();
            switch (playerMove.state)
            {
                case PlayerState.PlayerGround:
                    playerGround.Shoot(rigidBody.velocity.x,isTopKeyDown, isBottomKeyDown);
                    break;
                case PlayerState.PlayerJump:
                    playerJump.Shoot(rigidBody.velocity.x, isTopKeyDown, isBottomKeyDown);
                    break;
                case PlayerState.PlayerDown:
                    playerDown.Shoot(rigidBody.velocity.x, isTopKeyDown, isBottomKeyDown);
                    break;

            }
        }
    }
}