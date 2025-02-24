using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;


public class UdderSpawner : MonoBehaviour
{
    [SerializeField] GameObject teetPrefab;
    [SerializeField] public float udderRadius;
	[SerializeField] public TextMeshProUGUI milkText;
    [SerializeField] public TextMeshProUGUI moneyText;
    [SerializeField] private float shakeThreshold;
    [SerializeField] private List<GameObject> _teetList = new List<GameObject>();
    [SerializeField] private MilkManager _milkManager;
    [SerializeField] private Sprite[] udderSprites;
	
	private float _previousZ;
	private static JSL.JOY_SHOCK_STATE _joyShock;
	private JSL.IMU_STATE _imuState;
	private int[] _deviceArray = new int[1];
	private string _movement;
	private string _lastMovement;
	private float _milkValue;
	private float _moneyValue;
	private int _milkOutAmount = 1;
	private int _prestigeLevel;
	private AudioSource _audioSource;
	private float _lastTime;
	private System.Random rnd;
	
    
    private void Start()
    {
        JSL.JslDisconnectAndDisposeAll();
        JSL.JslConnectDevices();
        JSL.JslGetConnectedDeviceHandles(_deviceArray, 1);
         _imuState = JSL.JslGetIMUState(_deviceArray[0]);
         
        _previousZ = _imuState.accelZ;

        _prestigeLevel = _milkManager.prestigeLevel;
        switch (_prestigeLevel)
        {
            case 0:
                gameObject.GetComponent<SpriteRenderer>().sprite = udderSprites[0];
                teetPrefab.GetComponentInChildren<SpriteRenderer>().sprite = udderSprites[1];
                SetMilkValue(_milkManager.milkValue);
                break;
            case 1:
                gameObject.GetComponent<SpriteRenderer>().sprite = udderSprites[2];
                teetPrefab.GetComponentInChildren<SpriteRenderer>().sprite = udderSprites[3];
                SetMilkValue(_milkManager.vanillaMilkValue);
                break;
            case 2:
                gameObject.GetComponent<SpriteRenderer>().sprite = udderSprites[4];
                teetPrefab.GetComponentInChildren<SpriteRenderer>().sprite = udderSprites[5];
                SetMilkValue(_milkManager.strawberryMilkValue);
                break;
            case 3:
                gameObject.GetComponent<SpriteRenderer>().sprite = udderSprites[6];
                teetPrefab.GetComponentInChildren<SpriteRenderer>().sprite = udderSprites[7];
                SetMilkValue(_milkManager.chocolateMilkValue);
                break;
            default:
                gameObject.GetComponent<SpriteRenderer>().sprite = udderSprites[6];
                teetPrefab.GetComponentInChildren<SpriteRenderer>().sprite = udderSprites[7];
                SetMilkValue(_milkManager.chocolateMilkValue);
                break;
        }

        AddTeet();
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            AddTeet();
        }

        if (Input.GetKeyDown(KeyCode.F8))
        {
            Time.timeScale = 2;
        }

        if (Input.GetKeyDown((KeyCode.F9)))
        {
            Time.timeScale = 1;
        }

        if (Input.GetKeyDown(KeyCode.F7))
        {
            _milkManager.money += 1000000;
        }
        
    }

    private void FixedUpdate()
    {
        _imuState = JSL.JslGetIMUState(_deviceArray[0]);
        
        JoyConMovement();
    }

    public void AddTeet()
    {
        if (_teetList.Count < 6)
        {
            _teetList.Add(Instantiate(teetPrefab, transform));
            for (int i = 0; i < _teetList.Count; i++)
            {
                float a = (((180f / (_teetList.Count + 1)) * (i + 1)) - 180f) * Mathf.Deg2Rad;
                _teetList[i].transform.localPosition = new Vector3(Mathf.Cos(a), Mathf.Sin(a), 1) * udderRadius;
                _teetList[i].transform.localEulerAngles = new Vector3(0, 0, (a * Mathf.Rad2Deg) + 90);
            }
        }
        
    }

    void JoyConMovement()
    {
        float currentZ = _imuState.accelZ;

        if (currentZ - _previousZ > shakeThreshold)
        {
            _lastMovement = _movement;
            _movement = "up";
        }
        else if (currentZ - _previousZ < -shakeThreshold)
        {
            _lastMovement = _movement;
            _movement = "down";
            if (_lastMovement == "up" && _movement == "down")
            {
                Milk();
            }
        }

        _previousZ = currentZ;
    }

    void Milk()
    {   
        switch (_prestigeLevel)
        {
            case 1:
                _milkManager.vanillaMilkAmount += (_milkOutAmount * _teetList.Count);
                break;
            case 2:
                _milkManager.strawberryMilkAmount += (_milkOutAmount * _teetList.Count);
                break;
            case 3:
                _milkManager.chocolateMilkAmount += (_milkOutAmount * _teetList.Count);
                break;
            case 0:
                _milkManager.milkAmount += (_milkOutAmount * _teetList.Count);
                break;
            case >3:
                _milkManager.chocolateMilkAmount += (_milkOutAmount * _teetList.Count);
                break;
            default:
                _milkManager.milkAmount += (_milkOutAmount * _teetList.Count);
                break;
        }
        
        
        foreach (var go in _teetList)
        {
            go.GetComponent<Animator>().Play("milkingAnim");
        }
    }

    public float GetMilkValue()
    {
        return _milkValue;
    }

    public void SetMilkValue(float value)
    {
        _milkValue = value;
        milkText.text = $"Milk(L): {_milkValue}";
    }
    
    public float GetMoneyValue()
    {
        return _moneyValue;
    }

    public void SetMoneyValue(float value)
    {
        _moneyValue = value;
        moneyText.text = $"Money: {_moneyValue}";
    }

    public int GetTeetCount()
    {
        return _teetList.Count();
    }

    public void IncreaseMilkOutAmount(int num)
    {
        _milkOutAmount += num;
    }
    
}
