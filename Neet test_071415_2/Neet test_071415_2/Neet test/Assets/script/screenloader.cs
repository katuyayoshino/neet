using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace battle
{
    public class screenloader : MonoBehaviour
    {

        public GameObject DManager;
        public GameObject TManager;
        public GameObject Window;
        public GameObject Map;
        public GameObject rDot;
        public GameObject ShopMain;
        public Text DesName;
        public Button Home;
        public Button D1;
        public Button D2;
        public Button D3;
        public Button ShopButton;
        public TimeManager ProcTime;
        public DungeonManager Call;
        public Text NeededTime;
        public Vector3[] MapPos;
        public bool Onprocess = false;
        public int OrgPos = 0;
        public int PickedD=-1; 
        // Use this for initialization
        void Start()
        {
            ShopMain.SetActive(false);
            OrgPos = DungeonManager.Instance.currentPos;
            MapPos =new[]{Home.transform.position,D1.transform.position,D2.transform.position, D3.transform.position, ShopButton.transform.position };
            rDot.transform.position = Home.transform.position;
            rDot.SetActive(false);
            DManager = GameObject.Find("DungeonManager");
            TManager = GameObject.Find("TimeManager");
            ProcTime = TManager.GetComponent<TimeManager>();
            Call = DManager.GetComponent<DungeonManager>();
            D2.enabled = false;
            D2.transform.localScale = new Vector3(0, 0, 0);
            D3.enabled = false;
            D3.transform.localScale = new Vector3(0, 0, 0);
            ShopButton.enabled = false;
            ShopButton.transform.localScale = new Vector3(0, 0, 0);
            if (Call.StartTrigger == true)
            {
                if (Call.DungeonStatus[0].DungeonCleared == true)
                {
                    D2.enabled = true;
                    D2.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                    ShopButton.enabled = true;
                    ShopButton.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                }
            }
            if(Call.DungeonENum >= 1)
            {
                if(Call.DungeonStatus[1].DungeonCleared == true)
                {
                    D3.enabled = true;
                    D3.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                }
            }
            
            Window.SetActive(false);
        }
        // Update is called once per frame
        void Update()
        {
            if (ProcTime.Ending == true)
            {
                ProcTime.Ending = false;
                if (PickedD == 0)
                {
                    LoadHome();
                }else if (PickedD == 1)
                {
                    Call.Dungeon1();
                    LoadBattle();
                }else if (PickedD == 2)
                {
                    Call.Dungeon2();
                    LoadBattle();
                }
                else if(PickedD == 3)
                {
                    Call.Dungeon3();
                    LoadBattle();
                }
                else if(PickedD == 4)
                {
                    LoadShop();
                }
            }
        }
        public void LoadHome()
        {
            SceneManager.LoadScene("homescene", LoadSceneMode.Single);
            DungeonManager.Instance.currentPos = PickedD;
        }
        public void LoadBattle()
        {

            DungeonManager.Instance.currentPos = PickedD;
            SceneManager.LoadScene("battlescene", LoadSceneMode.Single);
            
        }
        public void LoadShop()
        {
            
            ShopMain.SetActive(true);
        }
        public void CloseShop()
        {
            ShopMain.SetActive(false);
            Onprocess = false;
            rDot.SetActive(false);
        }
        public void Dungeon1B()
        {
            if (Onprocess == false)
            {
                Onprocess = true;
                if (OrgPos != 1) {
                    Window.SetActive(true);
                    DesName.text = "コンビニ";
                    ProcTime.FFTime = 2;
                    NeededTime.text = "所要時間:" + ProcTime.FFTime/2 + "時間";
                    PickedD = 1;
                }
                else
                {
                    PickedD = 1;
                    LoadBattle();
                }
            }
        }
        public void Dungeon2B()
        {
            if (Onprocess == false)
            {
                Onprocess = true;
                if (OrgPos != 2)
                {
                    Window.SetActive(true);
                    DesName.text = "商店街";
                    ProcTime.FFTime = 2;
                    NeededTime.text = "所要時間:" + ProcTime.FFTime/2 + "時間";
                    PickedD = 2;
                }
                else
                {
                    PickedD = 2;
                    LoadBattle();
                }
            }
        }
        public void Dungeon3B()
        {
            if (Onprocess == false)
            {
                Onprocess = true;
                if (OrgPos != 3)
                {
                    Window.SetActive(true);
                    DesName.text = "公園";
                    ProcTime.FFTime = 2;
                    NeededTime.text = "所要時間:" + ProcTime.FFTime/2 + "時間";
                    PickedD = 3;
                }
                else
                {
                    PickedD = 3;
                    LoadBattle();
                }
            }
        }
        public void ShopB()
        {
            if (Onprocess == false)
            {
                Onprocess = true;
                if (OrgPos != 4)
                {
                    Window.SetActive(true);
                    DesName.text = "雑貨屋";
                    ProcTime.FFTime = 2;
                    NeededTime.text = "所要時間:" + ProcTime.FFTime/2 + "時間";
                    PickedD = 4;
                }
                else
                {
                    PickedD = 4;
                    LoadShop();
                }
            }
        }
        public void HomeB()
        {
            if (Onprocess == false)
            {
                Onprocess = true;
                if (OrgPos != 0)
                {
                    DesName.text = "家";
                    Window.SetActive(true);
                    ProcTime.FFTime = 2;
                    NeededTime.text = "所要時間:" + ProcTime.FFTime/2 + "時間";
                    PickedD = 0;
                }
                else
                {
                    PickedD = 0;
                    LoadHome();
                }
            }
        }
        
        public void proceed()
        {
            Window.SetActive(false);
            ProcTime.trigger=true;
            rDot.SetActive(true);
            StartCoroutine("MovingDots");
        }
        public void Cancel()
        {
            Window.SetActive(false);
            Onprocess = false;
            ProcTime.FFTime = 0;
            PickedD = 0;
        }
       IEnumerator MovingDots()
        {
            rDot.transform.position = MapPos[OrgPos];
            var NextPos = MapPos[PickedD];
            Vector3 relativePos = NextPos - rDot.transform.position;
            for(int i= 0; i < 60; i++)
            {
                rDot.transform.position += relativePos / 60;
                yield return new WaitForSeconds(0.01f);
            }
            OrgPos = PickedD;
            StopCoroutine("MovingDots");
        }
    }
}
