using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreation : MonoBehaviour {
    //0.Heart 1.Wall 2.barrier  3.bron 4.river 5.grass 6.airbarrier
    public GameObject[] item;
    public List<Vector3> itemPositionList = new List<Vector3>();


    private void Awake()
    {
        InitMap();
    }

    private void InitMap()
    {
        //Heart
        CreateItem(item[0], new Vector3(0, -8, 0), Quaternion.identity);
        //Wall defend Heart
        CreateItem(item[1], new Vector3(-1, -8, 0), Quaternion.identity);
        CreateItem(item[1], new Vector3(1, -8, 0), Quaternion.identity);
        CreateItem(item[1], new Vector3(-1, -7, 0), Quaternion.identity);
        CreateItem(item[1], new Vector3(0, -7, 0), Quaternion.identity);
        CreateItem(item[1], new Vector3(1, -7, 0), Quaternion.identity);
        //AirBarrier
        for (int i = -11; i < 12; i++)
        {
            CreateItem(item[6], new Vector3(i, 9, 0), Quaternion.identity);
        }
        for (int i = -11; i < 12; i++)
        {
            CreateItem(item[6], new Vector3(i, -9, 0), Quaternion.identity);
        }
        for (int i = -8; i < 9; i++)
        {
            CreateItem(item[6], new Vector3(-11, i, 0), Quaternion.identity);
        }
        for (int i = -8; i < 9; i++)
        {
            CreateItem(item[6], new Vector3(11, i, 0), Quaternion.identity);
        }

        //Other map
        for (int i = 0; i < 60; i++)
        {
            CreateItem(item[1], CreateRandomPosition(), Quaternion.identity);
        }
        for (int i = 0; i < 20; i++)
        {
            CreateItem(item[2], CreateRandomPosition(), Quaternion.identity);
        }
        for (int i = 0; i < 20; i++)
        {
            CreateItem(item[4], CreateRandomPosition(), Quaternion.identity);
        }
        for (int i = 0; i < 20; i++)
        {
            CreateItem(item[5], CreateRandomPosition(), Quaternion.identity);
        }

        //Player and Enemy
        GameObject go = Instantiate(item[3], new Vector3(-2, -8, 0), Quaternion.identity);
        go.GetComponent<Born>().isCreatePlayer = true;

        CreateItem(item[3], new Vector3(-10, 8, 0), Quaternion.identity);
        CreateItem(item[3], new Vector3(0, 8, 0), Quaternion.identity);
        CreateItem(item[3], new Vector3(10, 8, 0), Quaternion.identity);
        InvokeRepeating("CreateEnemy", 4, 5);
    }

    private void CreateItem(GameObject craeteGameObject,
        Vector3 createPosition,Quaternion createRotation)
    {
        GameObject itemGo = Instantiate(craeteGameObject, createPosition, createRotation);
        itemGo.transform.SetParent(gameObject.transform);
        itemPositionList.Add(createPosition);
    }

    private Vector3 CreateRandomPosition()
    {
        while (true)
        {
            Vector3 createPosition = new Vector3(Random.Range(-9, 10), Random.Range(-7, 8), 0);
            if (!HasThePosition(createPosition))
            {
                return createPosition;
            }
        }
    }

    private bool HasThePosition(Vector3 createPos)
    {
        for (int i = 0; i < itemPositionList.Count; i++)
        {
            if (createPos == itemPositionList[i])
            {
                return true;
            }
        }
        return false;
    }

    private void CreateEnemy()
    {
        int num = Random.Range(0, 3);
        Vector3 enemyPos = new Vector3();
        if (num == 0)
        {
            enemyPos = new Vector3(-10, 8, 0);
        }
        else if (num == 1)
        {
            enemyPos = new Vector3(0, 8, 0);
        }
        else
        {
            enemyPos = new Vector3(10, 8, 0);
        }
        CreateItem(item[3], enemyPos, Quaternion.identity);
    }
}
