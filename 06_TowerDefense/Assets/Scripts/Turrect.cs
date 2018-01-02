using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turrect : MonoBehaviour {


    private List<GameObject> enemys = new List<GameObject>();
    public  float attackRateTime = 1 ;
    public GameObject bulletPrefab;
    public Transform firePosition;
    public Transform head;
    public bool useLaser = false;
    public float damageRate = 70;
    public LineRenderer laserRenderer;
    public GameObject laserEffect;

    private float timer = 0;

    private void Start()
    {
        timer = attackRateTime;
    }

    private void Update()
    {
        if (enemys.Count > 0 && enemys[0] != null)
        {
            Vector3 targetPosition =
                enemys[0].transform.position;
            targetPosition.y = head.position.y;
            head.LookAt(targetPosition);
        }
        if (!useLaser)
        {
            timer += Time.deltaTime;
            if (enemys.Count > 0 && timer >= attackRateTime)
            {                                             
                timer = 0;
                Attack();
            }
        }
        else if(enemys.Count>0)
        {
            if (enemys[0] == null)
            {
                UpdateEnemys();
            }
            if (enemys.Count > 0)
            {
                if (!laserRenderer.enabled)
                    laserRenderer.enabled = true;
                laserEffect.SetActive(true);
                laserRenderer.SetPositions(
                    new Vector3[]{firePosition.position,
                    enemys[0].transform.position });
                enemys[0].GetComponent<Enemy>().TakeDamage(
                    damageRate * Time.deltaTime);

                laserEffect.transform.position =
                    enemys[0].transform.position;
                Vector3 pos = transform.position;
                pos.y = enemys[0].transform.position.y;
                laserEffect.transform.LookAt(pos);  
            }
        }
        else
        {
            laserEffect.SetActive(false);
            laserRenderer.enabled = false;
        }
    }

    void Attack()
    {
        if (enemys[0] == null)
        {
            UpdateEnemys();
        }
        if (enemys.Count > 0)
        {
            GameObject bullet = GameObject.Instantiate(
                bulletPrefab,
                firePosition.position,
                firePosition.rotation);
            bullet.GetComponent<Bullet>().SetTarget(enemys[0].transform);
        }
        else
        {
            timer = attackRateTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemys.Add(other.gameObject);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemys.Remove(other.gameObject);
        }
    }

    private void UpdateEnemys()
    {
        List<int> emptyIndex = new List<int>();
        for (int index = 0; index < enemys.Count; index++)
        {
            if (enemys[index] == null)
            {
                emptyIndex.Add(index);
            }
        }
        for (int i = 0; i < emptyIndex.Count; i++)
        {
            enemys.RemoveAt(emptyIndex[i] - i);
        }
    }


}
