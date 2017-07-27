using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace battle
{
    public class LootContainer
    {
        public GameObject Loots;
    }
    public class showEnemyData : MonoBehaviour
    {
        public Text text1;
        public Text text2;
        public Text EventLog;
        public Text Btlelog;
        public int logLine;
        public int currentStg;
        public int attTrgt=0;
        public int StartEnd = 0;
        public string[] PatternName = new string[]{"Engage","Reward","endingDungeon"};
        public string[] Dname = new string[] { "コンビニ" , "商店街" , "公園" };
        public string[] StartEvent = new string[] {"近所のコンビニたまに武装職安\nの姿もみかける","近所の商店街駅前に比べれば\n落ち着いた雰囲気","近所の公園よく催し事が\n開催されている"};
        public GameObject E1;
        public GameObject E2;
        public GameObject E3;
        public GameObject E4;
        public GameObject ItemWindow;
        public GameObject firstButtonSet;
        public GameObject SecondButtonSet;
        public GameObject ThirdButtonSet;
        public GameObject returnB;
        public GameObject BattleLog;
        public GameObject Texts;
        public GameObject board;
        public GameObject TManager;
        public GameObject ItemCanvas;
        public GameObject itemScroll;
        public GameObject bagScroll;
        public GameObject SerifuMnger;
        public GameObject BagCanvas;
        public GameObject[] CanvasContainer;
        public GameObject[] ScrollContainer;
        public List<DungeonEnemy> TempD = null;
        public List<DungeonData> TempS = null;
        public List<LootContainer> Items = null;
        public int Stages;
        public int loadeditem = 0;
        public int itemCount = 0;
        public int EnemyNum = 0;
        public int spacingCount = 0;
        public int BItem = 0;
        public int ContentSize = 1;
        public TimeManager ProcTime;
        public class Enemypic
        {
            public GameObject[] Enemys;
        }
        public List<Enemypic> Epics = null;
        public int[][] Ditem = new int[10][];



        // Use this for initialization
        private void Awake()
        {
            Debug.Log("awake");
            Stages = DungeonManager.Instance.currentPos-1;
            E1.GetComponent<Renderer>().enabled = false;
            E2.GetComponent<Renderer>().enabled = false;
            E3.GetComponent<Renderer>().enabled = false;
            E4.GetComponent<Renderer>().enabled = false;
            text2.transform.localScale = new Vector3(0, 0, 0);
            returnB.SetActive(false);
            firstButtonSet.SetActive(false);
            SecondButtonSet.SetActive(false);
            ThirdButtonSet.SetActive(false);
            BattleLog.SetActive(false);
            currentStg = DungeonManager.Instance.DungeonStatus[Stages].CrntStg;
            EventLog.transform.localScale = new Vector3(0, 0, 0);
            
        }
        void Start()
        {
            Debug.Log(Stages);
            CanvasContainer =new GameObject[]{ItemCanvas, BagCanvas };
            ScrollContainer = new GameObject[] { itemScroll, bagScroll };
            Items = new List<LootContainer>();
            ItemWindow.SetActive(false);
            TManager = GameObject.Find("TimeManager");
            ProcTime = TManager.GetComponent<TimeManager>();
            TempS = new List<DungeonData>();
            TempS.Add(new DungeonData()
            {
                EnemyNum = DungeonManager.Instance.DungeonStatus[Stages].EnemyNum,
                DungeonLevel = DungeonManager.Instance.DungeonStatus[Stages].DungeonLevel,
                DungeonCleared = DungeonManager.Instance.DungeonStatus[Stages].DungeonCleared,
                StgPattern = DungeonManager.Instance.DungeonStatus[Stages].StgPattern,
                battleCount = DungeonManager.Instance.DungeonStatus[Stages].battleCount,
                StgENum = DungeonManager.Instance.DungeonStatus[Stages].StgENum,
                ItemStgNum= DungeonManager.Instance.DungeonStatus[Stages].ItemStgNum
            });
            EnemyNum = TempS[0].StgENum;
            Debug.Log("fin 1");
            if (DungeonManager.Instance.DungeonStatus[Stages].DungeonCleared == false)
            {
                if (DungeonManager.Instance.DungeonStatus[Stages].DungeonEntered == false)
                {
                    logLine = 0;
                    EventLog.transform.localScale = new Vector3(1, 1, 1);
                    EventChatLog();
                }
                Epics = new List<Enemypic>();
                Epics.Add(new Enemypic()
                {
                    Enemys = new GameObject[] {E1,E2,E3,E4}
                });
                TempD = new List<DungeonEnemy>();
                for (int i = 0; i < TempS[0].EnemyNum; i++)
                {
                    TempD.Add(new DungeonEnemy()
                    {
                        hp = DungeonManager.Instance.DungeonCEstatus[i].hp,
                        attack = DungeonManager.Instance.DungeonCEstatus[i].attack,
                        defence = DungeonManager.Instance.DungeonCEstatus[i].defence,
                        attackspeed = DungeonManager.Instance.DungeonCEstatus[i].attackspeed
                    });
                }
            }
            Debug.Log("finishStart");
            LoadItem();
            if (DungeonManager.Instance.DungeonStatus[Stages].DungeonEntered == true)
            StartCoroutine("StartChat");
        }
        IEnumerator StartChat(){
            yield return null;
            EventLog.text = "";
            board.transform.localScale = new Vector3(1, 1, 1);
            board.transform.localPosition = new Vector3(0, -476, 0);
            firstButtonSet.SetActive(true);
            returnB.SetActive(true);
            text1.rectTransform.localPosition = new Vector3(-200, -200, 0);
            text1.rectTransform.sizeDelta = new Vector2(500, 120);
            text1.text = Dname[DungeonManager.Instance.DungeonENum]+"[進行度" + currentStg + "/" + TempS[0].DungeonLevel + "]";
            text2.transform.localScale = new Vector3(1, 1, 1);
            text2.rectTransform.localPosition = new Vector3(0, -360, 0);
            text2.rectTransform.sizeDelta = new Vector2(720, 120);
            text2.text = StartEvent[DungeonManager.Instance.DungeonENum];
            StopCoroutine("StartChat");
        }
        IEnumerator Engage()
        {
            yield return new WaitForSeconds(0.5f);
            board.transform.localScale = new Vector3(1, 1, 1);
            board.transform.position = new Vector3(0, -2.84f, 0);
            Texts.SetActive(true);
            SecondButtonSet.SetActive(true);
            for (int i = 0; i < EnemyNum; i++)
            {
                Epics[0].Enemys[i].GetComponent<Renderer>().enabled = true;
            }
            text1.rectTransform.localPosition = new Vector3(20,-120, 0);
            text1.rectTransform.sizeDelta = new Vector2(720, 120);
            text1.text = "武装職安隊が待ち構えている！";
            text1.fontSize = 50;
            ProcTime.FFTime = 4;
            text2.text = "数" + EnemyNum + "\t" + "危険度：低\n"+ "時間：" + ProcTime.FFTime / 2 + "時間" + "\t" + "階層:" + (currentStg + 1) + "/" + TempS[0].DungeonLevel;
            text2.rectTransform.localPosition = new Vector3(0, -250, 0);
            text2.fontSize = 40;
        }
        public void EnterOnClicked()
        {
            if (TempS[0].DungeonCleared != true && currentStg != TempS[0].DungeonLevel) {
                firstButtonSet.SetActive(false);
                returnB.SetActive(false);
                StartCoroutine(PatternName[TempS[0].StgPattern[currentStg]]);
            }
        }
       public void BackOnclicked()
        {
            StartCoroutine("StartChat");
            SecondButtonSet.SetActive(false);
        }
        public void BackOnclicked2()
        {
            StartCoroutine("StartChat");
            ThirdButtonSet.SetActive(false);
        }
        public void GoOnClicked()
        {
            currentStg++;
            DungeonManager.Instance.DungeonStatus[Stages].CrntStg=currentStg;
            Texts.SetActive(false);
           SecondButtonSet.SetActive(false);
            StartCoroutine("Logging");
        }
        IEnumerator Logging()
        {
            var PlayerData = PlayerManager.P_Instance.Playerpara;
            var EQData = PlayerManager.P_Instance.EQ[0].EquipAddress;
            var CIlist = ItemManager.I_Instance.GroupUp[0].ItemLists[0].Attack;
            var CIlistDef = ItemManager.I_Instance.GroupUp[1].ItemLists[0].Def;
            int counter = 0;
            attTrgt = TempS[0].battleCount;
            BattleLog.SetActive(true);
            Btlelog.text = "";
            while (PlayerData[0].hp >0  &&  TempD[TempS[0].battleCount+EnemyNum-1].hp > 0)
            {
                TempD[attTrgt].hp -= PlayerData[0].atk+ CIlist[EQData[0]];
                counter++;
                if(counter >= 10) {
                    int removeP = Btlelog.text.IndexOf("\n");
                    Btlelog.text = Btlelog.text.Remove(0, removeP + 1);
                   
                }
                Btlelog.text += "たかしの攻撃" + "\t" + "隊員Ａに" + (PlayerData[0].atk+CIlist[EQData[0]]) + "ダメージ"+"\n";
                yield return new WaitForSeconds(1.0f);
                if (TempD[attTrgt].hp <= 0)
                {
                    StartCoroutine("Blinking");
                    attTrgt++;
                    counter++;
                    if (counter >= 10)
                    {
                        int removeP = Btlelog.text.IndexOf("\n");
                        Btlelog.text = Btlelog.text.Remove(0, removeP + 1);
                    }
                    Btlelog.text += "\t" + "隊員Ａは倒れた！" + "\n";
                    
                }
                else
                {
                    PlayerData[0].hp -= (TempD[attTrgt].attack- (PlayerData[0].def + CIlistDef[EQData[1]]));
                    counter++;
                    if (counter >= 10)
                    {
                        int removeP = Btlelog.text.IndexOf("\n");
                        Btlelog.text = Btlelog.text.Remove(0, removeP + 1);
                       
                    }
                    Btlelog.text += "隊員Ａの攻撃" + "\t" + "たかしに" + (TempD[attTrgt].attack- (PlayerData[0].def+CIlistDef[EQData[1]])) + "ダメージ" + "\n";
                    
                }
                yield return new WaitForSeconds(1.0f);
            }
            if (PlayerData[0].hp <= 0)
            {
                Debug.Log("gameOver");
            }
            else if (TempD[TempS[0].battleCount + EnemyNum - 1].hp <= 0)
            {
                TempS[0].battleCount = EnemyNum;
                DungeonManager.Instance.DungeonStatus[Stages].battleCount = EnemyNum;
                EnemyNum++;
                DungeonManager.Instance.DungeonStatus[Stages].StgENum = EnemyNum;
                Btlelog.text = "すべての敵を倒した！" + "\n" + "たかしは少し成長した！";
                ProcTime.trigger = true;
                yield return new WaitForSeconds(1.0f);
                BattleLog.SetActive(false);
                attTrgt = 0;
                yield return new WaitForSeconds(2.0f);
                StartCoroutine(PatternName[TempS[0].StgPattern[currentStg]]);
                
            }
        }
        IEnumerator Reward()
        {
            yield return null;
            ThirdButtonSet.SetActive(true);
            text1.rectTransform.localPosition = new Vector3(20, -80, 0);
            board.transform.localScale = new Vector3(0.95f, 1, 1);
            board.transform.position = new Vector3(0, -2.84f, 0);
            text2.rectTransform.localPosition = new Vector3(0, -120, 0);
            Texts.SetActive(true);
            text1.text = "アイテムボックスが落ちている";
            ProcTime.FFTime = 2;
            text2.text = "時間：" + ProcTime.FFTime/2 + "時間\n"+ "階層：" + currentStg + "/" + TempS[0].DungeonLevel;
          
        }
        public void OpenButton()
        {
            ProcTime.trigger = true;
            currentStg++;
            Debug.Log(TempS[0].ItemStgNum);
            DungeonManager.Instance.DungeonStatus[Stages].CrntStg = currentStg;
            text1.text = "";
            if (TempS[0].ItemStgNum > 0)
            {
                Debug.Log("ran");
                Debug.Log(Stages);
                var ItemList = ItemManager.I_Instance.GroupUp;
                for (int i = 2; i < 5; i++)
                {
                    for (int j = 0; j < ItemList[i].ItemLists[Stages + 3].ItemNum.Length; j++)
                    {
                        ItemList[i].ItemLists[Stages + 3].ItemNum[j] += ItemList[i].ItemLists[Stages + 3 + TempS[0].ItemStgNum].ItemNum[j];
                    }
                }
                LoadItem();
            }
            Debug.Log("ran2");
            for (int i = 0; i < loadeditem; i++)
            {
                var CIlist = ItemManager.I_Instance.GroupUp[Ditem[i][0] + 2].ItemLists;
                Debug.Log(CIlist[Stages + 3 + TempS[0].ItemStgNum].ItemNum[Ditem[i][1]]);
                text1.text += CIlist[0].ItemName[Ditem[i][1]] + " X" + CIlist[Stages + 3+TempS[0].ItemStgNum].ItemNum[Ditem[i][1]] + "\n";
                Debug.Log(CIlist[0].ItemName[Ditem[i][1]]);
            }
            text2.text = "";
            TempS[0].ItemStgNum++;
            DungeonManager.Instance.DungeonStatus[Stages].ItemStgNum = TempS[0].ItemStgNum;
            ThirdButtonSet.SetActive(false);
            StartCoroutine(PatternName[TempS[0].StgPattern[currentStg]]);
        }
        IEnumerator endingDungeon()
        {
            yield return new WaitForSeconds(2.0f);
            text1.text = "";
            for (int i = 0; i < TempS[0].EnemyNum; i++) {
                DungeonManager.Instance.DungeonCEstatus[i].hp = TempD[i].hp;
                    }
            TempS[0].DungeonCleared = true;
            DungeonManager.Instance.DungeonStatus[Stages].DungeonCleared = TempS[0].DungeonCleared;
            yield return new WaitForSeconds(1.0f);
            StartEnd = 1;
            ClearLog();
        }
        private void ClearLog()
        {
            EventLog.text = "";
            EventLog.transform.localScale = new Vector3(1, 1, 1);
            logLine = 0;
            EventChatLog();
        }
        IEnumerator Blinking()
        {
            int target = attTrgt;
            if (attTrgt > 2)
            {
                target -= 3;
            }
            
            for (int i = 0; i < 3; i++)
            {
                Epics[0].Enemys[target].GetComponent<Renderer>().enabled = true;
                yield return new WaitForSeconds(0.2f);
                Epics[0].Enemys[target].GetComponent<Renderer>().enabled = false;
                yield return new WaitForSeconds(0.2f);
            }
        }
        public void itemButton()
        {
            ItemWindow.SetActive(true);
            Texts.SetActive(false);
            firstButtonSet.SetActive(false);
            returnB.SetActive(false);
            Debug.Log(loadeditem);
            if (TempS[0].ItemStgNum >0)
            {
                int Spacing = 0;
                var BagItem = PlayerManager.P_Instance.CarryingItem;
                var itemWidth = itemScroll.GetComponent<RectTransform>().sizeDelta.x/2;
                var itemHeight = itemScroll.GetComponent<RectTransform>().sizeDelta.y/2;
                for (int i = 0; i < loadeditem; i++)
                {
                    var CIlist = ItemManager.I_Instance.GroupUp[Ditem[i][0] + 2].ItemLists;
                    if (CIlist[Stages + 3].ItemNum[Ditem[i][1]] > 0) { 
                    LoadStorage(new Vector3(-itemWidth/3 + (Spacing * itemWidth / 3), itemHeight/2 - ((i / 5) * itemHeight / 2), 0), CIlist[0].ItemName[Ditem[i][1]], "X" + CIlist[Stages + 3].ItemNum[Ditem[i][1]].ToString(), ButtonOnClick, ItemCanvas.transform);
                        Spacing++; if (Spacing > 4) { Spacing = 0; }
                    }
                }
                //ItemCanvas.GetComponent<RectTransform>().sizeDelta = new Vector2(0, loadeditem / 4 * 80);
            }
            //ContentSize = 1;
            BItem = 0;
            var ListWidth = bagScroll.GetComponent<RectTransform>().sizeDelta.x / 2;
            var ListHeight = bagScroll.GetComponent<RectTransform>().sizeDelta.y / 2;
            for (int i = 0; i < 5; i++)
                {
                    var CIlist = ItemManager.I_Instance.GroupUp[i].ItemLists[0];
                    for(int j= 0; j < CIlist.ItemNum.Length - 1; j++)
                    {
                        if(CIlist.ItemNum[j] != 0)
                        {
                            LoadStorage(new Vector3(-ListWidth/2 + spacingCount*(ListWidth / 2), ListHeight/3 - (BItem / 3) * ListHeight / 3, 0),CIlist.ItemName[j], "X" + CIlist.ItemNum[j].ToString(), ButtonOnClick,BagCanvas.transform);
                            spacingCount++;
                            if (spacingCount > 2) { spacingCount = 0; }
                            BItem++;
                        }
                }
                    BagCanvas.GetComponent<RectTransform>().sizeDelta = new Vector2(0,7 * 80);
            }
        }
        public void LoadStorage(Vector3 position,/*Sprite Image,*/string IName, string itemCount, UnityEngine.Events.UnityAction method,Transform Parent)
        {
            Debug.Log("build");
            GameObject button = new GameObject();
            var ListWidth = bagScroll.GetComponent<RectTransform>().sizeDelta.x /6;
            
            GameObject text = new GameObject();
            button.transform.SetParent(Parent, false);
            button.AddComponent<RectTransform>();
            button.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
            button.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
            button.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
            button.GetComponent<RectTransform>().sizeDelta = new Vector2(ListWidth,ListWidth);
            button.AddComponent<Button>();
            button.GetComponent<RectTransform>().anchoredPosition = position;
            button.AddComponent<Image>();
            button.GetComponent<Image>().sprite =Resources.Load<Sprite>(IName);
            button.name = IName.ToString();
            button.GetComponent<Button>().onClick.AddListener(method);

            text.transform.SetParent(button.transform, false);
            text.AddComponent<RectTransform>();
            text.GetComponent<RectTransform>().sizeDelta = new Vector2(ListWidth, ListWidth/2);
            text.GetComponent<RectTransform>().localPosition = new Vector3(6, -40, 0);
            text.name = IName + "Number";
            text.AddComponent<Text>();
            text.GetComponent<Text>().font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            text.GetComponent<Text>().text = itemCount;
            text.GetComponent<Text>().color = Color.white;
            Items.Add(new LootContainer()
            {
                Loots = button
            });
        }
        public void ButtonOnClick()
        {
            var BagItem = PlayerManager.P_Instance.CarryingItem;
            var Name = EventSystem.current.currentSelectedGameObject.name;
            var CurrentIbutton = EventSystem.current.currentSelectedGameObject;
            var ParentsIndex = EventSystem.current.currentSelectedGameObject.transform.parent;
            if (BagItem < 20)
            {
                if (ParentsIndex == CanvasContainer[0].transform)
                {
                    for (int i = 0; i < loadeditem; i++)
                    {
                        var CIlist = ItemManager.I_Instance.GroupUp[Ditem[i][0] + 2].ItemLists;
                        if (CIlist[0].ItemName[Ditem[i][1]] == Name && CIlist[Stages + 3].ItemNum[Ditem[i][1]] > 0)
                        {
                            CIlist[Stages + 3].ItemNum[Ditem[i][1]] -= 1;
                            if (CIlist[Stages + 3].ItemNum[Ditem[i][1]] == 0)
                            {
                                CurrentIbutton.SetActive(false);
                            }
                            if (CIlist[0].ItemNum[Ditem[i][1]] == 0)
                            {
                                LoadStorage(new Vector3(-65 + spacingCount * (65), 250 - (BItem / 3) * 80, 0), CIlist[0].ItemName[Ditem[i][1]], "X" + (CIlist[0].ItemNum[Ditem[i][1]] + 1).ToString(), ButtonOnClick, BagCanvas.transform);
                                spacingCount++;
                                if (spacingCount > 2) { spacingCount = 0; BagCanvas.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 7 * 80); }
                                BItem++;
                            }
                            CIlist[0].ItemNum[Ditem[i][1]] += 1;
                            BagItem++;
                            PlayerManager.P_Instance.CarryingItem = BagItem;
                            CurrentIbutton.GetComponentInChildren<Text>().text = "X" + CIlist[Stages + 3].ItemNum[Ditem[i][1]];
                            CanvasContainer[1].transform.FindChild(Name).GetComponentInChildren<Text>().text = "X" + CIlist[0].ItemNum[Ditem[i][1]];
                        }
                    }
                }
            }
        }
        public void EventChatLog()
        {
             var ChatMnger = SerifuMnger.GetComponent<Serifu>();
            if (logLine >= ChatMnger.ChatLog[DungeonManager.Instance.DungeonENum].chats[StartEnd].Length)
            {
                DungeonManager.Instance.DungeonStatus[Stages].DungeonEntered = true;
                EventLog.transform.localScale = new Vector3(0, 0, 0);
                StartCoroutine("StartChat");
            }
            else
            {
                EventLog.text = ChatMnger.ChatLog[DungeonManager.Instance.DungeonENum].chats[StartEnd][logLine];
                logLine++;
            }
        }
        public void LoadItem()
        {
            loadeditem = 0;
            for (int i = 0; i < 3; i++)
            {
                var CIlist = ItemManager.I_Instance.GroupUp[i + 2].ItemLists;
                Debug.Log(CIlist[Stages + 3].ItemNum.Length - 1);
                for (int j = 0; j < CIlist[Stages + 3].ItemNum.Length - 1; j++)
                {

                    Debug.Log(CIlist[Stages + 3].ItemNum[CIlist[Stages + 3].ItemNum.Length - 1]);
                    
                    if (CIlist[Stages + 3].ItemNum[CIlist[Stages + 3].ItemNum.Length - 1] == 0)
                    {
                        break;
                    }
                    Debug.Log(CIlist[Stages + 3].ItemNum[j]);
                    if (CIlist[Stages + 3].ItemNum[j] > 0)
                    {
                        Ditem[loadeditem] = new int[] { i, j };
                        loadeditem++;
                    }
                    Debug.Log(loadeditem);
                }
            }
        }
        public void CloseStorage()
        {
            ItemWindow.SetActive(false);
            foreach (Transform child in BagCanvas.transform)
            {
                Destroy(child.gameObject);
            }
            foreach (Transform child in ItemCanvas.transform)
            {
                Destroy(child.gameObject);
            }
            Texts.SetActive(true);
            firstButtonSet.SetActive(true);
            returnB.SetActive(true);
        }
    }
}
