using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    

    private UiManager _uiManager;
    private PlayerBehaviour _player;

    private float _energy = 3, _money = 0, _maxEnergy = 3;
    private int _level = 0;
    private float _xpPoints = 0f, _xpToLvlUp = 100f;
    private float _energyRenewCd = 2f;

    public float GetMoney()
    {
        return _money;
    }
    public float GetRawEnergy()
    {
        return _energy;
    }
    public float GetMaxEnergy()
    {
        return _maxEnergy;
    }
    public float GetEnergyPercentage()
    {
        return _energy / _maxEnergy;
    }
    public float GetXpPercentage()
    {
        return _xpPoints / _xpToLvlUp;
    }
    public int GetCurrentLevel()
    {
        return _level;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        _uiManager = UiManager.Instance;
        StageManager.Instance.CheckInstance();

        if (_player == null)
            _player = FindFirstObjectByType<PlayerBehaviour>();
        
        StartCoroutine(RechargeEnergy());
    }
    private void Update()
    {
        if (_energy > _maxEnergy)
        { 
            _energy = _maxEnergy;
        }
    }
    public void EarnMoney(float amount)
    {
        _money += amount;
        _uiManager.UpdateText();
    }
    public void SpendMoney(float amount)
    {
        if (_money - amount >= 0)
        {
            _money -= amount;
            _uiManager.UpdateText();
        }
        else
        {
            Debug.Log("Dinheiro Insuficiente");
        }
    }
    public void LevelUp(float xpGained)
    {
        float levelsGained = Mathf.FloorToInt(((_xpPoints + xpGained) / _xpToLvlUp) * 10f) / 10f;
        if (levelsGained >= 1f)
        {
            if (levelsGained > 1f)
            {
                _level++;
                MaxXpUpdate();
                LevelUp(levelsGained - 1f);
            }
            else
            {
                _level++;
                _xpPoints = (_xpPoints + xpGained) - (levelsGained * _xpToLvlUp);
                _uiManager.UpdateLevel();
                MaxXpUpdate();
            }
        }
        else
        {
            _xpPoints += xpGained;
        }
    }
    public void MaxXpUpdate()
    {
        if (_level == 0)
        {
            float startLevelUp = 100f;
            _xpToLvlUp = startLevelUp;
        }
        else
        {
            _xpToLvlUp += _level * 50f;
        }
    }

    public void ChangeScene(string newScene)
    {
        SceneManager.LoadScene(newScene);
    }
    public void SpendEnergy()
    {
        if (_energy != 0)
        {
            _energy--;
            _uiManager.UpdateText();
        }
        else
        {
            Debug.Log("Sem Energia");
        }
    }
    private IEnumerator RechargeEnergy()
    {
        if (_maxEnergy - 1 == _energy)
        {
            Mathf.Lerp(_energy, _maxEnergy, _energyRenewCd);
            yield return new WaitUntil(() => _energy == _maxEnergy);
            _uiManager.UpdateText();
            yield return new WaitForSeconds(_energyRenewCd + 0.2f);
            yield return new WaitUntil(() => _energy < _maxEnergy);
            StartCoroutine(RechargeEnergy());
        }
        else
        {
            StartCoroutine(RechargeEnergy());
            yield break;
        }
    }
}
