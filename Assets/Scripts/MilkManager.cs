using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/MilkManager")]
public class MilkManager : ScriptableObject
{
    private string _prefabName;
    
    public int milkValue;
    public int vanillaMilkValue;
    public int strawberryMilkValue;
    public int chocolateMilkValue;

    public int milkAmount;
    public int vanillaMilkAmount;
    public int strawberryMilkAmount;
    public int chocolateMilkAmount;
    public List<GameObject> cowList = new List<GameObject>();
    public List<GameObject> suckList = new List<GameObject>();
    
    public int prestigeLevel = 0;
    public float money;

    private int _suckerCost;
    private int _fasterSuckerCost;
    private int _biggerCowsCost;
    private int _moreTeetsCost;

    private int _friesianCost;
    private int _vanillaCost;
    private int _strawberryCost;
    private int _chocolateCost;
    private int _prestigeCost;
    
    private const int _baseMilkValue = 5;
    private const int _baseVanillaMilkValue = 8;
    private const int _baseStrawberryMilkValue = 12;
    private const int _baseChocolateMilkValue = 15;

    private const int _baseMoreTeetsCost = 60;
    private const int _baseSuckerCost = 100;
    private const int _baseFasterSuckerCost = 78;
    private const int _baseBiggerCowsCost = 238;
    private const int _baseFriesianCost = 150;
    private const int _baseVanillaCost = 1250;
    private const int _baseStrawberryCost = 1850;
    private const int _baseChocolateCost = 4000;
    private const int _basePrestigeCost = 1500000;
    
    [SerializeField] private int _timesTeatsBought = 0;
    [SerializeField] private int _timesSuckerBought = 0;
    [SerializeField] private int _timesFasterSuccBought = 0;
    [SerializeField] private int _timesBiggerCowBought = 0;
    [SerializeField] private int _timesFriesianBought = 0;
    [SerializeField] private int _timesVanillaBought = 0;
    [SerializeField] private int _timesStrawberryBought = 0;
    [SerializeField] private int _timesChocolateBought = 0;

    
    public void OnEnable()
    {
        milkValue = 5;
        vanillaMilkValue = 8;
        strawberryMilkValue = 12;
        chocolateMilkValue = 15;
        milkAmount = 0;
        vanillaMilkAmount = 0;
        strawberryMilkAmount = 0;
        chocolateMilkAmount = 0;
        _moreTeetsCost = _baseMoreTeetsCost * (prestigeLevel + 1);
        _suckerCost = _baseSuckerCost * (prestigeLevel +1);
        _fasterSuckerCost = _baseFasterSuckerCost * (prestigeLevel +1);
        _biggerCowsCost = _baseBiggerCowsCost * (prestigeLevel +1);
        _friesianCost = _baseFriesianCost * (prestigeLevel +1);
        _vanillaCost = _baseVanillaCost * (prestigeLevel +1);
        _strawberryCost = _baseStrawberryCost * (prestigeLevel +1);
        _chocolateCost = _baseChocolateCost * (prestigeLevel +1);
        _prestigeCost = _basePrestigeCost * (prestigeLevel +1);
    }

    public void ResetObject(int _prestigeLevel)
    {
        prestigeLevel = _prestigeLevel;
        
        milkValue = _baseMilkValue * (prestigeLevel +1);
        vanillaMilkValue = _baseVanillaMilkValue * (prestigeLevel +1);
        strawberryMilkValue = _baseStrawberryMilkValue * (prestigeLevel +1);
        chocolateMilkValue = _baseChocolateMilkValue * (prestigeLevel +1);
        milkAmount = 0;
        vanillaMilkAmount = 0;
        strawberryMilkAmount = 0;
        chocolateMilkAmount = 0;
        money = 0;
        suckList.Clear();
        cowList.Clear();

        _moreTeetsCost = _baseMoreTeetsCost * (prestigeLevel + 1);
        _suckerCost = _baseSuckerCost * (prestigeLevel +1);
        _fasterSuckerCost = _baseFasterSuckerCost * (prestigeLevel +1);
        _biggerCowsCost = _baseBiggerCowsCost * (prestigeLevel +1);
        _friesianCost = _baseFriesianCost * (prestigeLevel +1);
        _vanillaCost = _baseVanillaCost * (prestigeLevel +1);
        _strawberryCost = _baseStrawberryCost * (prestigeLevel +1);
        _chocolateCost = _baseChocolateCost * (prestigeLevel +1);
        _prestigeCost = _basePrestigeCost * (prestigeLevel +1);
        
        _timesTeatsBought = 0;
        _timesSuckerBought = 0;
        _timesFasterSuccBought = 0;
        _timesBiggerCowBought = 0;
        _timesFriesianBought = 0;
        _timesVanillaBought = 0;
        _timesStrawberryBought = 0;
        _timesChocolateBought = 0;
    }

    public void SellMilk()
    {
        int total = 0;

        total += milkAmount * milkValue;
        milkAmount = 0;

        total += vanillaMilkAmount * vanillaMilkValue;
        vanillaMilkAmount = 0;
        
        total += strawberryMilkAmount * strawberryMilkValue;
        strawberryMilkAmount = 0;
        
        total += chocolateMilkAmount * chocolateMilkValue;
        chocolateMilkAmount = 0;

        money += total;
    }

    public int GetCost(string upgradeName)
    {
        switch (upgradeName)
        {
            case "sucker":
                return _suckerCost * (_timesSuckerBought + 1);
            case "fasterSucker":
                return _fasterSuckerCost * (_timesFasterSuccBought + 1);
            case "biggerCows":
                return _biggerCowsCost * (_timesBiggerCowBought + 1);
            case "friesian":
                return _friesianCost * (_timesFriesianBought + 1);
            case "vanilla":
                return _vanillaCost * (_timesVanillaBought + 1);
            case "strawberry":
                return _strawberryCost * (_timesStrawberryBought + 1);
            case "chocolate":
                return _chocolateCost * (_timesChocolateBought + 1);
            case "teat":
                return _moreTeetsCost * (_timesTeatsBought + 1);
            case "prestige":
                return _prestigeCost;
            default:
                Debug.Log("bad case");
                return 999999999;
        }
    }

    public void IncrementCost(string upgradeName)
    {
        switch (upgradeName)
        {
            case "sucker":
                _timesSuckerBought++;
                break;
            case "fasterSucker":
                _timesFasterSuccBought++;
                break;
            case "biggerCows":
                _timesBiggerCowBought++;
                break;
            case "friesian":
                _timesFriesianBought++;
                break;
            case "vanilla":
                _timesVanillaBought++;
                break;
            case "strawberry":
                _timesStrawberryBought++;
                break;
            case "chocolate":
                _timesChocolateBought++;
                break;
            case "teat":
                _timesTeatsBought++;
                break;
            default:
                Debug.Log("bad case");
                break;
        }
    }
    
    public int GetTimesBought(string upgradeName)
    {
        switch (upgradeName)
        {
            case "sucker":
                return _timesSuckerBought;
            case "fasterSucker":
                return _timesFasterSuccBought;
            case "biggerCows":
                return _timesBiggerCowBought ;
            case "friesian":
                return _timesFriesianBought;
            case "vanilla":
                return _timesVanillaBought ;
            case "strawberry":
                return _timesStrawberryBought ;
            case "chocolate":
                return _timesChocolateBought ;
            case "teat":
                return _timesTeatsBought;
            default:
                Debug.Log("bad case");
                return 999999999;
        }
    }
}
