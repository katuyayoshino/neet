using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace battle
{
    public class PlayerStats
    {
        [Range(0, 100)]public int hp;
        [Range(0, 100)]public int hunger;
        [Range(0, 100)]public int health;
        public int itemCarry;
        public int atk;
        public int def;
        public int spd;
        public int money;
    }
    public class PlayerEquipment{
        public int[] EquipAddress;
        public int Durability;
    }
    public class PlayerManager : MonoBehaviour
    {
        public int CarryingItem = 0;
        public static PlayerManager P_Instance;
        public List<PlayerStats> Playerpara = null;
        public List<PlayerEquipment> EQ = null;
        // Use this for initialization
        void Awake()
        {
            if(P_Instance == null)
            {
                DontDestroyOnLoad(gameObject);
                P_Instance = this;
            }else if(P_Instance != this)
            {
                Destroy(gameObject);
            }
            Playerpara = new List<PlayerStats>();
            PlayerManager.P_Instance.Playerpara.Add(new PlayerStats()
            {
                hp = 100,
                hunger = 100,
                health = 100,
                itemCarry = 20,
                atk = 10,
                def = 5,
                spd = 100,
                money = 1000
            });
            EQ = new List<PlayerEquipment>();
            PlayerManager.P_Instance.EQ.Add(new PlayerEquipment()
            {
                EquipAddress = new int[] { 6, 2 }

            });
        }
        void Start()
        {
            for ( int i = 0; i < 5; i++)
            {
                var Imnger = ItemManager.I_Instance.GroupUp[i].ItemLists[0];
                PlayerManager.P_Instance.CarryingItem += Imnger.ItemNum[Imnger.ItemNum.Length-1];
            }
          
        }
    }
}