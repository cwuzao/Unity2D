using UnityEngine;

public class Ef_AutoRotate:MonoBehaviour
{
    public float speed = 10f;

    private void Update()
    {
        transform.Rotate(Vector3.forward, speed * Time.deltaTime);
    }
}
