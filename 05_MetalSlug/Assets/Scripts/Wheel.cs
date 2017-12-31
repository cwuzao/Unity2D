using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour {

    public float speed = 450;
    private bool isRotate = true;
	
	// Update is called once per frame
	void Update () {
        if (isRotate)
        {
            transform.Rotate(-Vector3.forward * speed * Time.deltaTime);
        }
	}

    public void Stop()
    {
        isRotate = false;
    }

    public void Start()
    {
        isRotate = true ;
    }
}
