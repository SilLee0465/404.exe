using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CandySaveData : MonoBehaviour
{

    public static CandySaveData Instance;

    void Awake()
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

    [SerializeField] Text[] allCandysUIText;

    public int Candys;

    void Start()
    {
        UpdateAllCandysUIText();
    }

    public void UseCandys(int amount)
    {
        Candys -= amount;
    }

    public bool HasEnoughCandys(int amount)
    {
        return (Candys >= amount);
    }

    public void UpdateAllCandysUIText()
    {
        for (int i = 0; i < allCandysUIText.Length; i++)
        {
            allCandysUIText[i].text = Candys.ToString();
        }
    }
}
