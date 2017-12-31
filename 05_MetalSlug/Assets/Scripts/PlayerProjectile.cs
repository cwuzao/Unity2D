using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public float speed = 10;

    private void Start()
    {
        Destroy(gameObject, 3f);
    }

    private void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
}