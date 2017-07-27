using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class returnMap : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	// Update is called once per frame
	void Update () {
		
	}
    public void LoadMap()
    {
        SceneManager.LoadScene("mapscene", LoadSceneMode.Single);
    }
}
