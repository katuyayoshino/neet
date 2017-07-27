using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
namespace battle
{
    public class CraftList
    {
        public int[][] MenuList;
        public int[][] ListMat;
    }
    public class Home : MonoBehaviour
    {
        public GameObject CookingMenu;
        public GameObject Details;
        public GameObject content;
        public GameObject Confirmation;
        public GameObject ItemWindow;
        public GameObject ItemCanvas;
        public GameObject BagCanvas;
        public GameObject EQWindow;
        public GameObject CraftDetailspic;
        public GameObject WeapPic;
        public GameObject DefPic;
        public GameObject WeapSlot;
        public GameObject AmrSlot;
        public GameObject WeapList;
        public GameObject DefList;
        public GameObject MenuListView;
        public GameObject BagScroll;
        public GameObject StorageScroll;
        public Transform[] EQContainer; 
        public Transform[] CanvasContainer;
        public GameObject[] ScrollViewContainer;
        public Button MakeB;
        public Button CloseMenu;
        public int[] spacingCount= new int[] { 0,0};
        public int[] BItem = new int[2];
        public int[] loadeditem = new int[] {0,0 };
        int[][] EQpointer = new int[2][];
        public int MenuMode;
        public int ButtonIndex;
        public int Place;
        public int[][][] Ditem = new int[2][][];
        public int[][] DitemData = new int[20][];
        public int[][] SitemData = new int[20][];
        public Text ItemName;
        public Text NeededMat;
        public Text ItemDescribe;
        public Text UpdatePStats;
        public string Name;
		public GameObject SeepPop;
        
