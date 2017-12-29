using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacdot : MonoBehaviour {

    public bool isSuppor;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Pacman")
        {
            if (isSuppor)
            {
                GameManager.Instance.OnEatSuperPacdot();
            }
            GameManager.Instance.OnEatPacdot(gameObject);
            Destroy(gameObject);

        }
    }
}
