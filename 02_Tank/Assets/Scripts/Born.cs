using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Born : MonoBehaviour {

    public GameObject playerPrefab;
    public GameObject[] enemyPrefablist;
    public bool isCreatePlayer;

	// Use this for initialization
	void Start () {
        Invoke("BornTank", 1f);
        Destroy(gameObject, 1f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void BornTank()
    {
        if (isCreatePlayer)
        {
            Instantiate(playerPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            int num = Random.Range(0, enemyPrefablist.Length);
            Instantiate(enemyPrefablist[num], transform.position, Quaternion.identity);
        }
    }
}
