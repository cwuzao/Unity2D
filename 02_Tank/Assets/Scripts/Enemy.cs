using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {


    public float moveSpeed = 3;
    private Vector3 bullectEulerAngles;
    private float v=-1;
    private float h;

    private SpriteRenderer sr;
    public Sprite[] tankSprite;
    public GameObject bullectPrefab;
    public GameObject explosionPrefab;

    private float timeVal;
    private float timeValChangeDirection = 0;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (timeVal >= 3f)
        {
            Attack();
        }
        else
        {
            timeVal += Time.fixedDeltaTime;
        }

        Move();
    }

    private void Attack()
    {
        Instantiate(bullectPrefab, transform.position,
            Quaternion.Euler(transform.eulerAngles + bullectEulerAngles));
        timeVal = 0;
    }

    private void Move()
    {
        if (timeValChangeDirection >= 4)
        {
            int num = Random.Range(0, 8);
            if (num > 5)
            {
                v = -1;
                h = 0;
            }
            else if (num == 0)
            {
                v = 1;
                h = 0;
            }
            else if (num > 0 && num <= 2)
            {
                h = -1;
                v = 0;
            }
            else if (num > 2 && num <= 4)
            {
                h = 1;
                v = 0;
            }
            timeValChangeDirection = 0;
        }
        else
        {
            timeValChangeDirection += Time.fixedDeltaTime;
        }
        
        transform.Translate(Vector3.up * v * moveSpeed * Time.fixedDeltaTime,
            Space.World);
        if (v < 0)
        {
            sr.sprite = tankSprite[2];
            bullectEulerAngles = new Vector3(0, 0, 180);
        }
        else if (v > 0)
        {
            sr.sprite = tankSprite[0];
            bullectEulerAngles = new Vector3(0, 0, 0);
        }
        if (v == 0)
        {
            transform.Translate(Vector3.right * h * moveSpeed * Time.fixedDeltaTime,
                Space.World);
            if (h < 0)
            {
                sr.sprite = tankSprite[3];
                bullectEulerAngles = new Vector3(0, 0, 90);
            }
            else if (h > 0)
            {
                sr.sprite = tankSprite[1];
                bullectEulerAngles = new Vector3(0, 0, -90);
            }
        }
    }

    private void Die()
    {
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            timeValChangeDirection = 4;
        }
    }
}
