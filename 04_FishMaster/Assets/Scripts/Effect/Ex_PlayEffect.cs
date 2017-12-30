using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ex_PlayEffect : MonoBehaviour {

    public GameObject[] effectPrefabs;

    public void PlayEffect()
    {
        foreach (var item in effectPrefabs)
        {
            Instantiate(item);
        }
    }
}
