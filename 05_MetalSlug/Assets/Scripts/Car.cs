using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Car : MonoBehaviour {

    public Vector3 targetPos;
    public Vector3 endTargetPos;
    public float smoothing = 2;
    public Wheel[] wheelArray;
    public Damper damper;

    private bool isReaching = false;

    private void Start()
    {
        Invoke("PlaySound",0.3f);
    }

    // Update is called once per frame
    void Update () {
        transform.position = Vector3.Lerp(
            transform.position,
            targetPos,
            smoothing*Time.deltaTime);
        if (isReaching == false)
        {
            if (Vector3.Distance(transform.position, targetPos) < 0.4f) {
                isReaching = true;
                OnReach();
            }
        }
	}

    private void OnReach()
    {
        foreach (var item in wheelArray)
        {
            item.Stop();
        }
        damper.StartRotate();
        Invoke("GoOut", 1f);
    }

    void PlaySound()
    {
        transform.GetComponent<AudioSource>().Play();
    }

    void GoOut() {
        targetPos = endTargetPos;
        foreach (var item in wheelArray)
        {
            item.Start();
        }
        Destroy(this.gameObject,1f);
    }
}
