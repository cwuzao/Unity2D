using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

//test github
public class SnakeHead : MonoBehaviour {

    public List<Transform> bodyList = new List<Transform>();
    public float velocity = 0.35f;
    public int step = 30;
    private int x;
    private int y;
    private Vector3 headPos;
    public GameObject bodyPrefab;
    public GameObject dieEffect;
    private bool isDie = false;
    public AudioClip eatClip;
    public AudioClip dieClip;

    public Sprite[] bodySprites = new Sprite[2];
    private Transform canvas;

    void Awake()
    {
        canvas = GameObject.Find("Canvas").transform;
        gameObject.GetComponent<Image>().sprite =
            Resources.Load<Sprite>(PlayerPrefs.GetString("sh", "sh02"));
        bodySprites[0] =
            Resources.Load<Sprite>(PlayerPrefs.GetString("sb01", "sb0201"));
        bodySprites[1] =
            Resources.Load<Sprite>(PlayerPrefs.GetString("sb02", "sb0202"));
    }
    void Start()
    {
        InvokeRepeating("Move", 0, velocity);
        x = 0;
        y = step;
    }

    void Update()
    {
        if (!isDie)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !MainUIController.Instance.isPause)
            {
                CancelInvoke();
                InvokeRepeating("Move", 0, velocity - 0.2f);
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                CancelInvoke();
                InvokeRepeating("Move", 0, velocity);
            }
            if (Input.GetKey(KeyCode.W) && y != -step)
            {
                gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
                x = 0;
                y = step;
            }
            else if (Input.GetKey(KeyCode.S) && y != step)
            {
                gameObject.transform.localRotation = Quaternion.Euler(0, 0, 180);
                x = 0;
                y = -step;
            }
            else if (Input.GetKey(KeyCode.A) && x != step)
            {
                gameObject.transform.localRotation = Quaternion.Euler(0, 0, 90);
                x = -step;
                y = 0;
            }
            else if (Input.GetKey(KeyCode.D) && x != -step)
            {
                gameObject.transform.localRotation = Quaternion.Euler(0, 0, -90);
                x = step;
                y = 0;
            }
        }
    }
    void Move()
    {
        headPos = gameObject.transform.localPosition;
        gameObject.transform.localPosition = new Vector3(headPos.x + x, headPos.y + y);
        if (bodyList.Count > 0)
        {
            //bodyList.Last().localPosition = headPos;
            //bodyList.Insert(0, bodyList.Last());
            //bodyList.RemoveAt(bodyList.Count - 1);
            for (int i = bodyList.Count()-2; i >= 0; i--)
            {
                bodyList[i + 1].localPosition = bodyList[i].localPosition;
            }
            bodyList[0].localPosition = headPos;
        }
    }

    void Grow()
    {
        AudioSource.PlayClipAtPoint(eatClip, Vector3.zero);
        GameObject body = Instantiate(bodyPrefab, new Vector3(2000, 2000, 0), Quaternion.identity);
        body.GetComponent<Image>().sprite = bodySprites[bodyList.Count % 2];
        body.transform.SetParent(canvas, false);
        bodyList.Add(body.transform);
    }

    void Die()
    {
        AudioSource.PlayClipAtPoint(dieClip, Vector3.zero);
        CancelInvoke();
        isDie = true;
        Instantiate(dieEffect);
        PlayerPrefs.SetInt("lastl", MainUIController.Instance.length);
        PlayerPrefs.SetInt("lasts", MainUIController.Instance.score);
        if (PlayerPrefs.GetInt("bests", 0) < MainUIController.Instance.score)
        {
            PlayerPrefs.SetInt("bestl", MainUIController.Instance.length);
            PlayerPrefs.SetInt("bests", MainUIController.Instance.score);
        }
        StartCoroutine(GameOver(1.5f));
    }

    IEnumerator GameOver(float t)
    {
        yield return new WaitForSeconds(t);
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Food"))
        {
            Destroy(collision.gameObject);
            MainUIController.Instance.UpdateUI();
            Grow();
            FoodMaker.Instance.MakeFood(Random.Range(0, 100) < 20);
        }
        else if (collision.gameObject.CompareTag("Reward"))
        {
            Destroy(collision.gameObject);
            MainUIController.Instance.UpdateUI(Random.Range(5,15)*10);
            Grow();
        }
        else if (collision.gameObject.CompareTag("Body"))
        {
            Die();
        }
        else
        {
            if (MainUIController.Instance.hasBorder)
            {
                Die();
            }
            else
            {
                switch (collision.gameObject.name)
                {
                    case "Up":
                        transform.localPosition = new Vector3(transform.localPosition.x,
                            -transform.localPosition.y + 30, transform.localPosition.z);
                        break;
                    case "Down":
                        transform.localPosition = new Vector3(transform.localPosition.x,
                            -transform.localPosition.y - 30, transform.localPosition.z);
                        break;
                    case "Left":
                        transform.localPosition = new Vector3(-transform.localPosition.x + 180,
                            transform.localPosition.y, transform.localPosition.z);
                        break;
                    case "Right":
                        transform.localPosition = new Vector3(-transform.localPosition.x + 240,
                            transform.localPosition.y + 30, transform.localPosition.z);
                        break;
                }
            }
        }
    }
}
