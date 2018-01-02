using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

    public float speed = 10;
    public float hp = 150;
    private float totalHP;
    private Transform[] positions;
    private int index = 0;
    public GameObject explosionEffect;
    public Slider hpSlider;
                                    
	// Use this for initialization
	void Start () {
        positions = WayPoints.positions;
        totalHP = hp;

    }
	
	// Update is called once per frame
	void Update () {
        Move();
	}

    private void Move()
    {
        if (index > positions.Length - 1) return;
        transform.Translate((positions[index].position
            - transform.position).normalized 
            * Time.deltaTime * speed);
        if (Vector3.Distance(positions[index].position, 
            transform.position) < 0.2f)
        {
            index++;
        }
        if (index > positions.Length - 1)
        {
            ReachDestination();
        }
    }

    internal void TakeDamage(float damage)
    {
        if (hp < 0) return;
        hp -= damage;
        hpSlider.value = hp / totalHP;
        if (hp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
       GameObject effect =  GameObject.Instantiate(
           explosionEffect,
           transform.position,
           transform.rotation
           );
        Destroy(effect, 1.5f);
        Destroy(this.gameObject);
    }

    private void ReachDestination()
    {
        GameManager.Instance.Failed();
        GameObject.Destroy(gameObject);

    }

    private void OnDestroy()
    {
        EnemySpawner.CountEnemyAlive--;
    }

}
