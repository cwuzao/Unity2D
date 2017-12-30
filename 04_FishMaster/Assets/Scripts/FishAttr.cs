using UnityEngine;

public class FishAttr : MonoBehaviour
{ 
    public int hp;
    public int exp;
    public int gold;
    public int maxNum;
    public int maxSpeed;

    public GameObject goldPrefab;
    public GameObject diePrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Border")
        {
            DestroyObject(gameObject);
        }
    }

    void TakeDamage(int value)
    {
        hp -= value;
        if (hp <= 0)
        {
            GameController.Instance.gold += gold;
            GameController.Instance.exp += exp;
            GameObject die = Instantiate(diePrefab);

            die.transform.SetParent(gameObject.transform.parent, false);
            die.transform.position = transform.position;
            die.transform.rotation = transform.rotation;

            GameObject goldGo = Instantiate(goldPrefab);
            goldGo.transform.SetParent(gameObject.transform.parent, false);
            goldGo.transform.position = transform.position;
            goldGo.transform.rotation = transform.rotation;

            if (gameObject.GetComponent<Ex_PlayEffect>() != null)
            {
                AudioManager.Instance.PlayEffectSound(AudioManager.Instance.rewardClip);
                gameObject.GetComponent<Ex_PlayEffect>().PlayEffect();
            }

            Destroy(gameObject);
        }
    }
}


