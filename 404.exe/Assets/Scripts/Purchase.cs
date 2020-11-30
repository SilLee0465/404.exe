using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Purchase : MonoBehaviour
{
    public static Purchase Instance;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    [System.Serializable]
    public class ShopItem
    {
        public Sprite Image;
        public string Lv;
        public string ItemName;
        public string Detail;
        public int Price;
        public int UpgradeLv;
        public bool IsPurchased = false;
    }

    [SerializeField] public List<ShopItem> ShopItemsList;
    [SerializeField] Transform ShopScrollView;
    [SerializeField] GameObject NoEnoughCandysUI;
    [SerializeField] GameObject CandysText;

    GameObject ItemTemplate;
    GameObject g;
    Button buyBtn;

    int currentCandys;

    //Haijen added=
    public static int webDuration = 5;
    public static int broomDuration = 5;
    public static int zapDuration = 5;
    public static int moonDuration = 5;
    public static int lanternDuration = 5;
    //------------------------------------

    // Start is called before the first frame update
    void Start()
    {
        CandySaveData.Instance.UpdateAllCandysUIText();
        currentCandys = CandySaveData.Instance.Candys;
        CandysText.GetComponent<Text>().text = currentCandys.ToString();

        ItemTemplate = ShopScrollView.GetChild(0).gameObject;

        int len = ShopItemsList.Count;
        for (int i = 0; i < len; i++)
        {
            g = Instantiate(ItemTemplate, ShopScrollView);
            g.transform.GetChild(0).GetComponent<Image>().sprite = ShopItemsList[i].Image;
            g.transform.GetChild(1).GetComponent<Text>().text = ShopItemsList[i].Lv;
            g.transform.GetChild(2).GetComponent<Text>().text = ShopItemsList[i].ItemName;
            g.transform.GetChild(3).GetComponent<Text>().text = ShopItemsList[i].Detail;
            g.transform.GetChild(4).GetChild(0).GetComponent<Text>().text = ShopItemsList[i].Price.ToString();
            buyBtn = g.transform.GetChild(4).GetComponent<Button>();
            if (ShopItemsList[i].IsPurchased)
            {
                DisableBuyButton();
            }
            buyBtn.AddEventListener(i, OnShopItemBtnClicked);
        }
        Destroy(ItemTemplate);
    }

    void OnShopItemBtnClicked(int itemIndex)
    {
        if (CandySaveData.Instance.HasEnoughCandys(ShopItemsList[itemIndex].Price))
        {
            CandySaveData.Instance.UseCandys(ShopItemsList[itemIndex].Price);
            buyBtn = ShopScrollView.GetChild(itemIndex).GetChild(4).GetComponent<Button>();

            //change UI text: candys
            CandySaveData.Instance.UpdateAllCandysUIText();
            currentCandys = CandySaveData.Instance.Candys;
            CandysText.GetComponent<Text>().text = currentCandys.ToString();

            //add power-up
            ShopItemsList[itemIndex].UpgradeLv++;

            //stop purchase when upgrade Lv reach MAX 
            if (ShopItemsList[itemIndex].UpgradeLv == 6)
            {
                ShopItemsList[itemIndex].IsPurchased = true;
                DisableBuyButton();
            }

            //add power-up
            UpgradePower(itemIndex, ShopItemsList[itemIndex].ItemName, ShopItemsList[itemIndex].UpgradeLv);

        }
        else
        {
            NoEnoughCandysUI.SetActive(true);
        }
    }

    void DisableBuyButton()
    {
        buyBtn.interactable = false;
        buyBtn.transform.GetChild(0).GetComponent<Text>().text = "MAX LEVEL";
    }

    void UpgradePower(int itemIndex, string itemName, int upgradeLv)
    {
        if (itemName == "BroomStrick")
        {
            if (upgradeLv == 2)
            {
                ShopItemsList[itemIndex].Lv = "Level 2";
                ShopItemsList[itemIndex].Price = 2500;
                broomDuration = 5; //Haijen added
                ShopScrollView.GetChild(itemIndex).GetChild(1).GetComponent<Text>().text = "Level 2";
                ShopScrollView.GetChild(itemIndex).GetChild(4).GetChild(0).GetComponent<Text>().text = ShopItemsList[itemIndex].Price.ToString();
            }
            else if (upgradeLv == 3)
            {
                ShopItemsList[itemIndex].Lv = "Level 3";
                ShopItemsList[itemIndex].Price = 5000;
                broomDuration = 6; //Haijen added
                ShopScrollView.GetChild(itemIndex).GetChild(1).GetComponent<Text>().text = "Level 3";
                ShopScrollView.GetChild(itemIndex).GetChild(4).GetChild(0).GetComponent<Text>().text = ShopItemsList[itemIndex].Price.ToString();
            }
            else if (upgradeLv == 4)
            {
                ShopItemsList[itemIndex].Lv = "Level 4";
                ShopItemsList[itemIndex].Price = 8750;
                broomDuration = 7; //Haijen added
                ShopScrollView.GetChild(itemIndex).GetChild(1).GetComponent<Text>().text = "Level 4";
                ShopScrollView.GetChild(itemIndex).GetChild(4).GetChild(0).GetComponent<Text>().text = ShopItemsList[itemIndex].Price.ToString();
            }
            else if (upgradeLv == 5)
            {
                ShopItemsList[itemIndex].Lv = "Level 5";
                ShopItemsList[itemIndex].Price = 11250;
                broomDuration = 8; //Haijen added
                ShopScrollView.GetChild(itemIndex).GetChild(1).GetComponent<Text>().text = "Level 5";
                ShopScrollView.GetChild(itemIndex).GetChild(4).GetChild(0).GetComponent<Text>().text = ShopItemsList[itemIndex].Price.ToString();
            }
            else
            {
                ShopItemsList[itemIndex].Lv = "Level Max";
                broomDuration = 9; //Haijen added
                ShopScrollView.GetChild(itemIndex).GetChild(1).GetComponent<Text>().text = "MAX LEVEL";
            }
        }
        else if (itemName == "SpiderWeb")
        {
            if (upgradeLv == 2)
            {
                ShopItemsList[itemIndex].Lv = "Level 2";
                ShopItemsList[itemIndex].Price = 2500;
                webDuration = 5; //Haijen added
                ShopScrollView.GetChild(itemIndex).GetChild(1).GetComponent<Text>().text = "Level 2";
                ShopScrollView.GetChild(itemIndex).GetChild(4).GetChild(0).GetComponent<Text>().text = ShopItemsList[itemIndex].Price.ToString();
            }
            else if (upgradeLv == 3)
            {
                ShopItemsList[itemIndex].Lv = "Level 3";
                ShopItemsList[itemIndex].Price = 5000;
                webDuration = 6; //Haijen added
                ShopScrollView.GetChild(itemIndex).GetChild(1).GetComponent<Text>().text = "Level 3";
                ShopScrollView.GetChild(itemIndex).GetChild(4).GetChild(0).GetComponent<Text>().text = ShopItemsList[itemIndex].Price.ToString();
            }
            else if (upgradeLv == 4)
            {
                ShopItemsList[itemIndex].Lv = "Level 4";
                ShopItemsList[itemIndex].Price = 8750;
                webDuration = 7; //Haijen added
                ShopScrollView.GetChild(itemIndex).GetChild(1).GetComponent<Text>().text = "Level 4";
                ShopScrollView.GetChild(itemIndex).GetChild(4).GetChild(0).GetComponent<Text>().text = ShopItemsList[itemIndex].Price.ToString();
            }
            else if (upgradeLv == 5)
            {
                ShopItemsList[itemIndex].Lv = "Level 5";
                ShopItemsList[itemIndex].Price = 11000;
                webDuration = 8; //Haijen added
                ShopScrollView.GetChild(itemIndex).GetChild(1).GetComponent<Text>().text = "Level 5";
                ShopScrollView.GetChild(itemIndex).GetChild(4).GetChild(0).GetComponent<Text>().text = ShopItemsList[itemIndex].Price.ToString();
            }
            else
            {
                ShopItemsList[itemIndex].Lv = "Level Max";
                webDuration = 9; //Haijen added
                ShopScrollView.GetChild(itemIndex).GetChild(1).GetComponent<Text>().text = "MAX LEVEL";
            }
        }
        else if (itemName == "MagicWand")
        {
            if (upgradeLv == 2)
            {
                ShopItemsList[itemIndex].Lv = "Level 2";
                ShopItemsList[itemIndex].Price = 1000;
                zapDuration = 5; //Haijen added
                ShopScrollView.GetChild(itemIndex).GetChild(1).GetComponent<Text>().text = "Level 2";
                ShopScrollView.GetChild(itemIndex).GetChild(4).GetChild(0).GetComponent<Text>().text = ShopItemsList[itemIndex].Price.ToString();
            }
            else if (upgradeLv == 3)
            {
                ShopItemsList[itemIndex].Lv = "Level 3";
                ShopItemsList[itemIndex].Price = 2500;
                zapDuration = 6; //Haijen added
                ShopScrollView.GetChild(itemIndex).GetChild(1).GetComponent<Text>().text = "Level 3";
                ShopScrollView.GetChild(itemIndex).GetChild(4).GetChild(0).GetComponent<Text>().text = ShopItemsList[itemIndex].Price.ToString();
            }
            else if (upgradeLv == 4)
            {
                ShopItemsList[itemIndex].Lv = "Level 4";
                ShopItemsList[itemIndex].Price = 5000;
                zapDuration = 7; //Haijen added
                ShopScrollView.GetChild(itemIndex).GetChild(1).GetComponent<Text>().text = "Level 4";
                ShopScrollView.GetChild(itemIndex).GetChild(4).GetChild(0).GetComponent<Text>().text = ShopItemsList[itemIndex].Price.ToString();
            }
            else if (upgradeLv == 5)
            {
                ShopItemsList[itemIndex].Lv = "Level 5";
                ShopItemsList[itemIndex].Price = 8750;
                zapDuration = 8; //Haijen added
                ShopScrollView.GetChild(itemIndex).GetChild(1).GetComponent<Text>().text = "Level 5";
                ShopScrollView.GetChild(itemIndex).GetChild(4).GetChild(0).GetComponent<Text>().text = ShopItemsList[itemIndex].Price.ToString();
            }
            else
            {
                ShopItemsList[itemIndex].Lv = "Level Max";
                zapDuration = 9; //Haijen added
                ShopScrollView.GetChild(itemIndex).GetChild(1).GetComponent<Text>().text = "MAX LEVEL";
            }
        }
        else if (itemName == "JackoLanterns")
        {
            if (upgradeLv == 2)
            {
                ShopItemsList[itemIndex].Lv = "Level 2";
                ShopItemsList[itemIndex].Price = 1000;
                lanternDuration = 5; //Haijen added
                ShopScrollView.GetChild(itemIndex).GetChild(1).GetComponent<Text>().text = "Level 2";
                ShopScrollView.GetChild(itemIndex).GetChild(4).GetChild(0).GetComponent<Text>().text = ShopItemsList[itemIndex].Price.ToString();
            }
            else if (upgradeLv == 3)
            {
                ShopItemsList[itemIndex].Lv = "Level 3";
                ShopItemsList[itemIndex].Price = 2500;
                lanternDuration = 6; //Haijen added
                ShopScrollView.GetChild(itemIndex).GetChild(1).GetComponent<Text>().text = "Level 3";
                ShopScrollView.GetChild(itemIndex).GetChild(4).GetChild(0).GetComponent<Text>().text = ShopItemsList[itemIndex].Price.ToString();
            }
            else if (upgradeLv == 4)
            {
                ShopItemsList[itemIndex].Lv = "Level 4";
                ShopItemsList[itemIndex].Price = 5000;
                lanternDuration = 7; //Haijen added
                ShopScrollView.GetChild(itemIndex).GetChild(1).GetComponent<Text>().text = "Level 4";
                ShopScrollView.GetChild(itemIndex).GetChild(4).GetChild(0).GetComponent<Text>().text = ShopItemsList[itemIndex].Price.ToString();
            }
            else if (upgradeLv == 5)
            {
                ShopItemsList[itemIndex].Lv = "Level 5";
                ShopItemsList[itemIndex].Price = 8750;
                lanternDuration = 8; //Haijen added
                ShopScrollView.GetChild(itemIndex).GetChild(1).GetComponent<Text>().text = "Level 5";
                ShopScrollView.GetChild(itemIndex).GetChild(4).GetChild(0).GetComponent<Text>().text = ShopItemsList[itemIndex].Price.ToString();
            }
            else
            {
                ShopItemsList[itemIndex].Lv = "Level Max";
                lanternDuration = 9; //Haijen added
                ShopScrollView.GetChild(itemIndex).GetChild(1).GetComponent<Text>().text = "MAX LEVEL";
            }
        }
        else if (itemName == "FullMoon")
        {
            if (upgradeLv == 2)
            {
                ShopItemsList[itemIndex].Lv = "Level 2";
                ShopItemsList[itemIndex].Price = 1000;
                moonDuration = 5; //Haijen added
                ShopScrollView.GetChild(itemIndex).GetChild(1).GetComponent<Text>().text = "Level 2";
                ShopScrollView.GetChild(itemIndex).GetChild(4).GetChild(0).GetComponent<Text>().text = ShopItemsList[itemIndex].Price.ToString();
            }
            else if (upgradeLv == 3)
            {
                ShopItemsList[itemIndex].Lv = "Level 3";
                ShopItemsList[itemIndex].Price = 2500;
                moonDuration = 6; //Haijen added
                ShopScrollView.GetChild(itemIndex).GetChild(1).GetComponent<Text>().text = "Level 3";
                ShopScrollView.GetChild(itemIndex).GetChild(4).GetChild(0).GetComponent<Text>().text = ShopItemsList[itemIndex].Price.ToString();
            }
            else if (upgradeLv == 4)
            {
                ShopItemsList[itemIndex].Lv = "Level 4";
                ShopItemsList[itemIndex].Price = 5000;
                moonDuration = 7; //Haijen added
                ShopScrollView.GetChild(itemIndex).GetChild(1).GetComponent<Text>().text = "Level 4";
                ShopScrollView.GetChild(itemIndex).GetChild(4).GetChild(0).GetComponent<Text>().text = ShopItemsList[itemIndex].Price.ToString();
            }
            else if (upgradeLv == 5)
            {
                ShopItemsList[itemIndex].Lv = "Level 5";
                ShopItemsList[itemIndex].Price = 8750;
                moonDuration = 8; //Haijen added
                ShopScrollView.GetChild(itemIndex).GetChild(1).GetComponent<Text>().text = "Level 5";
                ShopScrollView.GetChild(itemIndex).GetChild(4).GetChild(0).GetComponent<Text>().text = ShopItemsList[itemIndex].Price.ToString();
            }
            else
            {
                ShopItemsList[itemIndex].Lv = "Level Max";
                moonDuration = 9; //Haijen added
                ShopScrollView.GetChild(itemIndex).GetChild(1).GetComponent<Text>().text = "MAX LEVEL";
            }
        }
        else if (itemName == "MagicPotion")
        {
            if (upgradeLv == 2)
            {
                ShopItemsList[itemIndex].Lv = "Level 2";
                ShopItemsList[itemIndex].Price = 5000;
                ShopScrollView.GetChild(itemIndex).GetChild(1).GetComponent<Text>().text = "Level 2";
                ShopScrollView.GetChild(itemIndex).GetChild(4).GetChild(0).GetComponent<Text>().text = ShopItemsList[itemIndex].Price.ToString();
            }
            else if (upgradeLv == 3)
            {
                ShopItemsList[itemIndex].Lv = "Level 3";
                ShopItemsList[itemIndex].Price = 8750;
                ShopScrollView.GetChild(itemIndex).GetChild(1).GetComponent<Text>().text = "Level 3";
                ShopScrollView.GetChild(itemIndex).GetChild(4).GetChild(0).GetComponent<Text>().text = ShopItemsList[itemIndex].Price.ToString();
            }
            else if (upgradeLv == 4)
            {
                ShopItemsList[itemIndex].Lv = "Level 4";
                ShopItemsList[itemIndex].Price = 11250;
                ShopScrollView.GetChild(itemIndex).GetChild(1).GetComponent<Text>().text = "Level 4";
                ShopScrollView.GetChild(itemIndex).GetChild(4).GetChild(0).GetComponent<Text>().text = ShopItemsList[itemIndex].Price.ToString();
            }
            else if (upgradeLv == 5)
            {
                ShopItemsList[itemIndex].Lv = "Level 5";
                ShopItemsList[itemIndex].Price = 13000;
                ShopScrollView.GetChild(itemIndex).GetChild(1).GetComponent<Text>().text = "Level 5";
                ShopScrollView.GetChild(itemIndex).GetChild(4).GetChild(0).GetComponent<Text>().text = ShopItemsList[itemIndex].Price.ToString();
            }
            else
            {
                ShopItemsList[itemIndex].Lv = "Level Max";
                ShopScrollView.GetChild(itemIndex).GetChild(1).GetComponent<Text>().text = "MAX LEVEL";
            }
        }
        else if (itemName == "Skull")
        {
            if (upgradeLv == 2)
            {
                ShopItemsList[itemIndex].Lv = "Level 2";
                ShopItemsList[itemIndex].Price = 8750;
                ShopScrollView.GetChild(itemIndex).GetChild(1).GetComponent<Text>().text = "Level 2";
                ShopScrollView.GetChild(itemIndex).GetChild(4).GetChild(0).GetComponent<Text>().text = ShopItemsList[itemIndex].Price.ToString();
            }
            else if (upgradeLv == 3)
            {
                ShopItemsList[itemIndex].Lv = "Level 3";
                ShopItemsList[itemIndex].Price = 11250;
                ShopScrollView.GetChild(itemIndex).GetChild(1).GetComponent<Text>().text = "Level 3";
                ShopScrollView.GetChild(itemIndex).GetChild(4).GetChild(0).GetComponent<Text>().text = ShopItemsList[itemIndex].Price.ToString();
            }
            else
            {
                ShopItemsList[itemIndex].Lv = "Level Max";
                ShopScrollView.GetChild(itemIndex).GetChild(1).GetComponent<Text>().text = "MAX LEVEL";
                ShopItemsList[itemIndex].UpgradeLv = 6;
                ShopItemsList[itemIndex].IsPurchased = true;
                DisableBuyButton();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}