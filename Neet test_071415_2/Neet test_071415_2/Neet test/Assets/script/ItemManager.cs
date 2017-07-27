using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace battle
{
    public class AllitemList
    {
        public List<ItemList> ItemLists;
    }
   
    public class ItemList{
        public string[] ItemName;
        public int[] ItemNum;
        public int[] ItemPrice;
        public int[] Attack;
        public int[] AtkSpd;
        public string[] description;
        public int[,] RecoverStats;
        public int[] Def;
    }
    public class ItemManager : MonoBehaviour
    {
        public static ItemManager I_Instance;
        public List<AllitemList> GroupUp = null;
        public List<ItemList> I_List = null;
        public List<ItemList> S_List = null;
        public List<ItemList> D_List = null;
        public List<ItemList> F_List = null;
        public List<ItemList> FM_List = null;
        public int[][] Storage = new int[50][];
        void Awake()
        {
            if (I_Instance == null)
            {
                DontDestroyOnLoad(gameObject);
                I_Instance = this;
            }
            else if ( I_Instance != this)
            {
                Destroy(gameObject);
            }
            
        }
        void Start()
        {
            LoadItem();
            GroupUp = new List<AllitemList>();
            ItemManager.I_Instance.GroupUp.Add(new AllitemList()
            {
                ItemLists = S_List
            });
            ItemManager.I_Instance.GroupUp.Add(new AllitemList()
            {
                ItemLists = D_List
            });
            ItemManager.I_Instance.GroupUp.Add(new AllitemList()
            {
                ItemLists = F_List
            });
            ItemManager.I_Instance.GroupUp.Add(new AllitemList()
            {
                ItemLists = FM_List
            });
            ItemManager.I_Instance.GroupUp.Add(new AllitemList()
            {
                ItemLists = I_List
            });

        }
        // Update is called once per frame
        void Update()
        {

        }
        void LoadItem()
        {
            if(I_List == null)
            {
                string[] Material = new string[] {"cloth","wood","metal","plastic","screw","parts","repair material"};
                int[] initialNum = new int[] { 1, 1, 1,1, 1, 0, 0,0 };
                int[] initialNum1 = new int[] { 0, 0, 0, 0, 0, 0, 0, 0 };
                int[] ShopNum = new int[] { 0, 0, 0, 0, 0, 0, 0 };
                int[] Sprice = new int[] {30,30,20,10,10,100,150 };
                int[] Bprice = new int[] {-1,-1,-1,-1,-1,-1,200 };
                I_List = new List<ItemList>();
                ItemManager.I_Instance.I_List.Add(new ItemList()
                {
                    ItemName = Material,
                    ItemNum = initialNum,
                    ItemPrice = Sprice
                });
                ItemManager.I_Instance.I_List.Add(new ItemList()
                {
                    ItemNum = ShopNum,
                    ItemPrice = Bprice
                    //0 if ofr Player, 1++ for dungeon
                });
                ItemManager.I_Instance.I_List.Add(new ItemList()
                {
                    ItemNum = initialNum1
                });
            }
            if(S_List == null)
            {
                string[] Material = new string[] { "toy sword", "pistol", "machine gun", "assault rifle", "sniper rifle", "rocket launcher"};
                int[] initialPnum = new int[] { 0, 0, 0, 0, 0, 0,0 };
                int[] initialPnum1 = new int[] { 0, 0, 0, 0, 0, 0, 0 };
                int[] initialNum = new int[] { 1, 2, 2, 2, 2, 2};
                int[] ItemIBPrice = new int[] {120,500,750,750,1500,1500};
                int[] ItemISPrice = new int[] {50,200,300,300,720,720 };
                int[] ItemIAtk = new int[] {20,35,15,25,110,180,0 };
                int[] ItemIAtkSpd = new int[]{100,100,30,50,200,300 };
                S_List = new List<ItemList>();
                ItemManager.I_Instance.S_List.Add(new ItemList()
                {
                    ItemName = Material,
                    ItemNum = initialPnum,
                    ItemPrice = ItemISPrice,
                    Attack = ItemIAtk,
                    AtkSpd = ItemIAtkSpd
                    //0 for Player, 1 for shop  0 for sell price,1for buy price
                });
                ItemManager.I_Instance.S_List.Add(new ItemList()
                {
                    ItemNum = initialNum,
                    ItemPrice = ItemIBPrice
                    //0 for Player, 1 for shop
                });
                ItemManager.I_Instance.S_List.Add(new ItemList()
                {
                    ItemNum = initialPnum1
                });
            }
            if(D_List == null)
            {
                string[] Material = { "Light Armour", "Heavy Armour" };
                int[] initialNum = { 0, 0 ,0};
                int[] initialNum1 = { 0, 0, 0 };
                int[] ShopNum = { 0, 0 };
                int[] IStats = { 10, 25,0 };
                int[] ISPrice = {200,300 };
                int[] IBPrice = { 400,650};
                D_List = new List<ItemList>();
                ItemManager.I_Instance.D_List.Add(new ItemList()
                {
                    ItemName = Material,
                    ItemNum = initialNum,
                    Def = IStats,
                    ItemPrice = ISPrice
                });
                ItemManager.I_Instance.D_List.Add(new ItemList()
                {
                    ItemNum = ShopNum,
                    ItemPrice = IBPrice
                });
                ItemManager.I_Instance.D_List.Add(new ItemList()
                {
                    ItemNum = initialNum1
                });
            }
            if(F_List == null)
            {
                F_List = new List<ItemList>();
                string[] Material = { "onigiri", "oyatsu set","m-v itame","curry","n drink","n jelly" };
                int[] initialNum = { 0,0,0,0,0,0 ,0};
                int[] initialNum1 = { 0, 0, 0, 0, 0, 0, 0 };
                int[] ShopNum = { 0, 0 ,0,0,0,0};
                int[,] IStats = { {2,25 }, {2,50 }, { 2,75}, {2,100 }, {3,50 }, {1,30 } };
                int[] ISPrice = { 30,50,100,500,50,50};
                int[] IBPrice = { -1,250,-1,-1,300,300};
                
                ItemManager.I_Instance.F_List.Add(new ItemList()
                {
                    ItemName = Material,
                    ItemNum = initialNum,
                    RecoverStats = IStats,
                    ItemPrice = ISPrice
                });
                ItemManager.I_Instance.F_List.Add(new ItemList()
                {
                    ItemNum = ShopNum,
                    ItemPrice = IBPrice
                });
                ItemManager.I_Instance.F_List.Add(new ItemList()
                {
                    ItemNum = initialNum1
                });
            }
            if(FM_List == null)
            {
                FM_List = new List<ItemList>();
                string[] Material = { "rice", "meat", "vegetable", "roux"};
                int[] initialNum = { 0,0 ,0, 0,0};
                int[] initialNum1 = { 0, 0, 0, 0, 0 };
                int[] ShopNum = { 0, 0, 0, 0};
                int[,] IStats = { { 1, 10,3,-5 }, { 1, 10,3,-5 }, { 1, 5,2,0 }, { 1, 5,3,-5 } };
                int[] ISPrice = { 10,20,20,100};
                int[] IBPrice = { 100,200,150,-1 };
                ItemManager.I_Instance.FM_List.Add(new ItemList()
                {
                    ItemName = Material,
                    ItemNum = initialNum,
                    RecoverStats = IStats,
                    ItemPrice = ISPrice
                });
                ItemManager.I_Instance.FM_List.Add(new ItemList()
                {
                    ItemNum = ShopNum,
                    ItemPrice = IBPrice
                });
                ItemManager.I_Instance.FM_List.Add(new ItemList()
                {
                    ItemNum = initialNum1
                });
            }
        }
    }
}
