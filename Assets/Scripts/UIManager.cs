using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class UIManager : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI milkText;
    [SerializeField] public TextMeshProUGUI vanillaMilkText;
    [SerializeField] public TextMeshProUGUI strawberryMilkText;
    [SerializeField] public TextMeshProUGUI chocolateMilkText;
    [SerializeField] public TextMeshProUGUI moneyText;
    [SerializeField] public MilkManager milkManager;
    [SerializeField] private UdderSpawner udderSpawner;


    [SerializeField] private TextMeshProUGUI suctionatorText;
    [SerializeField] private TextMeshProUGUI fasterSuckerText;
    [SerializeField] private TextMeshProUGUI biggerCowsText;
    [SerializeField] private TextMeshProUGUI moreTeatsText;
    [SerializeField] private TextMeshProUGUI friesianText;
    [SerializeField] private TextMeshProUGUI vanillaCowText;
    [SerializeField] private TextMeshProUGUI strawberryCowText;
    [SerializeField] private TextMeshProUGUI chocolateCowText;
    [SerializeField] private TextMeshProUGUI prestigeText;
    
    [SerializeField] private TextMeshProUGUI normalCowNumText;
    [SerializeField] private TextMeshProUGUI vanillaCowNumText;
    [SerializeField] private TextMeshProUGUI strawberryCowNumText;
    [SerializeField] private TextMeshProUGUI chocolateCowNumText;
    [SerializeField] private TextMeshProUGUI suctionatorNumText;
    [SerializeField] private GameObject[] upgradeButtons;

    private void Start()
    {
        suctionatorText.text = $"Buy Suctionator: £{milkManager.GetCost("sucker")}";
        fasterSuckerText.text = $"Buy Faster Suctionator: £{milkManager.GetCost("fasterSucker")}";
        biggerCowsText.text = $"Buy Bigger Cows: £{milkManager.GetCost("biggerCows")}";
        friesianText.text = $"Buy Friesian Cow: £{milkManager.GetCost("friesian")}";
        vanillaCowText.text = $"Buy Vanilla Cow: £{milkManager.GetCost("vanilla")}";
        strawberryCowText.text = $"Buy Strawberry Cow: £{milkManager.GetCost("strawberry")}";
        chocolateCowText.text = $"Buy Chocolate Cow: £{milkManager.GetCost("chocolate")}";
        moreTeatsText.text = $"Buy More Teats: £{milkManager.GetCost("teat")}";
        prestigeText.text = $"Prestige: £{milkManager.GetCost(("prestige"))}";
    }

    // Update is called once per frame
    void Update()
    {
        moneyText.text = $"Money: {milkManager.money}";
        milkText.text = $"Milk(L): {milkManager.milkAmount}";
        vanillaMilkText.text = $"Vanilla Milk(L): {milkManager.vanillaMilkAmount}";
        strawberryMilkText.text = $"Strawberry Milk(L): {milkManager.strawberryMilkAmount}";
        chocolateMilkText.text = $"Chocolate Milk(L): {milkManager.chocolateMilkAmount}";
    }

    public void UpdateCost(string upgradeName)
    {
        switch (upgradeName)
        {
            case "sucker":
                suctionatorText.text = $"Buy Suctionator: £{milkManager.GetCost(upgradeName)}";
                suctionatorNumText.text = $"x{milkManager.GetTimesBought(upgradeName)}";
                break;
            case "fasterSucker":
                fasterSuckerText.text = $"Buy Faster Suctionator: £{milkManager.GetCost(upgradeName)}";
                break;
            case "biggerCows":
                biggerCowsText.text = $"Buy Bigger Cows: £{milkManager.GetCost(upgradeName)}";
                break;
            case "friesian":
                friesianText.text = $"Buy Friesian Cow: £{milkManager.GetCost(upgradeName)}";
                normalCowNumText.text = $"x{milkManager.GetTimesBought(upgradeName)}";
                break;
            case "vanilla":
                vanillaCowText.text = $"Buy Vanilla Cow: £{milkManager.GetCost(upgradeName)}";
                vanillaCowNumText.text = $"x{milkManager.GetTimesBought(upgradeName)}";
                break;
            case "strawberry":
                strawberryCowText.text = $"Buy Strawberry Cow: £{milkManager.GetCost(upgradeName)}";
                strawberryCowNumText.text = $"x{milkManager.GetTimesBought(upgradeName)}";
                break;
            case "chocolate":
                chocolateCowText.text = $"Buy Chocolate Cow: £{milkManager.GetCost(upgradeName)}";
                 chocolateCowNumText.text = $"x{milkManager.GetTimesBought(upgradeName)}";
                break;
            case "teat":
                moreTeatsText.text = $"Buy More Teats: £{milkManager.GetCost(upgradeName)}";
                break;
            case "prestige":
                prestigeText.text = $"Prestige: £{milkManager.GetCost((upgradeName))}";
                break;
            case "teatMax":
                moreTeatsText.text = $"Buy More Teats: MAX";
                break;
            default:
                Debug.Log("bad case");
                break;
        }
    }

    public IEnumerator NotEnoughMoney()
    {
        moneyText.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        moneyText.color = Color.black;
    }
}
