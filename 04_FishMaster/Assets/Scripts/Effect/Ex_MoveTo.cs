using UnityEngine;


public class Ex_MoveTo : MonoBehaviour
{
    private GameObject goldCollect;

    void Start()
    {
        goldCollect = GameObject.Find("GoldCollect");
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position,
            goldCollect.transform.position, 50 * Time.deltaTime);
    }
}

