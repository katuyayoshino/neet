using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace battle
{
   
    public class DungeonData
    {
        public int DungeonLevel;
        public int[] StgPattern;
        public int EnemyNum;
        public int CrntStg;
        public int battleCount;
        public bool DungeonCleared;
        public bool DungeonEntered;
        public int StgENum;
        public int ItemStgNum;
    }
    public class DungeonType
    {
        public List<DungeonData> DData;
    }

    public class DungeonEnemy
    {
        public int hp;
        public int attack;
        public int defence;
        public int attackspeed;
    }
    public class DungeonManager : MonoBehaviour
    {
        public static DungeonManager Instance;
        
        void Awake()
        {
                if (Instance == null)
                {
                    DontDestroyOnLoad(gameObject);
                    Instance = this;
                }
                else if (Instance != this)
                {
                    Destroy(gameObject);
                }
            
        }
        
        List<EnemyData> EnemyStatus = null;
        public List<DungeonData> DungeonStatus = null;
        public List<DungeonEnemy> DungeonCEstatus = null;
        public int currentPos = 0;
        public int DungeonENum = 0;
        public bool StartTrigger = false;
        public int[][] E_Type = { new int[] { 0, 0 }, new int[] { 0, 0, 0, 0, 3 }, new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 3 } };
         int[] HpData = new int[]{35,100,200,500};
         int[] AttackData = new int[] {10,20,35,35,15,25,15,25,110,180};
        int[] DefenceData = new int[] {0,5,20,30};
         int[] AttackSpdData = new int[] {100,100,100,100,30,50,30,50,200,300};
        void LoadData()
        {
            if (EnemyStatus != null) { return; }
            EnemyStatus = new List<EnemyData>();
           for(int i = 0; i < 4; i++)
            {
                
                for (int j = 0; j < 3; j++)
                {
                    if (!(i == 3 && j > 0)) {
                        EnemyStatus.Add(new EnemyData()
                        {
                            hp = HpData[i],
                            attack = AttackData[(i*3)+j],
                            defence = DefenceData[i],
                            attackspeed =AttackSpdData[(i * 3) + j]
                        });
                    }else
                    {
                        break;
                    }
                }
            }
            
        } // Use this for initialization
        void Start()
        {
            LoadData();
            DungeonManager.Instance.currentPos = 0;
            DungeonStatus = new List<DungeonData>();
        }
        // Update is called once per frame
        void Update()
        {
            
        }
        private void BuildDungeon(int TekiNum,int DunLvl,int SEnum,int itemSNum,int BttCount,int Stgs,bool Dunsts,bool DunEntered,int[] enemyType,int[] pattern)
        {
            DungeonManager.Instance.DungeonStatus.Add(new DungeonData()
            {
                EnemyNum = TekiNum,
                DungeonLevel = DunLvl,
                battleCount = BttCount,
                StgENum = SEnum,
                DungeonCleared = Dunsts ,
                CrntStg = Stgs,
                StgPattern=pattern,
                ItemStgNum= itemSNum
            });
            DungeonCEstatus = new List<DungeonEnemy>();
            for (int j = 0; j < DungeonStatus[DungeonENum].EnemyNum; j++)
                {
                    DungeonManager.Instance.DungeonCEstatus.Add(new DungeonEnemy()
                    {
                        hp = EnemyStatus[enemyType[j]/3].hp,
                        attack=EnemyStatus[enemyType[j]].attack,
                        defence= EnemyStatus[enemyType[j]].defence,
                        attackspeed= EnemyStatus[enemyType[j]].attackspeed
                    });
                }
        }
        public void Dungeon1()
        {
            if (StartTrigger == false)
            {
                DungeonManager.Instance.StartTrigger = true;
                DungeonManager.Instance.DungeonENum = 0;
                int[] S_Pattern = new int[] {0,1,2 };
                int[] Etc = new int[] { 5, 0, 0, 5, 0, 0, 0, 10 };
                int[] Food = new int[] { 0, 3, 0, 0, 0, 0, 3 };
                int[] ingredient = new int[] { 0,0,0,0,0};
                AddItem(Etc,Food,ingredient);
                
                BuildDungeon(2, 2, 2,0,0,0, false, false,E_Type[DungeonENum],S_Pattern);
           }
        }
        public void Dungeon2()
        {
            if (DungeonStatus[0].DungeonCleared == true && DungeonManager.Instance.DungeonENum <1)
            {
                DungeonManager.Instance.DungeonENum = 1;
                int[] S_Pattern = new int[] { 0,0,1, 2 };
                int[] Etc = new int[] { 5, 5, 3, 3, 0, 0, 0, 16 };
                int[] Food = new int[] { 0, 0, 0, 0, 0, 0,0 };
                int[] ingredient = new int[] { 5, 3, 3, 0, 11 };
                AddItem(Etc, Food, ingredient);
                BuildDungeon(5, 3,2,0,0,0, false,false, E_Type[DungeonENum],S_Pattern);
            }
        }
        public void Dungeon3()
        {
            if (DungeonStatus[1].DungeonCleared == true && DungeonManager.Instance.DungeonENum < 2)
            {
                DungeonManager.Instance.DungeonENum = 2;
                int[] S_Pattern = new int[] { 0, 0, 1,0, 1, 2 };
                int[] Etc = new int[] { 0, 0, 0, 0, 0, 0, 0, 0 };
                int[] Food = new int[] { 0, 2, 0, 0, 0, 1, 3 };
                int[] ingredient = new int[] { 2, 0, 0, 0, 2 };
                int[] S_Pattern1 = new int[] { 0, 0, 1, 0, 1, 2 };
                int[] Etc1 = new int[] { 5, 5, 3, 3, 0, 5, 0, 21 };
                int[] Food1 = new int[] { 0, 5, 0, 0, 2, 1, 3 };
                int[] ingredient1 = new int[] { 2, 0, 0, 0, 2 };
                AddItem(Etc, Food, ingredient);
                AddItem(Etc1, Food1, ingredient1);
                Debug.Log("ran");
                BuildDungeon(9, 5, 2,0, 0, 0, false, false, E_Type[DungeonENum], S_Pattern);
            }
        }
        public void AddItem(int[] Etc,int[] Food,int[] Ingredient)
        {
            ItemManager.I_Instance.GroupUp[4].ItemLists.Add(new ItemList()
            {
                ItemNum = Etc
            });
            ItemManager.I_Instance.GroupUp[2].ItemLists.Add(new ItemList()
            {
                ItemNum = Food
            });
            ItemManager.I_Instance.GroupUp[3].ItemLists.Add(new ItemList()
            {
                ItemNum = Ingredient
            });
        }
    }
}
