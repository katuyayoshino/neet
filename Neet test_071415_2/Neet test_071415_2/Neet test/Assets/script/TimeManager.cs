using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace battle
{
    public class TimeManager : MonoBehaviour
    {
        public static TimeManager T_Instance;
        public GameObject hpbar;
        public GameObject healthbar;
        public GameObject hungerbar;
        public GameObject ItemBar;
        public Text Times;
        public Text Date;
        public bool trigger = false;
        public bool Ending = false;
        public int counter = -1;
        public int FFTime;
        public int hour;
        public int minutes;
        public int Days;
        public float Delay;
        void Awake()
        {
            if (T_Instance == null)
            {
                DontDestroyOnLoad(gameObject);
                T_Instance = this;
            }
            else if (T_Instance != this)
            {
                Destroy(gameObject);
            }
        }

        // Use this for initialization
        void Start()
        {
            hour = 12;
            minutes = 00;
            Days = 1;
            Delay = 1.0f;
            StartCoroutine("Time");
            
        }
        // Update is called once per frame
        void Update()
        {
            if (trigger)
            {
                trigger = false;
                counter = FFTime*30;
                Delay = 0.01f;
            }
            Date.text = Days + "日目";
            if (minutes < 10)
            {
                Times.text = hour + ":0" + minutes;
            }
            else
            {
                Times.text = hour + ":" + minutes;
            }
            if (PlayerManager.P_Instance.Playerpara[0].hp<=0)
            {
                Debug.Log("game over");
            }
        }
        IEnumerator Time()
        {
            var PStats = PlayerManager.P_Instance.Playerpara[0];
            var Hpb = hpbar.GetComponent<Image>();
            var Healthb = healthbar.GetComponent <Image>();
            var Hungerb = hungerbar.GetComponent<Image>();
            var Itemb = ItemBar.GetComponent<Image>();
            float A = PStats.itemCarry;
            while (minutes < 59)
            {
                minutes++;
                Hpb.fillAmount = PStats.hp / 100.0f;
                Hungerb.fillAmount = PStats.hunger / 100.0f;
                Healthb.fillAmount = PStats.health / 100.0f;
                if(PStats.hp<= 0)
                {
                    SceneManager.LoadScene("GameOver");
                    Destroy(gameObject);
                }
                Itemb.fillAmount = PlayerManager.P_Instance.CarryingItem/A;
                if (PStats.health <= 50)
                {
                    PStats.hp--;
                    Hpb.fillAmount = PStats.hp / 100.0f;

                }
                if(minutes % 5 == 0)
                {
                    if (PStats.hunger > 0)
                    {
                        PStats.hunger--;
                        Hungerb.fillAmount = PStats.hunger / 100.0f;
                    }
                    if (PStats.hunger < 50)
                    {
                        PStats.hp--;
                    }
                    else if (PStats.hunger < 20)
                    {
                        PStats.hp -= 2;
                    }
                    Hpb.fillAmount = PStats.hp / 100.0f;
                }
                if(counter > 0)
                {
                    counter--;
                }else if(counter == 0)
                {
                    counter = -1;
                    FFTime = 0;
                    Ending = true;
                    Delay = 1.0f;
                }

                yield return new WaitForSeconds(Delay);
            }
            PlayerManager.P_Instance.Playerpara[0].hunger--;
            StopCoroutine("Time");
            Addhour();
        }
        void Addhour()
        {
            minutes = 0;
            if (hour < 23)
            {
                hour++;
            }else
            {
                Days++;
                hour = 0;
            }
            StartCoroutine("Time");
        }
    }
}