using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Suctionator : MonoBehaviour
{
    [SerializeField] MilkManager _milkManager;
    public float fireRate = 1.1f;
    public float nextFire = 1.25f;
    public bool hasCow = false;
    public CowBase _cow;

    private void Start()
    {
        GetCow();
    }

    private void Update()
    {
        if (hasCow)
        {
            Suck();
        }
    }

    private void Suck()
    {
        if (hasCow && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            
            _cow.Milk();
        }
    }

    private void GetCow()
    {
        foreach (GameObject Go in _milkManager.cowList)
        {
            if (Go.GetComponent<CowBase>().hasSucker == true)
            {
                continue;
            }
            
            Go.GetComponent<CowBase>().hasSucker = true;
            _cow = Go.GetComponent<CowBase>();
            hasCow = true;
            return;
        }
    }
}
