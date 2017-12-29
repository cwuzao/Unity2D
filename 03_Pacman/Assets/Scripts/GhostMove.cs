using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMove : MonoBehaviour {
    public GameObject[] wayPointsGos;
    public float speed = 0.2f;

    private Vector3 startPos;
    private List<Vector3> wayPoints = new List<Vector3>();
    private int index = 0;
    private Rigidbody2D _Rigidbody2D;
    private Animator _Animator;

    private void Awake()
    {
        _Rigidbody2D = GetComponent<Rigidbody2D>();
        _Animator = GetComponent<Animator>();
    }

    private void Start()
    {
        startPos = transform.position + new Vector3(0, 3, 0);
        LoadAPath(wayPointsGos[
            GameManager.Instance.usingIndex[GetComponent<SpriteRenderer>().sortingOrder - 2]]);
    }

    private void FixedUpdate()
    {
        if (transform.position != wayPoints[index])
        {
            Vector2 temp = Vector2.MoveTowards(transform.position, wayPoints[index], speed);
            _Rigidbody2D.MovePosition(temp);
        }
        else
        {
            index++;
            if (index >= wayPoints.Count)
            {
                index = 0;
                LoadAPath(wayPointsGos[Random.Range(0, wayPointsGos.Length)]);
            }
        }
        Vector2 dir = wayPoints[index] - transform.position;
        _Animator.SetFloat("DirX", dir.x);
        _Animator.SetFloat("DirY", dir.y);
    }

    private void LoadAPath(GameObject go)
    {
        wayPoints.Clear();
        foreach (Transform T in go.transform)
        {
            wayPoints.Add(T.position);
        }
        wayPoints.Insert(0, startPos);
        wayPoints.Add(startPos);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Pacman")
        {
            if (GameManager.Instance.isSupperPacman)
            {
                transform.position = startPos - new Vector3(0, 3, 0);
                index = 0;
                GameManager.Instance.score += 500;
            }
            else
            {
                GameManager.Instance.gamePanel.SetActive(false);
                collision.gameObject.SetActive(false);
                Instantiate(GameManager.Instance.gameoverPrefab);
                Invoke("Restart",3f);
            }
        }
    }
    private void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
