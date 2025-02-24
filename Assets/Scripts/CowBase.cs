using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CowBase : MonoBehaviour
{
    [SerializeField] public MilkManager _milkManager;
    public string cowType;
    private int _milkOutAmount = 1;
    public bool hasSucker = false;

    private void Start()
    {
        GetSucker();
    }

    public void Milk()
    {
        switch (cowType.ToLower())
        {
            case "vanilla":
                _milkManager.vanillaMilkAmount += _milkOutAmount;
                break;
            case "strawberry":
                _milkManager.strawberryMilkAmount += _milkOutAmount;
                break;
            case "chocolate":
                _milkManager.chocolateMilkAmount += _milkOutAmount;
                break;
            case "friesian":
                _milkManager.milkAmount += _milkOutAmount;
                break;
            default:
                _milkManager.milkAmount += _milkOutAmount;
                break;
        }
    }

    private void GetSucker()
    {
        Suctionator succ;
        foreach (GameObject Go in _milkManager.suckList)
        {
            
            try
            {
                 succ = Go.GetComponent<Suctionator>();
            }
            catch
            {
                continue;
            }
            
            if (succ.hasCow == true)
            {
                continue;
            }
            
            succ._cow = this;
            succ.hasCow = true;
            hasSucker = true;
            return;
        }
    }

    public void IncreaseMilkOutAmount(int num)
    {
        _milkOutAmount += num;
    }

    public int GetMilkOutAmount()
    {
        return _milkOutAmount;
    }
    
}
