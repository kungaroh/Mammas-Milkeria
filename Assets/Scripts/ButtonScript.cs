using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class ButtonScript : MonoBehaviour
{
    [SerializeField] private UdderSpawner _udderSpawner;
    [SerializeField] private MilkManager _milkManager;
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private GameObject cowPrefab;
    [SerializeField] private GameObject vanillaCowPrefab;
    [SerializeField] private GameObject strawberryCowPrefab;
    [SerializeField] private GameObject chocolateCowPrefab;
    [SerializeField] private GameObject suckerPrefab;
    private int _upgradeCost;
    public void SellMilk()
    {
        _milkManager.SellMilk();
    }

    public void BuyTeat()
    {
        _upgradeCost = _milkManager.GetCost("teat");
        
        if (_milkManager.money < _upgradeCost)
        {
            _uiManager.StartCoroutine(_uiManager.NotEnoughMoney());
            return;
        }

        if (_udderSpawner.GetTeetCount() > 5)
        {
            _uiManager.UpdateCost("teatMax");
            return;
        }

        if (_udderSpawner.GetTeetCount() == 5)
        {
            _uiManager.UpdateCost("teatMax");
            _udderSpawner.AddTeet();
            _milkManager.money -= _upgradeCost;
            return;
        }
        _udderSpawner.AddTeet();
        _milkManager.IncrementCost("teat");
        _uiManager.UpdateCost("teat");
        _milkManager.money -= _upgradeCost;
    }
    
    //Upgrades
    public void BuySuctionator()
    {
        _upgradeCost = _milkManager.GetCost("sucker");
        if (_milkManager.money < _upgradeCost)
        {
            _uiManager.StartCoroutine(_uiManager.NotEnoughMoney());
            return;
        }
        
        _milkManager.IncrementCost("sucker");
        _uiManager.UpdateCost("sucker");
        _milkManager.suckList.Add(Instantiate(suckerPrefab));
        _milkManager.money -= _upgradeCost;
    }

    public void FasterSuctionator()
    {
        int suckersFastered = 0;
        
        _upgradeCost = _milkManager.GetCost("fasterSucker");
        if (_milkManager.money < _upgradeCost)
        {
            _uiManager.StartCoroutine(_uiManager.NotEnoughMoney());
            return;
        }
        
        Suctionator succ;
        for (int i = _milkManager.suckList.Count ; i > 0; i--)
        {
            succ = _milkManager.suckList[i-1].GetComponent<Suctionator>();
            if (succ.fireRate < 0.4f && succ.hasCow)
            {
                _milkManager.suckList.Remove(succ.gameObject);
                continue;
            }
            if (succ.fireRate < 0.4f)
            {
                continue;
            }
            succ.fireRate -= 0.2f;
            suckersFastered++;
        }

        if (suckersFastered > 0)
        {
            _milkManager.money -= _upgradeCost;
            _milkManager.IncrementCost("fasterSucker");
            _uiManager.UpdateCost("fasterSucker");
        }
    }

    public void BiggerCows()
    {
        _upgradeCost = _milkManager.GetCost("biggerCows");
        if (_milkManager.money < _upgradeCost)
        {
            _uiManager.StartCoroutine(_uiManager.NotEnoughMoney());
            return;
        }
        
        CowBase cow;
        for (int i = _milkManager.cowList.Count; i > 0; i--)
        {
            cow = _milkManager.cowList[i-1].GetComponent<CowBase>();
            if (cow.GetMilkOutAmount() > 4 && cow.hasSucker)
            {
                _milkManager.cowList.Remove(cow.gameObject);
                continue;
            }
            if (cow.GetMilkOutAmount() > 4)
            {
                continue;
            }
            cow.IncreaseMilkOutAmount(1);
        }

        _udderSpawner.IncreaseMilkOutAmount(1);
        
        _milkManager.IncrementCost("biggerCows");
        _uiManager.UpdateCost("biggerCows");
        _milkManager.money -= _upgradeCost;
    }
    
    //Buy More Cows

    public void BuyFriesian()
    {
        _upgradeCost = _milkManager.GetCost("friesian");
        if (_milkManager.money < _upgradeCost)
        {
            _uiManager.StartCoroutine(_uiManager.NotEnoughMoney());
            return;
        }
        _milkManager.cowList.Add(Instantiate(cowPrefab));
        _milkManager.IncrementCost("friesian");
        _uiManager.UpdateCost("friesian");
        _milkManager.money -= _upgradeCost;
    }
    
    public void BuyVanilla()
    {
        _upgradeCost = _milkManager.GetCost("vanilla");
        if (_milkManager.money < _upgradeCost)
        {
            _uiManager.StartCoroutine(_uiManager.NotEnoughMoney());
            return;
        }
        _milkManager.cowList.Add(Instantiate(vanillaCowPrefab));
        _milkManager.IncrementCost("vanilla");
        _uiManager.UpdateCost("vanilla");
        _milkManager.money -= _upgradeCost;
        
    }
    
    public void BuyStrawberry()
    {
        _upgradeCost = _milkManager.GetCost("strawberry");
        if (_milkManager.money < _upgradeCost)
        {
            _uiManager.StartCoroutine(_uiManager.NotEnoughMoney());
            return;
        }
        _milkManager.cowList.Add(Instantiate(strawberryCowPrefab));
        _milkManager.IncrementCost("strawberry");
        _uiManager.UpdateCost("strawberry");
        _milkManager.money -= _upgradeCost;
    }
    
    public void BuyChocolate()
    {
        _upgradeCost = _milkManager.GetCost("chocolate");
        if (_milkManager.money < _upgradeCost)
        {
            _uiManager.StartCoroutine(_uiManager.NotEnoughMoney());
            return;
        }
        _milkManager.cowList.Add(Instantiate(chocolateCowPrefab));
        _milkManager.IncrementCost("chocolate");
        _uiManager.UpdateCost("chocolate");
        _milkManager.money -= _upgradeCost;
    }
    
    public void Prestige()
    {
        _upgradeCost = _milkManager.GetCost("prestige");
        if (_milkManager.money < _upgradeCost)
        {
            _uiManager.StartCoroutine(_uiManager.NotEnoughMoney());
            return;
        }
        //Prestige
        
        StartCoroutine(ResetLevel());
        _uiManager.UpdateCost("prestige");
        _milkManager.money -= _upgradeCost;

    }

    private IEnumerator ResetLevel()
    {
         yield return new WaitForEndOfFrame();
         _milkManager.ResetObject(_milkManager.prestigeLevel+1);
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
}
