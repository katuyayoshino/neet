using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


namespace battle
{
    public class ShopBManager
    {
        public GameObject ShopListButton;
    }
    public class Shop : MonoBehaviour
    {
        public GameObject content;
        public GameObject ShopBuyMenu;
        public GameObject RecepientDetails;
        public GameObject ConfirmWindow;
        public GameObject DetailsImage;
        public Button BacktoMainShop;
        public Button DBuybutton;
        public Text ItemDetailsName;
        public Text ItemDetailsPrice;
        public Text itemCount;
        public Text TotalCost;
        public Text PMoney;
        public Text DealText;
        public Text TitleText;
        public Text ConfirmText;
        public string Name;
        public int Place;
        public int Icount=0;
        public int ShopMode;
        public int PlayerMoney;
        public bool Confirmation = false;

        public List<ShopBManager> Buttons= null;
        // Use this for initialization
        void Start()
        {
            PlayerMoney = PlayerManager.P_Instance.Playerpara[0].money;
            ShopBuyMenu.SetActive(false);
            RecepientDetails.SetActive(false);
            ConfirmWindow.SetActive(false);
            itemCount.text = Icount.ToString();
            PMoney.text = "所持金\t" + "￥" + PlayerMoney;
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void SellBuyButton()
        {
            Buttons = new List<ShopBManager>();
            Name = EventSystem.current.currentSelectedGameObject.name;
            if (Name == "Buy")
            {
                ShopMode = 1;
                TitleText.text=DealText.text = "買";
                
            }
            else if(Name == "Sell")
            {
                ShopMode = 0;
                TitleText.text = DealText.text = "売";
            }
                if (content.transform.childCount == 0)
                {
                    var Shoplist = ItemManager.I_Instance.S_List;
                    int ItemsCount = Shoplist[0].ItemName.Length;
                    int count = 0;
                    for (int i = 0; i < ItemsCount; i++)
                    {
                        if (Shoplist[ShopMode].ItemNum[i] > 0 && Shoplist[ShopMode].ItemPrice[i]>0)
                        {
                            LoadStorage(new Vector3(0, count * -62, 0), Shoplist[0].ItemName[i], Shoplist[0].ItemName[i] + " ￥" + Shoplist[ShopMode].ItemPrice[i], ShopListOnClick);
                            
                            count++;
                        }
                    }
                    content.GetComponent<RectTransform>().sizeDelta = new Vector2(0, count * 62);
                }
            
            ShopBuyMenu.SetActive(true);
            PMoney.transform.SetParent(ShopBuyMenu.transform,false);
            PMoney.transform.localPosition = new Vector3(70, 90, 0);
        }
        public void LoadStorage(Vector3 position,/*Sprite Image,*/string IName, string itemNamenPrice, UnityEngine.Events.UnityAction method)
        {
            GameObject button = new GameObject();
            GameObject text = new GameObject();
            GameObject Images = new GameObject();
            button.transform.SetParent(content.transform, false);
            button.AddComponent<RectTransform>();
            button.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
            button.GetComponent<RectTransform>().anchorMax = new Vector2(1.0f, 1.0f);
            button.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);
            button.GetComponent<RectTransform>().sizeDelta = new Vector2(0,60);
            button.GetComponent<RectTransform>().localPosition = position;
            button.AddComponent<Button>();
            button.AddComponent<Image>();
            button.name = IName.ToString();
            button.GetComponent<Button>().onClick.AddListener(method);
            button.AddComponent<Outline>();
            button.GetComponent<Outline>().effectColor = Color.black;

            Images.AddComponent<Image>();
            Images.transform.SetParent(button.transform, true);
            Images.AddComponent<RectTransform>();
            Images.GetComponent<RectTransform>().sizeDelta = new Vector2(50, 50);
            Images.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            Images.GetComponent<RectTransform>().anchoredPosition = new Vector3(-110, 0, 0);
            Images.GetComponent<Image>().sprite = Resources.Load<Sprite>(IName);

            text.transform.SetParent(button.transform, true);
            text.AddComponent<RectTransform>();
            text.GetComponent<RectTransform>().sizeDelta = new Vector2(150, 40);
            text.name = IName + "Number";
            text.AddComponent<Text>();
            text.GetComponent<Text>().font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            text.GetComponent<Text>().text = itemNamenPrice;
            text.GetComponent<Text>().color = Color.black;
            text.GetComponent<RectTransform>().localPosition = new Vector3(0,0,0);
            text.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -30, 0);
            Buttons.Add(new ShopBManager()
            {
                ShopListButton = button
            });
        }
        public void ShopListOnClick()
        {
            RecepientDetails.SetActive(true);
            DBuybutton.interactable = false;
            BacktoMainShop.interactable = false;
            Name = EventSystem.current.currentSelectedGameObject.name;
            Place = System.Array.IndexOf(ItemManager.I_Instance.S_List[0].ItemName, Name);
            Icount = 0;
            itemCount.text = Icount.ToString();
            DetailsImage.GetComponent<Image>().sprite = Resources.Load<Sprite>(Name);
            TotalCost.color = Color.white;
            TotalCost.text = "Total\t" + "￥" + Icount.ToString();
            ItemDetailsName.text = Name;
            ItemDetailsPrice.text = "￥"+ItemManager.I_Instance.S_List[ShopMode].ItemPrice[Place].ToString();
        }
        public void AddMinus()
        {
            int bagitem = PlayerManager.P_Instance.CarryingItem;
            Debug.Log(bagitem);
            Name = EventSystem.current.currentSelectedGameObject.name;
            Place = System.Array.IndexOf(ItemManager.I_Instance.S_List[0].ItemName, ItemDetailsName.text);
            if (Name == "Plus" && Icount < ItemManager.I_Instance.S_List[ShopMode].ItemNum[Place])
            {
                Icount++;
                itemCount.text = Icount.ToString();
                TotalCost.text = "Total\t" + "￥" + (ItemManager.I_Instance.S_List[ShopMode].ItemPrice[Place] * Icount).ToString();
                if (PlayerMoney < ItemManager.I_Instance.S_List[ShopMode].ItemPrice[Place] * Icount && ShopMode == 1)
                {
                    TotalCost.color = Color.red;
                    DBuybutton.interactable = false;
                }
                else
                {
                    TotalCost.color = Color.white;
                    DBuybutton.interactable = true;
                }
                if (bagitem + (Mathf.Pow(-1, ShopMode + 1) * Icount) > 20) { DBuybutton.interactable = false; }
            }
            else if (Name == "Minus" && Icount > 0)
            {
                Icount--;
                itemCount.text = Icount.ToString();
                TotalCost.text = "Total\t" + "￥" + (ItemManager.I_Instance.S_List[ShopMode].ItemPrice[Place] * Icount).ToString();
                if (PlayerMoney < ItemManager.I_Instance.S_List[ShopMode].ItemPrice[Place] * Icount && ShopMode == 1)
                {
                    TotalCost.color = Color.red;
                    DBuybutton.interactable = false;
                }
                else
                {
                    TotalCost.color = Color.white;
                    DBuybutton.interactable = true;
                    if (Icount == 0)
                    {
                        DBuybutton.interactable = false;
                    }
                }
                if (bagitem + (Mathf.Pow(-1, ShopMode + 1) * Icount) == 20) DBuybutton.interactable = true;
            }
        }
        public void BackToShoplist()
        {
            if (Confirmation == false)
            {
                RecepientDetails.SetActive(false);
                BacktoMainShop.interactable = true;
            }
        }
        public void BackToShopMain()
        {
            ShopBuyMenu.SetActive(false);
            PlayerManager.P_Instance.Playerpara[0].money = PlayerMoney;
            foreach(Transform child in content.transform)
            {
                Destroy(child.gameObject);
            }
            PMoney.transform.SetParent(transform,false);
            PMoney.transform.localPosition = new Vector3(70, -25, 0);
        }
        public void DetailsDealbutton()
        {
            if (Confirmation == false)
            {
                ConfirmWindow.SetActive(true);
                if (ShopMode == 1)
                {
                    ConfirmText.text = "本当に買いますか？";
                }else { ConfirmText.text = "本当に売りますか？"; }
                Confirmation = true;
            }
        }
        public void ConfirmBuy()
        {
            int bagitem = PlayerManager.P_Instance.CarryingItem;
            if (ShopMode == 0)
            {
                PlayerMoney += Icount * ItemManager.I_Instance.S_List[ShopMode].ItemPrice[Place];
                ItemManager.I_Instance.S_List[0].ItemNum[Place] -= Icount;
                bagitem -= Icount;
                //ItemManager.I_Instance.S_List[1].ItemNum[Place] += Icount;
                if(ItemManager.I_Instance.S_List[0].ItemNum[Place] == 0)
                {
                    var Binterect = Buttons[Place].ShopListButton.GetComponent<Button>();
                    Binterect.interactable = false;
                }
            }
            else if (ShopMode == 1)
            {
                PlayerMoney -= Icount * ItemManager.I_Instance.S_List[ShopMode].ItemPrice[Place];
                ItemManager.I_Instance.S_List[0].ItemNum[Place] += Icount;
                bagitem += Icount;
                ItemManager.I_Instance.S_List[1].ItemNum[Place] -= Icount;
                if (ItemManager.I_Instance.S_List[1].ItemNum[Place] == 0)
                {
                    var Binterect = Buttons[Place].ShopListButton.GetComponent<Button>();
                    Binterect.interactable = false;
                }
            }
            PlayerManager.P_Instance.CarryingItem=bagitem;
            PMoney.text = "所持金\t" + "￥" + PlayerMoney;
            BacktoMainShop.interactable = true;
            ConfirmWindow.SetActive(false);
            Confirmation = false;
            RecepientDetails.SetActive(false);
        }
        public void CancelBuy()
        {
            ConfirmWindow.SetActive(false);
            Confirmation = false;
        }
    }
}
