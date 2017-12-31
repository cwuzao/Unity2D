using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damper : MonoBehaviour {
    public float speed = 200;

    private bool isRotate = false;
    private float angle = 0;

    private void Update()
    {
        if (isRotate)
        {
            angle += speed * Time.deltaTime;
            transform.Rotate(Vector3.forward * speed * Time.deltaTime);
            if (angle > 90)
            {
                isRotate = false;
            }
        }
    }

    public void StartRotate() {
        isRotate = true;
    }
}