        public List<ShopBManager> Buttons = null;
        public int[][] FoodList = { new int[] {2,0,1 }, new int[] { 2, 2,2 }, new int[] { 2, 3,8 } };
        public int[][] FoodMat = { new int[] {0,2 }, new int[] { 1, 2,2,2 },new int[] {0,4,1,2,2,4,3,1 } };
        public int[][] Group ={ new int[]{ 2, 0, 0, 0 }, new int[] { 0, 2, 2, 0 },new int[] { 4, 2, 4, 1 } };
        public int[][] CraftList = { new int[] {0,0,1}, new int[] { 0, 1,3 }, new int[] { 1, 0,4 }, new int[] { 0, 2,8 },
                                     new int[] {0,3,9 },new int[] {1,1,10 },new int[] {0,4,16 },new int[] {0,5,20 }};
        public int[][] CraftMat = { new int[] {3,2}, new int[] {2,2,4,2,5,1}, new int[] {0,5,3,4,4,2 }, new int[] {2,4,4,5,5,2},
                                    new int[] {2,5,4,4,5,2},new int[] {0,6,2,6,4,4 },new int[] {2,7,4,7,5,4 },new int[] {2,10,4,6,5,4}};
        public List<CraftList> MenuData = null;
        void Start()
        {
			SeepPop.SetActive (false);
            EQWindow.SetActive(false);
            var PStats = PlayerManager.P_Instance;
            var CIlist = ItemManager.I_Instance.GroupUp;
            UpdatePStats.text = "Attack:\t" + (PStats.Playerpara[0].atk + ItemManager.I_Instance.GroupUp[0].ItemLists[0].Attack[PStats.EQ[0].EquipAddress[0]]) + "\n" +
                                           "Defence:\t" + (PStats.Playerpara[0].def + ItemManager.I_Instance.GroupUp[1].ItemLists[0].Def[PStats.EQ[0].EquipAddress[1]]);
            Ditem[0] = DitemData;
            Ditem[1] = SitemData;
            CanvasContainer = new Transform[] {BagCanvas.transform,ItemCanvas.transform };
            EQContainer = new Transform[] { WeapList.transform, DefList.transform };
            ScrollViewContainer = new GameObject[] { BagScroll, StorageScroll };
            BagCanvas.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 7 * 80);
            ItemWindow.SetActive(false);
            MenuData = new List<CraftList>();
            MenuData.Add(new CraftList()
            {
                MenuList = FoodList,
                ListMat = FoodMat
            });
            MenuData.Add(new CraftList()
            {
                MenuList = CraftList,
                ListMat = CraftMat
            });
            Details.SetActive(false);
            CookingMenu.SetActive(false);
            Confirmation.SetActive(false);
            if(PStats.EQ[0].EquipAddress[0] != 6)
            {
                WeapPic.GetComponent<Image>().sprite = Resources.Load<Sprite>(CIlist[0].ItemLists[0].ItemName[PStats.EQ[0].EquipAddress[0]]);
                WeapPic.GetComponent<Image>().color = Color.white;
                WeapSlot.GetComponent<Image>().sprite = Resources.Load<Sprite>(CIlist[0].ItemLists[0].ItemName[PStats.EQ[0].EquipAddress[0]]);
                WeapSlot.GetComponent<Image>().color = Color.white;
            }
            else if(PStats.EQ[0].EquipAddress[1]!= 2)
            {
                DefPic.GetComponent<Image>().sprite = Resources.Load<Sprite>(CIlist[0].ItemLists[0].ItemName[PStats.EQ[1].EquipAddress[1]]);
                DefPic.GetComponent<Image>().color = Color.white;
                AmrSlot.GetComponent<Image>().sprite = Resources.Load<Sprite>(CIlist[0].ItemLists[0].ItemName[PStats.EQ[1].EquipAddress[1]]);
                AmrSlot.GetComponent<Image>().color = Color.white;
            }
        }
        public void MenuListOut()
        {
            Buttons = new List<ShopBManager>();
            var Imnger = ItemManager.I_Instance.GroupUp;
            var MList = MenuData[MenuMode].MenuList;
            var LMat = MenuData[MenuMode].ListMat;
            var ListHeight = MenuListView.GetComponent<RectTransform>().sizeDelta.y / 5;
            var ListWidth = MenuListView.GetComponent<RectTransform>().sizeDelta.x;
            //int count = 0;
            CookingMenu.SetActive(true);
            ButtonIndex = 0;
            for (int i = 0; i < MList.Length; i++)
            {
               string Foodingridient = "";
                    for (int j = 0; j <LMat[i].Length/2; j++)
                    {
                           Foodingridient += Imnger[MenuMode+3].ItemLists[0].ItemName[LMat[i][j*2]] + " X" + LMat[i][(j*2)+1] + " ";
                    }
                    LoadStorage(new Vector3(0, i * -(ListHeight+3), 0), Imnger[MList[i][0]].ItemLists[0].ItemName[MList[i][1]]+"_"+ButtonIndex, Imnger[MList[i][0]].ItemLists[0].ItemName[MList[i][1]] + "\t(" + Foodingridient+")", MenuOnClick);
                     ButtonIndex++;
            }
            content.GetComponent<RectTransform>().sizeDelta = new Vector2(0, ButtonIndex * (ListHeight+3));
        }
        public void LoadStorage(Vector3 position,string IName, string itemNamenPrice, UnityEngine.Events.UnityAction method)
        {
            string[] RealName = IName.Split('_');
            var ListHeight = MenuListView.GetComponent<RectTransform>().sizeDelta.y / 5;
            var ListWidth = MenuListView.GetComponent<RectTransform>().sizeDelta.x;
            GameObject button = new GameObject();
            GameObject text = new GameObject();
            GameObject Images = new GameObject();
            button.transform.SetParent(content.transform, false);
            button.AddComponent<RectTransform>();
            button.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
            button.GetComponent<RectTransform>().anchorMax = new Vector2(1.0f, 1.0f);
            button.GetComponent<RectTransform>().pivot = new Vector2(0.0f, 1.0f);
            button.GetComponent<RectTransform>().sizeDelta = new Vector2(0, ListHeight);
            button.GetComponent<RectTransform>().localPosition = position;
            button.AddComponent<Button>();
            button.AddComponent<Image>(); 
            button.name = IName.ToString();
            button.GetComponent<Button>().onClick.AddListener(method);
            button.AddComponent<Outline>();
            button.GetComponent<Outline>().effectColor = Color.black;

            Images.AddComponent<Image>();
            Images.transform.SetParent(button.transform, false);

            Images.GetComponent<RectTransform>().sizeDelta = new Vector2(ListHeight, ListHeight);
            Images.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            Images.GetComponent<RectTransform>().anchoredPosition = new Vector3(-ListWidth/3, 0, 0);
            Images.GetComponent<Image>().sprite = Resources.Load<Sprite>(RealName[0]);

            text.transform.SetParent(button.transform, false);
            text.AddComponent<RectTransform>();
            text.GetComponent<RectTransform>().sizeDelta = new Vector2(ListWidth / 1.5f, ListHeight / 2);
            text.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            text.GetComponent<RectTransform>().anchoredPosition = new Vector3(512, 0, 0);
            text.name = IName + "Number";
            text.AddComponent<Text>();
            text.GetComponent<Text>().font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            text.GetComponent<Text>().fontSize = (int)((ListHeight / 5));
            text.GetComponent<Text>().text = itemNamenPrice;
            text.GetComponent<Text>().color = Color.black;
            text.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            text.GetComponent<RectTransform>().anchoredPosition = new Vector3(ListWidth / 6, 0, 0);
            
        }
        public void MenuOnClick()
        {
            var Imnger = ItemManager.I_Instance.GroupUp;
            var MList = MenuData[MenuMode].MenuList;
            var LMat = MenuData[MenuMode].ListMat;
            NeededMat.text = "";
            MakeB.interactable = true;
            Details.SetActive(true);
            CloseMenu.interactable = false;
            Name = EventSystem.current.currentSelectedGameObject.name;
            string[] IndexGet = Name.Split('_');
            CraftDetailspic.GetComponent<Image>().sprite = Resources.Load<Sprite>(IndexGet[0]);
            Place = int.Parse(IndexGet[1]);
            ItemName.text = IndexGet[0];
            for(int j = 0; j < LMat[Place].Length / 2; j++)
                {
                        NeededMat.text += Imnger[MenuMode+3].ItemLists[0].ItemName[LMat[Place][j*2]] + " X" + LMat[Place][(j*2)+1] +"\n";
                        if (Imnger[MenuMode + 3].ItemLists[0].ItemNum[LMat[Place][j * 2]]< MenuData[MenuMode].ListMat[Place][(j*2)+1]) 
                        {
                            MakeB.interactable = false;
                        }
                }
            ItemDescribe.text = "Time Spent:" + MList[Place][2] / 2.0f+ "hours";
        }
        public void FoodButton()
        {
            MenuMode = 0;
        }
        public void CraftButton1()
        {
            MenuMode = 1;
        }
        public void CancelB()
        {
            Details.SetActive(false);
            CloseMenu.interactable = true;
        }
        public void CraftButton()
        {
            Confirmation.SetActive(true);
        }
        public void Confirm()
        {
            var BagItem = PlayerManager.P_Instance.CarryingItem;
            var Imnger = ItemManager.I_Instance.GroupUp;
            var Proctime =TimeManager.T_Instance;
            var MList = MenuData[MenuMode].MenuList;
            var LMat = MenuData[MenuMode].ListMat;
            for (int i = 0; i < LMat[Place].Length / 2; i++)
            {
                Imnger[MenuMode + 3].ItemLists[0].ItemNum[LMat[Place][i * 2]] -= MenuData[MenuMode].ListMat[Place][(i * 2) + 1];
                BagItem--;
            }
            Imnger[MList[Place][0]].ItemLists[0].ItemNum[MList[Place][1]] += 1;
            BagItem++;
            PlayerManager.P_Instance.CarryingItem = BagItem;
            Proctime.FFTime = MList[Place][2];
            Proctime.trigger = true;
            Confirmation.SetActive(false);
            CloseMenu.interactable = true;
            Details.SetActive(false);
        }
        public void Back2List()
        {
            Confirmation.SetActive(false);
        }
        public void CloseButton()
        {
            CookingMenu.SetActive(false);
            foreach (Transform child in content.transform)
            {
                Destroy(child.gameObject);
            }
        }
        public void ExitMap()
        {
            ItemWindow.SetActive(true);
            for(int i = 0; i < 2; i++)
            {
                LoadItem(i);
                ItemStorage(i);
            }
        }
        public void CloseItem()
        {
            ItemWindow.SetActive(false);
            foreach(Transform Child in BagCanvas.transform)
            {
                Destroy(Child.gameObject);
            }
            foreach (Transform Child in ItemCanvas.transform)
            {
                Destroy(Child.gameObject);
            }
        }
        public void Exit()
        {
            SceneManager.LoadScene("mapscene");
        }
        public void ItemStorage(int index)
        {
            var BagItem = PlayerManager.P_Instance.CarryingItem;
            var ListWidth = ScrollViewContainer[index].GetComponent<RectTransform>().sizeDelta.x/2;
            var ListHeight = ScrollViewContainer[index].GetComponent<RectTransform>().sizeDelta.y/2;
            for (int i = 0; i < loadeditem[index]; i++)
                {
                    var CIlist = ItemManager.I_Instance.GroupUp[Ditem[index][i][0]].ItemLists;
                    LoadItemStorage(new Vector3(-ListWidth/2+(spacingCount[index]* ListWidth / 2), ListHeight-(index*130) - (BItem[index] / (3+(index*2))) * 80, 0),
                               CIlist[0].ItemName[Ditem[index][i][1]], "X" + (CIlist[index*2].ItemNum[Ditem[index][i][1]]).ToString(), ButtonOnClick, CanvasContainer[index]);
                    spacingCount[index]++;
                    if (spacingCount[index] > 2+(index*3)) { spacingCount[index] = 0; }
                    BItem[index]++;
                }
        }
        public void ButtonOnClick()
        {
            Debug.Log("Clicked");
            var BagItem = PlayerManager.P_Instance.CarryingItem;
            var CurrentIButton = EventSystem.current.currentSelectedGameObject;
            Name = CurrentIButton.name;
            var ButtonIndex = System.Array.IndexOf(CanvasContainer, CurrentIButton.transform.parent);
            int OtherSt = (ItemCanvas.transform == CanvasContainer[ButtonIndex] ? 0 : 1);
            for (int i = 0; i < loadeditem[ButtonIndex]; i++)
            {
                var CIlist = ItemManager.I_Instance.GroupUp[Ditem[ButtonIndex][i][0]].ItemLists;
                if (CIlist[0].ItemName[Ditem[ButtonIndex][i][1]] == Name && CIlist[ButtonIndex*2].ItemNum[Ditem[ButtonIndex][i][1]] > 0)
                {
                    if (CIlist[OtherSt * 2].ItemNum[Ditem[ButtonIndex][i][1]] == 0)
                    {
                        Debug.Log("Load");
                        LoadItemStorage(new Vector3(-65 - (OtherSt * 45) + spacingCount[OtherSt] * (65 - (OtherSt * 10)), 250 - (OtherSt * 130) - (BItem[OtherSt] / (3 + (OtherSt * 2))) * 80, 0),
                                    CIlist[0].ItemName[Ditem[ButtonIndex][i][1]], "X" + (CIlist[OtherSt * 2].ItemNum[Ditem[ButtonIndex][i][1]] + 1).ToString(), ButtonOnClick, CanvasContainer[OtherSt]);
                        Ditem[OtherSt][loadeditem[OtherSt]] = new int[] { Ditem[ButtonIndex][i][0], Ditem[ButtonIndex][i][1] };
                        loadeditem[OtherSt]++;
                        spacingCount[OtherSt]++;
                        if (spacingCount[OtherSt] > 2 + (OtherSt * 2)) { spacingCount[OtherSt] = 0; }
                        BItem[OtherSt]++;
                    }
                    CIlist[ButtonIndex * 2].ItemNum[Ditem[ButtonIndex][i][1]] -= 1;
                    CIlist[OtherSt * 2].ItemNum[Ditem[ButtonIndex][i][1]] += 1;
                    CurrentIButton.GetComponentInChildren<Text>().text = "X" + CIlist[ButtonIndex * 2].ItemNum[Ditem[ButtonIndex][i][1]];
                    CanvasContainer[OtherSt].FindChild(Name).GetComponentInChildren<Text>().text = "X" + CIlist[OtherSt * 2].ItemNum[Ditem[ButtonIndex][i][1]];
                    if (CIlist[ButtonIndex * 2].ItemNum[Ditem[ButtonIndex][i][1]] == 0)
                    {
                        int ResetSpace = 0;
                        int ResetYpos = 0;
                        int BIndex = 0;
                        for(int k = 0;k< loadeditem[ButtonIndex]; k++)
                        {
                            if(ItemManager.I_Instance.GroupUp[Ditem[ButtonIndex][k][0]].ItemLists[0].ItemName[Ditem[ButtonIndex][k][1]] == Name)
                              {
                                  BIndex = k;
                                  ResetSpace =BIndex % (3 + (ButtonIndex * 2));
                                  ResetYpos = (BIndex - (BIndex % (3 + (ButtonIndex * 2)))) / (3 + (ButtonIndex * 2));
                                  break;
                              }
                        }
                        loadeditem[ButtonIndex]--;
                        spacingCount[ButtonIndex]--; if (spacingCount[ButtonIndex] < 0) spacingCount[ButtonIndex] = 2 + (ButtonIndex * 2);
                        BItem[ButtonIndex]--;
                        for (int j = BIndex; j < loadeditem[ButtonIndex]; j++)
                        {
                            if (j!= loadeditem[ButtonIndex])
                            Ditem[ButtonIndex][j] = Ditem[ButtonIndex][j+1];
                            var MoveButton = CanvasContainer[ButtonIndex].Find(ItemManager.I_Instance.GroupUp[Ditem[ButtonIndex][j][0]].ItemLists[0].ItemName[Ditem[ButtonIndex][j][1]]);
                            if (MoveButton.GetComponent<RectTransform>().anchoredPosition.x == -65 - (ButtonIndex *45))
                            {
                                MoveButton.GetComponent<RectTransform>().localPosition += new Vector3((2+(ButtonIndex*2))*(65-(ButtonIndex*10)), 80, 0);
                            }
                            else
                            {
                                MoveButton.GetComponent<RectTransform>().localPosition += new Vector3(-65+(ButtonIndex*10), 0, 0);
                            }
                            ResetSpace++;
                            if (ResetSpace > (3 + (ButtonIndex * 2))) ResetSpace = 0;
                        }
                        GameObject.Destroy(CurrentIButton);
                    }
                    if(OtherSt == 0)
                    {
                        BagItem++;
                    }else
                    {
                        BagItem--;
                    }
                    PlayerManager.P_Instance.CarryingItem = BagItem;
                }
            }
        }
        public void LoadItem(int index)
        {
            loadeditem[index] = 0;
            for (int i = 0; i < 5; i++)
            {
                var CIlist = ItemManager.I_Instance.GroupUp[i].ItemLists;
                for (int j = 0; j < CIlist[index *2].ItemNum.Length - 1; j++)
                {
                    if (CIlist[index * 2].ItemNum[j] > 0)
                    {
                        Ditem[index][loadeditem[index]] = new int[] { i, j };
                        loadeditem[index]++;
                    }
                }
            }
        }
        public void Equipment()
        {
            EQpointer = new int[2][];
            EQWindow.SetActive(true);
            for(int i = 0; i < 2;i++)
            {
                int EQLoaded = 0;
                int Spacing = 0;
                var CIlist = ItemManager.I_Instance.GroupUp[i].ItemLists[0];
                int[] EQAdd = new int[CIlist.ItemNum.Length-1];
                for (int j = 0; j < CIlist.ItemNum.Length - 1;j++)
                {
                    if (CIlist.ItemNum[j] > 0)
                    {
                        LoadItemStorage(new Vector3(-110+(Spacing*55),50- ((EQLoaded / 5)*75), 0), CIlist.ItemName[j], "X" + CIlist.ItemNum[0].ToString(), WearEQ, EQContainer[i]);
                        EQAdd[EQLoaded] = j;
                        Spacing++; if (Spacing > 4) Spacing = 0;
                        EQLoaded++;
                    }
                }
                EQpointer[i] = EQAdd;
            }
        }
        public void WearEQ()
        {
            var CurrentIButton = EventSystem.current.currentSelectedGameObject;
            var PStats = PlayerManager.P_Instance;
            var EQIndex = System.Array.IndexOf(EQContainer, CurrentIButton.transform.parent);
            var CIlist = ItemManager.I_Instance.GroupUp[EQIndex].ItemLists[0];
            for (int i = 0; i < EQpointer[EQIndex].Length; i++)
            {
                if (CurrentIButton.name == CIlist.ItemName[EQpointer[EQIndex][i]])
                {
                    if(PStats.EQ[0].EquipAddress[EQIndex] != EQpointer[EQIndex][i])
                    {
                        PStats.EQ[0].EquipAddress[EQIndex] = EQpointer[EQIndex][i];
                        Debug.Log(EQpointer[EQIndex][i]);
                        UpdatePStats.text = "Attack:\t" + (PStats.Playerpara[0].atk + ItemManager.I_Instance.GroupUp[0].ItemLists[0].Attack[PStats.EQ[0].EquipAddress[0]]) +"\n"+
                                            "Defence:\t" + (PStats.Playerpara[0].def + ItemManager.I_Instance.GroupUp[1].ItemLists[0].Def[PStats.EQ[0].EquipAddress[1]]);
                    }
                }
            }
            if(EQIndex == 0)
            {
                WeapPic.GetComponent<Image>().sprite = Resources.Load<Sprite>(CurrentIButton.name);
                WeapPic.GetComponent<Image>().color = Color.white;
                WeapSlot.GetComponent<Image>().sprite = Resources.Load<Sprite>(CurrentIButton.name);
                WeapSlot.GetComponent<Image>().color = Color.white;
            }
            else if(EQIndex == 1)
            {
                DefPic.GetComponent<Image>().sprite = Resources.Load<Sprite>(CurrentIButton.name);
                DefPic.GetComponent<Image>().color = Color.white;
                AmrSlot.GetComponent<Image>().sprite = Resources.Load<Sprite>(CurrentIButton.name);
                AmrSlot.GetComponent<Image>().color = Color.white;
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
        public void CloseEQWindow()
        {
            EQWindow.SetActive(false);
        }

		public void Rest()
		{
			SeepPop.SetActive (true);
		}
		public void Rest_push()
		{
			SeepPop.SetActive (false);
			PlayerManager.P_Instance.Playerpara [0].hp += PlayerManager.P_Instance.Playerpara [0].hp / 12;
		}

    }
}







