using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDown : MonoBehaviour {

    public SpriteRenderer upRenderer;
    public SpriteRenderer downRenderer;
    public Sprite idleDownSprite;
    public float animSpeed = 10;
    public Sprite[] idleUpSpriteArray;
    public Sprite[] walkUpSpriteArray;
    public Sprite[] walkDownSpriteArray;
    public AnimStatus status = AnimStatus.Idle;

    private int idleUpIndex = 0;
    private int idleUpLength = 0;
    private float idleUptimer = 0;
    private int walkUpIndex = 0;
    private int walkUpLength = 0;
    private float walkUptimer = 0;
    private int walkDownIndex = 0;
    private int walkDownLength = 0;
    private float walkDowntimer = 0;
    private float animTimeInterval = 0;



    // Use this for initialization
    void Start()
    {
        animTimeInterval = 1 / animSpeed;
        idleUpLength = idleUpSpriteArray.Length;
        walkUpLength = walkUpSpriteArray.Length;
        walkDownLength = walkDownSpriteArray.Length;
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
                downRenderer.sprite = idleDownSprite;
                //downRenderer.sprite = idleDownSprite;
                break;
            case AnimStatus.Walk:
                walkUptimer += Time.deltaTime;
                if (walkUptimer > animTimeInterval)
                {
                    walkUptimer -= animTimeInterval;
                    walkUpIndex++;
                    walkUpIndex %= walkUpLength;
                    upRenderer.sprite = walkUpSpriteArray[walkUpIndex];
                }

                walkDowntimer += Time.deltaTime;
                if (walkDowntimer > animTimeInterval)
                {
                    walkDowntimer -= animTimeInterval;
                    walkDownIndex++;
                    walkDownIndex %= walkDownLength;
                    downRenderer.sprite = walkDownSpriteArray[walkDownIndex];
                }

                break;
        }
    }

    public void Shoot(float v_h,bool isTopKeyDown, bool isBottomKeyDown)
    {

    }
}

