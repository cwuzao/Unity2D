using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour {

    private DialogManager DM;

    private void Awake()
    {
        initComponent();
        Dialoguer.Initialize();
    }

    void initComponent()
    {
        GameObject player = GameObject.Find("Player").gameObject;
        DM = this.GetComponent<DialogManager>();
    }

}
