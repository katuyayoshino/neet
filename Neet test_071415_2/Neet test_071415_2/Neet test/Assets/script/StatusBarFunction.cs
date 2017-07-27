using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace battle
{
    public class StatusBarFunction : MonoBehaviour
    {
        public GameObject UsableHp;
        public GameObject UsableHunger;
        public GameObject UsableHealth;
        public GameObject HpItemlist;
        public GameObject HungerItemlist;
        public GameObject HealthItemlist;
        public GameObject[] BarsContainer;
        public bool Loaded = false;
        // Use this for initialization
        void Start()
        {
            BarsContainer = new GameObject[] { UsableHp, UsableHunger, UsableHealth };
            HpItemlist.SetActive(false);
            HungerItemlist.SetActive(false);
            HealthItemlist.SetActive(false);
           
        }
        public void Loaditem()
        {
            var CIlist = ItemManager.I_Instance.GroupUp;
            for (int i = 0; i < 2; i++)
            {
                Debug.Log(CIlist[i + 2].ItemLists[0].ItemNum.Length);
                for (int j = 0; j < CIlist[i + 2].ItemLists[0].ItemNum.Length - 1; j++)
                    if (CIlist[i + 2].ItemLists[0].ItemNum[j] > 0)
                    {
                        LoadItemStorage(new Vector3(0,150-(BarsContainer[CIlist[i + 2].ItemLists[0].RecoverStats[j, 0] - 1].transform.childCount*70) , 0),
                            CIlist[i + 2].ItemLists[0].ItemName[j], "X" + CIlist[i + 2].ItemLists[0].ItemNum[j], ConsumeItem, BarsContainer[CIlist[i + 2].ItemLists[0].RecoverStats[j, 0]-1].transform);
                    }
            }
        }
        public void ClickedHp()
        {
            if (HpItemlist.activeSelf == true)
            {
                HpItemlist.SetActive(false);
                for (int i = 0; i < 3; i++)
                {
                    foreach (Transform child in BarsContainer[i].transform)
                    {
                        Destroy(child.gameObject);
                    }
                }
                Loaded = false;
            }
            else
            {
                HpItemlist.SetActive(true);
                if (Loaded == false) { Loaditem(); Loaded = true; }
            }
            HungerItemlist.SetActive(false);
            HealthItemlist.SetActive(false);
        }
        public void ClickedHunger()
        {
            HpItemlist.SetActive(false);
            if (HungerItemlist.activeSelf == true)
            {
                HungerItemlist.SetActive(false);
                for (int i = 0; i < 3; i++)
                {
                    foreach (Transform child in BarsContainer[i].transform)
                    {
                        Destroy(child.gameObject);
                    }
                }
                Loaded = false;
            }
            else
            {
                HungerItemlist.SetActive(true);
                if (Loaded == false) { Loaditem(); Loaded = true; }
            }
            HealthItemlist.SetActive(false);
        }
        public void ClickedHealth()
        {
            HpItemlist.SetActive(false);
            HungerItemlist.SetActive(false);
            if (HealthItemlist.activeSelf == true)
            {
                HealthItemlist.SetActive(false);
                for (int i = 0; i < 3; i++)
                {
                    foreach (Transform child in BarsContainer[i].transform)
                    {
                        Destroy(child.gameObject);
                    }
                }
                Loaded = false;
            }
            else
            {
                HealthItemlist.SetActive(true);
                if (Loaded == false) { Loaditem(); Loaded = true; }
            }
        }
        public void ConsumeItem()
        {
            Debug.Log("Clicked");
            var PStats = PlayerManager.P_Instance.Playerpara[0];
            var Bagitem = PlayerManager.P_Instance.CarryingItem;
            int[] PlayerStats = { PStats.hp, PStats.hunger, PStats.health };
            var CurrenIButton = EventSystem.current.currentSelectedGameObject;
            var CIlist = ItemManager.I_Instance.GroupUp;
            for(int i = 0; i < 2; i++)
            {
                if(System.Array.IndexOf(CIlist[i + 2].ItemLists[0].ItemName, CurrenIButton.name) >= 0)
                {
                    Debug.Log("ran");
                    int A = System.Array.IndexOf(CIlist[i + 2].ItemLists[0].ItemName, CurrenIButton.name);
                    CIlist[i + 2].ItemLists[0].ItemNum[A] -= 1;
                    Bagitem--;
                    PlayerManager.P_Instance.CarryingItem= Bagitem;
                    CurrenIButton.GetComponentInChildren<Text>().text = "X" + CIlist[i + 2].ItemLists[0].ItemNum[A];
                    if (CIlist[i+2].ItemLists[0].ItemNum[A]== 0)
                    {
                        Debug.Log("Entered");
                        CurrenIButton.GetComponent<Button>().interactable = false;
                    }
                    for(int j = 0; j < CIlist[i + 2].ItemLists[0].RecoverStats.GetLength(1)/2;j++)
                    {
                        Debug.Log("recovered");
                        switch (CIlist[i + 2].ItemLists[0].RecoverStats[A, j * 2] - 1)
                        {
                            case 0:
                                PStats.hp+= CIlist[i + 2].ItemLists[0].RecoverStats[A, (j * 2) + 1];
                                PStats.hp =PStats.hp > 100 ? 100 : PStats.hp;
                                break;
                            case 1:
                                PStats.hunger +=CIlist[i + 2].ItemLists[0].RecoverStats[A, (j * 2) + 1];
                                PStats.hunger = PStats.hunger > 100 ? 100 : PStats.hunger;
                                break;
                            case 2:
                                PStats.health+= CIlist[i + 2].ItemLists[0].RecoverStats[A, (j * 2) + 1];
                                PStats.health = PStats.health > 100 ? 100 : PStats.health;
                                break;
                        }
                    }
                }
            }
        }
        public void LoadItemStorage(Vector3 position,/*Sprite Image,*/string IName, string itemCount, UnityEngine.Events.UnityAction method, Transform Parent)
        {
            GameObject button = new GameObject();
            GameObject text = new GameObject();
            button.transform.SetParent(Parent, true);
            button.AddComponent<RectTransform>();
            button.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
            button.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
            button.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
            button.GetComponent<RectTransform>().sizeDelta = new Vector2(50, 50);
            button.AddComponent<Button>();
            button.GetComponent<RectTransform>().anchoredPosition = position;
            button.AddComponent<Image>();
            button.GetComponent<Image>().sprite = Resources.Load<Sprite>(IName);
            button.name = IName.ToString();
            button.GetComponent<Button>().onClick.AddListener(method);

            text.transform.SetParent(button.transform, true);
            text.AddComponent<RectTransform>();
            text.GetComponent<RectTransform>().sizeDelta = new Vector2(30, 19);
            text.GetComponent<RectTransform>().localPosition = new Vector3(6, -40, 0);
            text.name = IName + "Number";
            text.AddComponent<Text>();
            text.GetComponent<Text>().font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            text.GetComponent<Text>().text = itemCount;
            text.GetComponent<Text>().color = Color.white;
        }

    }
}
