using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public float moveSpeed = 10;
    public bool isPlayerBullect;

    // Update is called once per frame
    void Update() {
        transform.Translate(transform.up * moveSpeed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Tank":
                if (!isPlayerBullect)
                {
                    collision.SendMessage("Die");
                    Destroy(gameObject);
                }
                break;
            case "Heart":
                collision.SendMessage("Die");
                Destroy(gameObject);

                break;
            case "Enemy":
                if (isPlayerBullect)
                {
                    PlayerManager.Instance.playerScore++;
                    collision.SendMessage("Die");
                    Destroy(gameObject);
                }
                break;
            case "Wall":
                Destroy(collision.gameObject);
                Destroy(gameObject);
                break;
            case "Barrier":
                if (isPlayerBullect)
                {
                    collision.SendMessage("PlayAudio");
                }
                Destroy(gameObject);
                break;
            case "AirBarrier":
                if (isPlayerBullect)
                {
                    collision.SendMessage("PlayAudio");
                }
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }

}
