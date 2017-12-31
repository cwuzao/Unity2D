using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour {

    public SpriteRenderer upRenderer;
    public SpriteRenderer downRenderer;
    //public Sprite idleDownSprite;
    public float animSpeed = 7;
    public Sprite[] idleUpSpriteArray;
    public Sprite[] idleDownSpriteArray;
    public AnimStatus status = AnimStatus.Idle;
    public Sprite shootUpSprite;
    public Sprite shootHorizontalSprite;
    public GameObject projectilePrefab;
    public Transform shootupPos;
    public Transform shoothorizontalPos;

    private int idleUpIndex = 0;
    private int idleUpLength = 0;
    private float idleUptimer = 0;
    private int idleDownIndex = 0;
    private int idleDownLength = 0;
    private float idleDowntimer = 0;
    private float animTimeInterval = 0;
    private bool shoot = false;
    private ShootDir shootDir;



    // Use this for initialization
    void Start()
    {
        animTimeInterval = 1 / animSpeed;
        idleUpLength = idleUpSpriteArray.Length;
        idleDownLength = idleDownSpriteArray.Length;
    }

    // Update is called once per frame
    void Update()
    {
        switch (status)
        {
            case AnimStatus.Idle:
                idleUptimer += Time.deltaTime;
                if (idleUptimer > animTimeInterval)
                {
                    idleUptimer -= animTimeInterval;
                    idleUpIndex++;
                    idleUpIndex %= idleUpLength;
                    upRenderer.sprite = idleUpSpriteArray[idleUpIndex];
                }
                idleDowntimer += Time.deltaTime;
                if (idleDowntimer > animTimeInterval)
                {
                    idleDowntimer -= animTimeInterval;
                    idleDownIndex++;
                    idleDownIndex %= idleDownLength;
                    downRenderer.sprite = idleDownSpriteArray[idleDownIndex];
                }
                // downRenderer.sprite = idleDownSprite;
                break;
        }
    }

    private void LateUpdate()
    {
        if (shoot)
        {
            shoot = false;
            Vector3 pos = Vector3.zero;
            if (shootDir == ShootDir.Top)
            {
                pos = shootupPos.position;
            }
            else if (shootDir == ShootDir.Left || shootDir == ShootDir.RIght) {
                pos = shoothorizontalPos.position;
            }
            int z_rotataion = 0;
            switch (shootDir)
            {
                case ShootDir.Left:
                    upRenderer.sprite = shootHorizontalSprite;
                    z_rotataion = 180;
                    break;
                case ShootDir.RIght:
                    upRenderer.sprite = shootHorizontalSprite;
                    z_rotataion = 0;
                    break;
                case ShootDir.Top:
                    z_rotataion = 90;
                    upRenderer.sprite = shootUpSprite;
                    break;
                case ShootDir.Down:
                    z_rotataion = 270;
                    break;
                default:
                    break;
            }
            GameObject.Instantiate(
                projectilePrefab,
                pos,
                Quaternion.Euler(0, 0, z_rotataion));
           
        }
    }

    public void Shoot(float v_h,bool isTopKeyDown, bool isBottomKeyDown)
    {
        shoot = true;
        if (isTopKeyDown == false && isBottomKeyDown == false)
        {
            if (transform.localScale.x == 1)
            {
                shootDir = ShootDir.Left;
            }
            else if (transform.localScale.x == -1)
            {
                shootDir = ShootDir.RIght;
            }
        }
        else {
            if (isTopKeyDown)
            {
                shootDir = ShootDir.Top;
            }
            else if (isBottomKeyDown) {

                shootDir = ShootDir.Down;
            }
        }
    }
}


