using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{

    //public GameObject tmenu;
    public Animator tanim;
    public Canvas load;



    public void Strat()
    {

        tanim.SetBool("trg", false);
    }

    public void update()
    {

    }

    public void TitleBtn()
    {
        //tmenu.SetActive(true);
        tanim.SetBool("trg", true);

    }

    public void NewGameBtn()
    {
        this.gameObject.GetComponent<SceneChangeManager>().SceneChange(SceneChangeManager.SCENE_NAME.MAP);
        load.planeDistance = 10;
    }


}
