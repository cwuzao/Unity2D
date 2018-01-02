using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public int damage = 50;
    public float speed = 60f;
    private float distanceArriveTarget = 1.2f;
    private Transform target;
    public GameObject explosionEffectPrefab;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (target == null)
        {
            Die();
            return;
        }
        transform.LookAt(target.position);
        transform.Translate(Vector3.forward 
            * speed * Time.deltaTime);

        Vector3 dir = target.position - transform.position;
        if (dir.magnitude <= distanceArriveTarget)
        {
            target.GetComponent<Enemy>().TakeDamage(damage);
            GameObject effect=  GameObject.Instantiate(explosionEffectPrefab,
                transform.position,
                transform.rotation);
            Destroy(effect, 1);
            Destroy(gameObject);
        }
	}

    public void SetTarget(Transform _target)
    {
        this.target = _target;
    }

    void Die()
    {
        GameObject effect = GameObject.Instantiate(explosionEffectPrefab,
            transform.position,
            transform.rotation);
        Destroy(effect, 1);
        Destroy(gameObject);
    }

}
