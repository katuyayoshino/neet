using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadPlane : MonoBehaviour {

	public Image _progressImg = null;
	public Text _progressText = null;

	private void Start()
	{
		_progressText.text = "0 %";
		_progressImg.fillAmount = 0f;
	}
}
