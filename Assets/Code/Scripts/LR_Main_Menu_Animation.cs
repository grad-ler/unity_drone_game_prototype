using System;
using System.Collections.Generic;
using UnityEngine;

public class LR_Main_Menu_Animation : MonoBehaviour
{
    [Header("Propeller Properties")] 
    [SerializeField] private List<Transform> propellers;
    [SerializeField] private float propRotSpeed = 3000f;

    private void Update()
    {
        if (propellers == null || propellers.Count == 0)
        {
            return;
        }

        foreach (Transform propeller in propellers)
        {
            propeller.Rotate(Vector3.forward, propRotSpeed * Time.deltaTime);
        }
    }
}