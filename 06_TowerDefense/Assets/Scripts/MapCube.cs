using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapCube : MonoBehaviour {
   
    [HideInInspector]
    public GameObject turretGo;
    [HideInInspector]
    public TurretData turretData;
    [HideInInspector]
    public bool isUpgraded;

    public GameObject buildefffect;
    private Renderer render;

    private void Start()
    {
        render = GetComponent<Renderer>();
    }

    internal void BuildTurret(TurretData turretData)
    {
        this.turretData = turretData;
        isUpgraded = false;
        turretGo = GameObject.Instantiate(turretData.turretPrefab,
             transform.position,
             Quaternion.identity);
        var effect = GameObject.Instantiate(buildefffect,
             transform.position,
             Quaternion.identity);
        Destroy(effect, 1f);
    }

    private void OnMouseEnter()
    {
        if (turretGo == null &&
            !EventSystem.current.IsPointerOverGameObject())
        {
            render.material.color = Color.red;
        }
    }

    private void OnMouseExit()
    {
        render.material.color = Color.white;
    }

    public void UpgradeTurret()
    {
        if (isUpgraded == true) return;
        Destroy(turretGo);
        isUpgraded = true;
        turretGo = GameObject.Instantiate(turretData.turretUpgradePrefab,
             transform.position,
             Quaternion.identity);
        var effect = GameObject.Instantiate(buildefffect,
            transform.position,
            Quaternion.identity);
        Destroy(effect, 1.5f);
    }

    public void DestroyTurret()
    {
        var effect = GameObject.Instantiate(buildefffect,
            transform.position,
            Quaternion.identity);
        Destroy(effect, 1f);
        Destroy(turretGo);
        isUpgraded = false;
        turretGo = null;
        turretData = null;

    }
}
